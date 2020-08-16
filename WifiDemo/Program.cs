using System;
using System.Diagnostics;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using Windows.Devices.WiFi;

namespace WifiDemo
{
    /// <summary>
    /// Sample:https://github.com/nanoframework/Samples/tree/master/samples/Wifi
    /// </summary>
    public class Program
    {

        const string MYSSID = "";
        const string MYPASSWORD = "";


        public static void Main()
        {
            Debug.WriteLine("Hello from nanoFramework wifi!");

            try
            {
                // Get the first WiFI Adapter
                WiFiAdapter wifi = WiFiAdapter.FindAllAdapters()[0];

                // Set up the AvailableNetworksChanged event to pick up when scan has completed
                wifi.AvailableNetworksChanged += Wifi_AvailableNetworksChanged;

                Debug.WriteLine("starting WiFi scan");
                wifi.ScanAsync();
                

                var remoteIp = IPAddress.Any;
                var receivePort = 12345;

                Debug.WriteLine("receive message");
                
                using (var udpReceive = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp))
                {
                    udpReceive.Bind(new IPEndPoint(remoteIp, receivePort));

                    var resBytes = new byte[1024];

                    while (true)
                    {
                        Debug.WriteLine("wait...");
                        
                        var resSize = udpReceive.Receive(resBytes);
                        
                        var receiveMsg = Encoding.UTF8.GetString(resBytes, 0 ,resSize);

                        Debug.WriteLine($"receiveData   : {receiveMsg}");
                        
                        if (receiveMsg == "exit") break;
                    }
                    udpReceive.Close();
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("message:" + ex.Message);
                Debug.WriteLine("stack:" + ex.StackTrace);
            }

            Thread.Sleep(Timeout.Infinite);

        }

        /// <summary>
        /// WiFiスキャンが完了したときのイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private static void Wifi_AvailableNetworksChanged(WiFiAdapter sender, object e)
        {
            Debug.WriteLine("Wifi_AvailableNetworksChanged - get report");

            // スキャンされたすべてのWiFiネットワークのレポートを取得
            WiFiNetworkReport report = sender.NetworkReport;

            // ネットワークを探して列挙する
            foreach (WiFiAvailableNetwork net in report.AvailableNetworks)
            {
                // 見つかったすべてのネットワークを表示
                Debug.WriteLine($"Net SSID :{net.Ssid},  BSSID : {net.Bsid},  rssi : {net.NetworkRssiInDecibelMilliwatts.ToString()},  signal : {net.SignalBars.ToString()}");

                // 自分のネットワークの場合、接続する
                if (net.Ssid == MYSSID)
                {
                    // すでに接続されている場合は切断します
                    sender.Disconnect();

                    // ネットワークに接続する
                    WiFiConnectionResult result = sender.Connect(net, WiFiReconnectionKind.Automatic, MYPASSWORD);

                    // ステータス表示
                    if (result.ConnectionStatus == WiFiConnectionStatus.Success)
                    {
                        Debug.WriteLine("Connected to Wifi network");
                    }
                    else
                    {
                        Debug.WriteLine($"Error {result.ConnectionStatus} connecting o Wifi network");
                    }
                }
            }
        }
    }
}
