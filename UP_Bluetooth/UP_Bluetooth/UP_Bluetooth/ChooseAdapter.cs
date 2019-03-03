using System;
using System.Windows.Forms;
using InTheHand.Net.Bluetooth;

namespace UP_Bluetooth
{
    public partial class ChooseAdapter : Form
    {
        private BluetoothRadio[] _adapters;

        public ChooseAdapter()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //Wyszukiwanie adapterów Bluetooth
            _adapters = BluetoothRadio.AllRadios;
            //Dodawanie nazw adapterów do Combo Boxa
            foreach (var adapter in _adapters)
            {
                comboBox1.Items.Add(adapter.Name);
            }

            comboBox1.SelectedIndex = 0;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //Wywołanie okna ChooseDevice
            BeginInvoke(new Action(() =>
            {
                using (var chooseDevice = new ChooseDevice(_adapters[comboBox1.SelectedIndex]))
                {
                    chooseDevice.ShowDialog();
                }
            }));
        }
    }
}
