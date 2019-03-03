using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using InTheHand.Net;
using InTheHand.Net.Bluetooth;
using InTheHand.Net.Sockets;

namespace UP_Bluetooth
{
    public partial class ChooseDevice : Form
    {
        private readonly BluetoothClient _client = new BluetoothClient();
        private BluetoothDeviceInfo[] _devices;
        private BluetoothDeviceInfo _chosenDevice;
        private readonly Thread _listener = new Thread(Listener);

        private static void Listener()
        {
            //Metoda nasłuchująca czy sparowane urządzenia Bluetooth chcą wysłać plik
            while (true)
            {
                //Stworzenie i uruchomienie słuchacza dla Bluetooth
                var listener = new ObexListener(ObexTransport.Bluetooth);
                listener.Start();
                //GetContext zwraca wartość null jeśli nie wykrywa zapytania od urządzenia
                var obexContext = listener.GetContext();
                if (obexContext != null)
                {
                    var confirmResult = MessageBox.Show("Urządzenie bluetooth chce wysłać plik. Czy chcesz go odebrać?",
                        "Przychodzący plik",
                        MessageBoxButtons.YesNo);
                    if (confirmResult == DialogResult.Yes)
                    {
                        //Pobranie przychodzącego requesta z urządzenia
                        var obexRequest = obexContext.Request;
                        //Ustalenie ścieżki zapisu pliku na komputerze
                        var pathSplits = obexRequest.RawUrl.Split('/');
                        var fileName = @"C:\Users\DELL\Desktop\Politechnika\Semestr 5\Urządzenia peryferyjne\LAB\UP_Bluetooth\" + pathSplits[pathSplits.Length - 1];
                        //Zapisanie pliku
                        obexRequest.WriteFile(fileName);
                    }
                }
                listener.Stop();
            }
        }

        public ChooseDevice(BluetoothRadio chosenAdapter)
        {
            _listener.Start();
            InitializeComponent();
        }
        private async void ChooseDevice_Shown(Object sender, EventArgs e)
        {
            //Ekran ładowania "Wyszukiwane Urządzeń..."
            await Task.Run(() =>
            {
                //Wyszukiwanie urządzeń Bluetooth
                _devices = _client.DiscoverDevices();
                //Dodanie nazw i adresów MAC urządzeń do Combo Boxa
                foreach (var device in _devices)
                {
                    comboBox1.Items.Add(device.DeviceName + "(MAC: " + device.DeviceAddress + ")");
                }

                comboBox1.SelectedIndex = 0;
            }).ContinueWith(task => BeginInvoke(new Action(() =>
            {
                label2.Visible = false;
                button2.Visible = true;
                button1.Visible = true;
                comboBox1.Visible = true;
                label1.Visible = true;
            })));
        }
        private void button1_Click(object sender, EventArgs e)
        {
            //Ustawienie wybranego urządzenia
            _chosenDevice = _devices[comboBox1.SelectedIndex];
            //Wysłanie prośby o parowania, jeśli zakonczy się powodzeniem to aktywujemy przycisk do wysłania pliku
            if (BluetoothSecurity.PairRequest(_chosenDevice.DeviceAddress, "0000"))
            {
                button2.Enabled = true;
                button1.Enabled = false;
            }
            else
            {
                MessageBox.Show("Wystąpił błąd w trakcie operacji parowania", "Błąd autoryzacji",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void button2_Click(object sender, EventArgs e)
        {
            //Wysyłanie pliku z komptera do urządzenia

            //Wybór pliku
            var result = openFileDialog1.ShowDialog();
            if (result == DialogResult.OK)
            {
                //Zapamiętanie nazwy pliku
                var file = openFileDialog1.FileName;
                try
                {
                    _chosenDevice.Update();
                    _chosenDevice.Refresh();
                    //Umożliwienie wysyłania plików między urządzeniami
                    _chosenDevice.SetServiceState(BluetoothService.ObexObjectPush, true);
                    //Ustalenie ścieżki pliku
                    var filePath = file.Replace(@"\\", @"\");
                    var fileToSend = new Uri("obex://" + _chosenDevice.DeviceAddress + "/" + filePath);
                    //Stworzenie requesta do wysłania pliku
                    var obexRequest = new ObexWebRequest(fileToSend);
                    //Wysłanie pliku
                    obexRequest.ReadFile(filePath);
                    //Zebranie informacji zwrotnej
                    var obexResponse = (ObexWebResponse)obexRequest.GetResponse();
                    MessageBox.Show(obexResponse.StatusCode.ToString());
                    obexResponse.Close();
                }
                catch (IOException)
                {
                }
            }
        }
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            //W zależności od tego czy urządzenia są sparowane aktywujemy odpowiednie przyciski
            if (!_devices[comboBox1.SelectedIndex].Authenticated)
            {
                button1.Enabled = true;
                button2.Enabled = false;
            }
            else
            {
                button1.Enabled = false;
                button2.Enabled = true;
            }
        }
    }
}
