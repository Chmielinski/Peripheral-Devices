using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WIA;
using CommonDialog = WIA.CommonDialog;

namespace scanner
{
    public partial class Form1 : Form
    {
        private ScannerClass device;

        public Form1()
        {
            InitializeComponent();
            ListDevices();
            device = (ScannerClass)comboBox2.SelectedItem;
            radioButton1.Checked = true;
        }

        public void ListDevices()
        {
            var deviceManager = new DeviceManager();

            for (int i = 1; i <= deviceManager.DeviceInfos.Count; i++)
            {
                if (deviceManager.DeviceInfos[i].Type == WiaDeviceType.ScannerDeviceType)
                {
                    comboBox2.Items.Add(new ScannerClass(deviceManager.DeviceInfos[i]));
                }
            }
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            device = (ScannerClass)comboBox2.SelectedItem;
        }
        
        private void button1_Click(object sender, EventArgs e)
        {
            pictureBox1.Image = null;
            if (device != null)
            {
                var connectedDevice = device._deviceInfo.Connect();
                var scannerItem = connectedDevice.Items[1];

                ImageFile image;
                //USTAWIENIA
                //KOLOR
                if (radioButton1.Checked)
                    SetColorMode(scannerItem, 1);
                else if(radioButton2.Checked)
                {
                    SetColorMode(scannerItem, 2);
                }
                else if (radioButton3.Checked)
                {
                    SetColorMode(scannerItem, 4);
                }

                int dpi = Int32.Parse(comboBox3.Text);
                SetDPI(scannerItem, dpi);
                
                SetHeightWidth(scannerItem,1250,1700);

                SetBrightness(scannerItem, trackBar1.Value);
                SetContrast(scannerItem, trackBar2.Value);

                switch (comboBox1.SelectedItem)
                {
                    case "png":
                        image = (ImageFile) scannerItem.Transfer(FormatID.wiaFormatPNG);
                        break;
                    case "jpeg":
                        image = (ImageFile)scannerItem.Transfer(FormatID.wiaFormatJPEG);
                        break;
                    case "tiff":
                        image = (ImageFile)scannerItem.Transfer(FormatID.wiaFormatTIFF);
                        break;
                    default:
                        image = (ImageFile)scannerItem.Transfer(FormatID.wiaFormatJPEG);
                        break;
                }

                var path = $"{textBox1.Text}.{comboBox1.SelectedItem}";

                if (File.Exists(path))
                {
                    File.Delete(path);
                }

                image.SaveFile(path);
                pictureBox1.Image = new Bitmap(path);
                pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
            }
            else
            {
                MessageBox.Show("Wybierz urządzenie");
            }
        }

        const string WIA_SCAN_COLOR_MODE = "6146";
        const string WIA_HORIZONTAL_SCAN_RESOLUTION_DPI = "6147";
        const string WIA_VERTICAL_SCAN_RESOLUTION_DPI = "6148";

        const string WIA_HORIZONTAL_SCAN_START_PIXEL = "6149";
        const string WIA_VERTICAL_SCAN_START_PIXEL = "6150";

        const string WIA_HORIZONTAL_SCAN_SIZE_PIXELS = "6151";
        const string WIA_VERTICAL_SCAN_SIZE_PIXELS = "6152";

        const string WIA_SCAN_BRIGHTNESS_PERCENTS = "6154";
        const string WIA_SCAN_CONTRAST_PERCENTS = "6155";
       
        public void SetColorMode(IItem scannerItem, int mode)
        {
            SetWIAProperty(scannerItem.Properties,WIA_SCAN_COLOR_MODE, mode );
        }

        public void SetHeightWidth(IItem scannerItem, int width, int height)
        {
            SetWIAProperty(scannerItem.Properties, WIA_HORIZONTAL_SCAN_SIZE_PIXELS, width);
            SetWIAProperty(scannerItem.Properties, WIA_VERTICAL_SCAN_SIZE_PIXELS, height);
        }

        public void SetDPI(IItem scannerItem, int dpi)
        {
            SetWIAProperty(scannerItem.Properties, WIA_HORIZONTAL_SCAN_RESOLUTION_DPI, dpi);
            SetWIAProperty(scannerItem.Properties, WIA_VERTICAL_SCAN_RESOLUTION_DPI, dpi);
        }

        private static void SetWIAProperty(IProperties props, object propName, object propValue)
        {
            Property property = props.get_Item(ref propName);
            property.set_Value(ref propValue);
        }

        public void SetBrightness(IItem scannerItem , int value)
        {
            SetWIAProperty(scannerItem.Properties, WIA_SCAN_BRIGHTNESS_PERCENTS, value);
        }

        public void SetContrast(IItem scannerItem, int value)
        {
            SetWIAProperty(scannerItem.Properties, WIA_SCAN_CONTRAST_PERCENTS, value);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            const string wiaFormatJPEG = "{B96B3CAE-0728-11D3-9D7B-0000F81EF32E}";
            String Archivo = "skannnnn";
            ICommonDialog wiaDiag = new WIA.CommonDialog();
            ImageFile wiaImage = null;
            wiaImage = wiaDiag.ShowAcquireImage(WiaDeviceType.UnspecifiedDeviceType, WiaImageIntent.GrayscaleIntent, WiaImageBias.MaximizeQuality,
                wiaFormatJPEG, true, true, false);
            Vector vector = wiaImage.FileData;
            pictureBox1.Image = Image.FromStream(new MemoryStream((byte[])vector.get_BinaryData()));

            Image img = Image.FromStream(new MemoryStream((byte[])vector.get_BinaryData()));
            img.Save(Archivo + ".TIFF");
        }
    }
}