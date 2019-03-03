using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SlimDX.DirectInput;

namespace USB
{
    public partial class Form1 : Form
    {
        Graphics g;
        private int x = -1;
        private int y = -1;
        private bool moving = false;
        private Pen pen;
        private Color currentColor = Color.Black;
        private Color previousColor = Color.Black;
        private Color currColor = Color.Black;
        private bool prev = false;
        public Form1()
        {
            InitializeComponent();
            GetSticks();
            Sticks = GetSticks();
            timer1.Enabled = true;
            g = panel1.CreateGraphics();
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            pen = new Pen(Color.Black, 5);
            pen.StartCap = pen.EndCap = System.Drawing.Drawing2D.LineCap.Round;
        }

        DirectInput Input = new DirectInput();
        SlimDX.DirectInput.Joystick stick;
        Joystick[] Sticks;

        bool mouseClicked = false;

        int yValue = 0;
        int xValue = 0;
        int zValue = 0;

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern void mouse_event(uint flag, uint _x, uint _y, uint btn, uint exInfo);
        private const int MOUSEEVENT_LEFTDOWN = 0x02;
        private const int MOUSEEVENT_LEFTUP = 0x04;
        
        public Joystick[] GetSticks()
        {
            List<SlimDX.DirectInput.Joystick> sticks = new List<SlimDX.DirectInput.Joystick>();
            foreach (DeviceInstance device in Input.GetDevices(DeviceClass.GameController, DeviceEnumerationFlags.AttachedOnly))
            {
                try
                {
                    stick = new SlimDX.DirectInput.Joystick(Input, device.InstanceGuid);
                    stick.Acquire();

                    foreach(DeviceObjectInstance deviceObject in stick.GetObjects())
                    {
                        if((deviceObject.ObjectType & ObjectDeviceType.Axis) != 0)
                        {
                            stick.GetObjectPropertiesById((int)deviceObject.ObjectType).SetRange(-100, 100);
                        }
                    }
                    sticks.Add(stick);
                }
                catch (DirectInputException)
                {
                    
                }
            }
            return sticks.ToArray();
        }

        void stickHandle(Joystick stick, int id)
        {
            JoystickState state = new JoystickState();
            state = stick.GetCurrentState();
           
            yValue = state.Y;
            xValue = state.X;
            zValue = state.Z;
            pen.Width = (float)((zValue + 101) * 0.2);
            MouseMove(xValue, yValue);

            bool[] buttons = state.GetButtons();

            if(id == 0)
            {
                if (buttons[1])
                {
                    panel1.BackColor = Color.White;
                    panel1.Refresh();
                }
                if (buttons[2])
                {
                    if (prev)
                    {
                        pen.Color = currentColor;
                        prev = false;
                    }
                    else
                    {
                        pen.Color = previousColor;
                        prev = true;
                    }

                    currColor = pen.Color;
                }
                if (buttons[0])
                {
                    if (mouseClicked == false)
                    {
                        mouse_event(MOUSEEVENT_LEFTDOWN, 0, 0, 0, 0);
                        mouseClicked = true;
                    }
                }
                else
                {
                    if(mouseClicked == true)
                    {
                        mouse_event(MOUSEEVENT_LEFTUP, 0, 0, 0, 0);
                        mouseClicked = false;
                    }
                }
            }
        }

        public void MouseMove (int posx, int posy)
        {
            Cursor.Position = new Point(Cursor.Position.X + posx/3, Cursor.Position.Y + posy/3);
            Cursor.Clip = new Rectangle(this.Location, this.Size);

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            for (int i = 0; i < Sticks.Length; i++)
            {
                stickHandle(Sticks[i], i);
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Joystick[] joystick = GetSticks();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            pictureBox1.BorderStyle = BorderStyle.None;
            pictureBox2.BorderStyle = BorderStyle.None;
            pictureBox3.BorderStyle = BorderStyle.None;
            pictureBox4.BorderStyle = BorderStyle.None;
            PictureBox p = (PictureBox) sender;
            pen.Color = p.BackColor;
            previousColor = currColor;
            currentColor = p.BackColor;
            if (p.BorderStyle == BorderStyle.FixedSingle)
            {
                p.BorderStyle = BorderStyle.None;
                pen.Color = Color.Black;
            }
            else
            {
                p.BorderStyle = BorderStyle.FixedSingle;
            }
        }

        private void panel1_MouseUp(object sender, MouseEventArgs e)
        {
            moving = false;
            x = -1;
            y = -1;
            panel1.Cursor = Cursors.Default;
        }

        private void panel1_MouseDown(object sender, MouseEventArgs e)
        {
            moving = true;
            x = e.X;
            y = e.Y;
            g.DrawLine(pen, new Point(x, y), new Point(x + 1, y));
            panel1.Cursor = Cursors.Cross;
        }

        private void panel1_MouseMove(object sender, MouseEventArgs e)
        {
            if (moving && x != -1 && y != -1)
            {
                g.DrawLine(pen, new Point(x, y), e.Location);
                x = e.X;
                y = e.Y;
            }
        }
    }
}
