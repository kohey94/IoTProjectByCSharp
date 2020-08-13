using System.Diagnostics;
using System.Threading;
using Windows.Devices.Gpio;

namespace NanoFrameworkDemo
{
    public class Program
    {
        public static void Main()
        {
            Debug.WriteLine("Hello from nanoFramework!");

            var gpio = new GpioController();
            var pin25 = gpio.OpenPin(25);
            pin25.SetDriveMode(GpioPinDriveMode.Output);
            Debug.WriteLine($"Pin number: {pin25.PinNumber}");
            Debug.WriteLine($"Pin drive mode: {pin25.GetDriveMode()}");

            while (true)
            {
                pin25.Write(GpioPinValue.High);
                Thread.Sleep(500);
                pin25.Write(GpioPinValue.Low);
                Thread.Sleep(500);
            }

            // Browse our samples repository: https://github.com/nanoframework/samples
            // Check our documentation online: https://docs.nanoframework.net/
            // Join our lively Discord community: https://discord.gg/gCyBu8T
        }
    }
}
