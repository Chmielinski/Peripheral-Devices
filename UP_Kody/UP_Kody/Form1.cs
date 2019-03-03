using System;
using System.Text;
using System.Drawing;
using System.Drawing.Imaging;
using System.Drawing.Text;
using System.Drawing.Drawing2D;
using System.Drawing.Printing;
using System.IO;
using System.Data;
using System.Net;
using System.Runtime;
using System.Windows.Forms;

namespace UP_Kody
{
    public partial class Form1 : Form
    {
        private string[] odd = { "0001101", "0011001", "0010011", "0111101", "0100011", "0110001", "0101111", "0111011", "0110111", "0001011" };
        private string[] even = { "0100111", "0110011", "0011011", "0100001", "0011101", "0111001", "0000101", "0010001", "0001001", "0010111" };
        private string[] parity = { "oooooo", "ooeoee", "ooeeoe", "ooeeeo", "oeooee", "oeeooe", "oeeeoo", "oeoeoe", "oeoeeo", "oeeoeo" };
        private string[] right = { "1110010", "1100110", "1101100", "1000010", "1011100", "1001110", "1010000", "1000100", "1001000", "1110100" };

        public Form1()
        {
            InitializeComponent();
            maskedTextBox1.Select();
        }
        private void button2_Click(object sender, EventArgs e)
        {
            PrintDocument printDocument = new PrintDocument();
            printDocument.PrintPage += PrintGeneratedBarcode;
            printDocument.Print();
        }

        private static void PrintGeneratedBarcode(object o, PrintPageEventArgs e)
        {
            System.Drawing.Image image = System.Drawing.Image.FromFile(@"C:\Users\lab\Desktop\myimage.jpg");
            Point loc = new Point(100, 100);
            e.Graphics.DrawImage(image, loc);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            var eanCode = maskedTextBox1.Text;
            var controlSum = 0;
            for (var i = 0; i < eanCode.Length - 1; i += 2)
            {
                controlSum += Int32.Parse(eanCode[i] + "");
                controlSum += 3 * Int32.Parse(eanCode[i + 1] + "");
            }
            controlSum = controlSum % 10;
            controlSum = 10 - controlSum;
            if (controlSum == 10)
            {
                controlSum = 0;
            }
            var barcode = new StringBuilder();
            barcode.Append("101");
            var first = Convert.ToInt32(maskedTextBox1.Text[0]) - 48;
            var scheme = parity[first];
            for (var i = 1; i < 7; i++)
            {
                if (scheme[i - 1] == 'o')
                {
                    barcode.Append(odd[Convert.ToInt32(maskedTextBox1.Text[i]) - 48]);
                }
                if (scheme[i - 1] == 'e')
                {
                    barcode.Append(even[Convert.ToInt32(maskedTextBox1.Text[i]) - 48]);
                }
            }
            barcode.Append("01010");
            for (var i = 7; i < 12; i++)
            {
                barcode.Append(right[Convert.ToInt32(maskedTextBox1.Text[i]) - 48]);
            }

            barcode.Append(right[controlSum]);

            barcode.Append("101");

            Font barCodeF = new Font("Free 3 of 9", 45, FontStyle.Regular, GraphicsUnit.Pixel);
            Font plainTextF = new Font("Arial", 20, FontStyle.Regular, GraphicsUnit.Pixel);

            Bitmap bmp = new Bitmap(1, 1);
            Graphics graphics = Graphics.FromImage(bmp);

            int barCodewidth = (int)graphics.MeasureString(maskedTextBox1.Text, barCodeF).Width + 100;
            int barCodeHeight = (int)graphics.MeasureString(maskedTextBox1.Text, barCodeF).Height;

            int plainTextWidth = (int)graphics.MeasureString(maskedTextBox1.Text, plainTextF).Width + 100;
            int plainTextHeight = (int)graphics.MeasureString(maskedTextBox1.Text, plainTextF).Height;

            if (barCodewidth > plainTextWidth)
            {
                bmp = new Bitmap(bmp,
                                 new Size(barCodewidth, barCodeHeight + plainTextHeight));
            }
            else
            {
                bmp = new Bitmap(bmp,
                                 new Size(plainTextWidth, barCodeHeight + plainTextHeight));
            }
            graphics = Graphics.FromImage(bmp);

            graphics.Clear(Color.White);
            graphics.SmoothingMode = SmoothingMode.AntiAlias;
            graphics.TextRenderingHint = TextRenderingHint.AntiAlias;

            for (var i = 0; i < 95; i++)
            {
                if (i < 3 || (i > 45 && i < 50) || i > 91)
                {
                    if (barcode[i] == '0')
                    {
                        graphics.DrawString("|",
                            barCodeF,
                            new SolidBrush(Color.White),
                            10 + i * 3,
                            15);
                    }
                    else
                    {
                        graphics.DrawString("|",
                            barCodeF,
                            new SolidBrush(Color.Black),
                            10 + i * 3,
                            15);
                    }
                }
                if (barcode[i] == '0')
                {
                    graphics.DrawString("|",
                        barCodeF,
                        new SolidBrush(Color.White),
                        10 + i * 3,
                        0);
                }
                else
                {
                    graphics.DrawString("|",
                        barCodeF,
                        new SolidBrush(Color.Black),
                        10 + i * 3,
                        0);
                }
            }
            var numbers = new StringBuilder();
            numbers.Append(maskedTextBox1.Text[0] - 48);
            numbers.Append("   ");
            for (var i = 1; i < 7; i++)
            {
                numbers.Append(Convert.ToInt32(maskedTextBox1.Text[i]) - 48);
                numbers.Append("  ");
            }
            numbers.Append("");
            for (var i = 7; i < 12; i++)
            {
                numbers.Append(Convert.ToInt32(maskedTextBox1.Text[i]) - 48);
                numbers.Append("  ");
            }
            numbers.Append(controlSum);
            graphics.DrawString(Convert.ToString(numbers),
                        plainTextF,
                        new SolidBrush(Color.Black),
                        0,
                        barCodeHeight);

            bmp.Save(@"C:\Users\DELl\Desktop\myimage.jpg", ImageFormat.Jpeg);
            pictureBox1.Image = bmp;
        }
    }
}