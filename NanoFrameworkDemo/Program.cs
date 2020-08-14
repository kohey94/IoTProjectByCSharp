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

            // LÉ`ÉJ
            /*
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
            */

            var pin22 = gpio.OpenPin(22);
            var pin23 = gpio.OpenPin(23);
            var pin25 = gpio.OpenPin(25);
            var pin26 = gpio.OpenPin(26);

            pin22.SetDriveMode(GpioPinDriveMode.Output);
            pin23.SetDriveMode(GpioPinDriveMode.Output);
            pin25.SetDriveMode(GpioPinDriveMode.Output);
            pin26.SetDriveMode(GpioPinDriveMode.Output);


            while (true)
            {
                pin22.Write(GpioPinValue.High);
                pin23.Write(GpioPinValue.Low);
                pin25.Write(GpioPinValue.High);
                pin26.Write(GpioPinValue.Low);
                Thread.Sleep(500);
                pin22.Write(GpioPinValue.Low);
                pin23.Write(GpioPinValue.Low);
                pin25.Write(GpioPinValue.Low);
                pin26.Write(GpioPinValue.Low);
                Thread.Sleep(500);
                pin22.Write(GpioPinValue.Low);
                pin23.Write(GpioPinValue.High);
                pin25.Write(GpioPinValue.Low);
                pin26.Write(GpioPinValue.High);
                Thread.Sleep(500);
                pin22.Write(GpioPinValue.Low);
                pin23.Write(GpioPinValue.Low);
                pin25.Write(GpioPinValue.Low);
                pin26.Write(GpioPinValue.Low);
                Thread.Sleep(500);

            }

        }

        static void SetPin(GpioPin pin)
        {
            pin.SetDriveMode(GpioPinDriveMode.Output);
            pin.Write(GpioPinValue.Low);
        }

        static void Forward()
        {

        }
    }
}
