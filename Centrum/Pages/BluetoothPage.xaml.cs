using InTheHand.Net.Bluetooth;
using InTheHand.Net.Sockets;

namespace Centrum.Pages;

public partial class BluetoothPage : ContentPage
{
	public BluetoothPage()
	{
		InitializeComponent();
        PinMessage.SelectedIndex = 1;
        ButtonOdKomend.IsEnabled = false;
	}

    private void CheckedBox(object sender, CheckedChangedEventArgs e)
    {
        if(!isPinRequest.IsChecked && !isLcdRequest.IsChecked)
            ButtonOdKomend.IsEnabled = false;
        else
            ButtonOdKomend.IsEnabled = true;
    }

    private async void WyslijKomende(object sender, EventArgs e)
    {
        string message = "";
        if (isPinRequest.IsChecked)
        {
            int pinMode = PinMessage.SelectedIndex==0 ? 1 : 0;
            message = $"{pinMode}";
        }
        message += ":";
        if (LcdMessage.Text!=null && isLcdRequest.IsChecked)
        {
            if(LcdMessage.Text.Length > 32)
            {
                await DisplayAlert("Uwaga", "Wiadomosc nie moze byc dluzsza niz 32 znaki!", "OK");
                return;
            }
            message = isLcdRequest.IsChecked ? message + $"{LcdMessage.Text}" : message+"^";
        }
        else
        {
            message += "^";
        }
        labelStan.Text = "Wykonywanie komendy";
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
                if (!isPaired)
                {
                    labelStan.Text = "Nie uda³o siê sparowaæ";
                    return;
                }
            }
            else
                labelStan.Text = "Urz¹dzenie nie autoryzowane";
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
                        byte[] dataToSend = System.Text.Encoding.ASCII.GetBytes(message);
                        stream.Write(dataToSend, 0, dataToSend.Length);
                        labelStan.Text = "Komenda wys³ana";
                    }
                }
                else
                    labelStan.Text = "Nie uda³o siê po³¹czyæ";
            }
        }
        else
            labelStan.Text = "Nie znaleziono urz¹dzenia";
    }
}