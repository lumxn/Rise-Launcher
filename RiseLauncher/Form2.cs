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

namespace RiseLauncher
{
    public partial class Form2 : Form
    {

        [DllImport("Gdi32.dll", EntryPoint = "CreateRoundRectRgn")]
        private static extern IntPtr CreateRoundRectRgn
      (
          int nLeftRect,     
          int nTopRect,      
          int nRightRect, 
          int nBottomRect,  
          int nWidthEllipse, 
          int nHeightEllipse 
      );

        public Point mouseLocation;
        public string SelectedFolderPath { get; private set; }
        public Form2()
        {
            InitializeComponent();
            this.FormBorderStyle = FormBorderStyle.None;
            Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, Width, Height, 80, 80));
            button1.Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, button1.Width, button1.Height, 20, 20));
            button3.Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, button3.Width, button3.Height, 20, 20));
            button2.Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, button2.Width, button2.Height, 20, 20));
            button4.Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, button4.Width, button4.Height, 20, 20));
            button5.Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, button5.Width, button5.Height, 20, 20));

        }

        private void button1_Click(object sender, EventArgs e)
        {
            new Form1().Show();
            this.Hide();
        }

        private void Form2_Load(object sender, EventArgs e)
        {

        }

        private void mouse_Down(object sender, MouseEventArgs e)
        {
            mouseLocation = new Point(e.X, e.Y);
        }

        private void mouse_Move(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                Point mousePoint = Control.MousePosition;
                mousePoint.Offset(-mouseLocation.X, -mouseLocation.Y);
                Location = mousePoint;
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            button3.Hide();
            button2.Hide();
            button4.Hide();
            button5.Hide();
            label2.Show();
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            button3.Hide();
            button2.Hide();
            button4.Hide();
            button5.Hide();
            label3.Show();
        }

        private void button4_Click_1(object sender, EventArgs e)
        {
            button3.Hide();
            button2.Hide();
            button4.Hide();
            button5.Hide();
            label4.Show();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            button3.Hide();
            button2.Hide();
            button4.Hide();
            button5.Hide();
            label5.Show();
        }
    }
}