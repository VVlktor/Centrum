using InTheHand.Net.Bluetooth;
using InTheHand.Net.Sockets;

namespace Centrum.Pages;

public partial class BluetoothPage : ContentPage
{
	public BluetoothPage()
	{
		InitializeComponent();
	}


    private async void WyslijKomende(object sender, EventArgs e)
    {
        czyPolaczylo.Text = "Wykonywanie komendy";
        var czyPozwolenie = await Permissions.RequestAsync<Permissions.Bluetooth>();
        if (czyPozwolenie != PermissionStatus.Granted)
        {
            await Navigation.PopAsync();
        }
        var devicePicker = new BluetoothDevicePicker();
        devicePicker.ClassOfDevices.Add(new ClassOfDevice(DeviceClass.Uncategorized, ServiceClass.None));
        var device = await devicePicker.PickSingleDeviceAsync();
        if (device != null)
        {
            if (!device.Authenticated)
            {
                bool isPaired = BluetoothSecurity.PairRequest(device.DeviceAddress, BluetoothService.SerialPort.ToString());
                if (!isPaired) return;
            }
            using (var client = new BluetoothClient())
            {
                if (client.Connected)
                {
                    client.Close();
                }
                await client.ConnectAsync(device.DeviceAddress, BluetoothService.SerialPort);
                if (client.Connected)
                {
                    await Task.Delay(2000);
                    using (Stream stream = client.GetStream())
                    {
                        string message = glownyEntry.Text;
                        message = message == "" ? "T" : message;
                        byte[] dataToSend = System.Text.Encoding.ASCII.GetBytes(message);
                        stream.Write(dataToSend, 0, dataToSend.Length);
                        czyPolaczylo.Text = "Komenda wyslana pomyslnie";
                    }
                }
            }
        }
    }


}