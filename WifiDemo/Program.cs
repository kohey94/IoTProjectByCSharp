using System;
using System.Threading;
using System.Diagnostics;
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

                // Loop forever scanning every 30 seconds
                while (true)
                {
                    Debug.WriteLine("starting WiFi scan");
                    wifi.ScanAsync();

                    Thread.Sleep(30000);
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
        /// WiFi�X�L���������������Ƃ��̃C�x���g
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private static void Wifi_AvailableNetworksChanged(WiFiAdapter sender, object e)
        {
            Debug.WriteLine("Wifi_AvailableNetworksChanged - get report");

            // �X�L�������ꂽ���ׂĂ�WiFi�l�b�g���[�N�̃��|�[�g���擾
            WiFiNetworkReport report = sender.NetworkReport;

            // �l�b�g���[�N��T���ė񋓂���
            foreach (WiFiAvailableNetwork net in report.AvailableNetworks)
            {
                // �����������ׂẴl�b�g���[�N��\��
                Debug.WriteLine($"Net SSID :{net.Ssid},  BSSID : {net.Bsid},  rssi : {net.NetworkRssiInDecibelMilliwatts.ToString()},  signal : {net.SignalBars.ToString()}");

                // �����̃l�b�g���[�N�̏ꍇ�A�ڑ�����
                if (net.Ssid == MYSSID)
                {
                    // ���łɐڑ�����Ă���ꍇ�͐ؒf���܂�
                    sender.Disconnect();

                    // �l�b�g���[�N�ɐڑ�����
                    WiFiConnectionResult result = sender.Connect(net, WiFiReconnectionKind.Automatic, MYPASSWORD);

                    // �X�e�[�^�X�\��
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
