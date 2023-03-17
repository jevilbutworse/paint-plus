using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace paint_
{
    public partial class drawing : Form
    {

        public static drawing instance;
        public Button btl1;
        Color colour = Color.Black;
        int size2 = 4;
        public drawing()
        {
            InitializeComponent();
            this.sizeScreen = this.Size;
            this.sizeRectangles = new Size(8, 8);
            instance = this;
            btl1 = clear;
        }

        bool drw;
        int beginX, beginY;
        Graphics g;
        private Size sizeScreen;
        private Size sizeRectangles;
        public string funy;
        int number2 = 1;

        private void drawing_MouseDown(object sender, MouseEventArgs e)
        {
            drw = true;
            beginX = e.X;
            beginY = e.Y;
        }

        private void drawing_MouseMove(object sender, MouseEventArgs e)
        {
            g = pictureBox1.CreateGraphics();
            Pen p = new Pen(colour, size2);
            Point point1 = new Point(beginX, beginY);
            Point point2 = new Point(e.X, e.Y);
            if (drw == true)
            {
                g.DrawLine(p, point1, point2);
                beginX = e.X;
                beginY = e.Y;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (funy == "ye")
            {
                pictureBox1.Image.Dispose();
                funy = "no";
            }
            g.Clear(Color.White);
        }

        private void save_Click(object sender, EventArgs e)
        {
            MessageBox.Show("save doesnt work yet lol, expect a black screen");
            var sfd = new SaveFileDialog();
            sfd.Filter = "Image(*.jpg)|*.jpg|(*.*)|*.*";
            if (sfd.ShowDialog()==DialogResult.OK)
            {
                this.Location = new Point(0, 0);
                Bitmap bmpScreenshot = Screenshot();
                g.CopyFromScreen(0, 0, 0, 0, pictureBox1.Size);
                bmpScreenshot.Save(sfd.FileName, ImageFormat.Jpeg);
            }
        }

        private void drawing_MouseUp(object sender, MouseEventArgs e)
        {
            drw = false;
        }
        private Bitmap Screenshot()
        {

            // This is where we will store a snapshot of the screen
            Bitmap bmpScreenshot =
                new Bitmap(pictureBox1.Width, pictureBox1.Height);

            // Creates a graphic object so we can draw the screen in the bitmap (bmpScreenshot);
            Graphics g = Graphics.FromImage(bmpScreenshot);

            // Copy from screen into the bitmap we created
            g.CopyFromScreen(0, 0, 0, 0, pictureBox1.Size);

            // Return the screenshot
            return bmpScreenshot;
        }

        public void clearshow()
        {
            btl1.Visible = true;
        }

        public void clearhide()
        {
            btl1.Visible = false;
        }

        public void changeColour(string colourchoose)
        {
            if (colourchoose == "Red")
            {
                colour = Color.Red;
                size2 = 4;
            }

            if (colourchoose == "Blue")
            {
                colour = Color.Blue;
                size2 = 4;
            }

            if (colourchoose == "Black")
            {
                colour = Color.Black;
                size2 = 4;
            }

            if (colourchoose == "Rubber")
            {
                colour = Color.White;
                size2 = 25;
            }

            if (colourchoose == "Orange")
            {
                colour = Color.Orange;
                size2 = 4;
            }

            if (colourchoose == "Green")
            {
                colour = Color.LightGreen;
                size2 = 4;
            }

            if (colourchoose == "Purple")
            {
                colour = Color.Purple;
                size2 = 4;
            }

            if (colourchoose == "Brown")
            {
                colour = ColorTranslator.FromHtml("#79443b");
                size2 = 4;
            }

            if (colourchoose == "Cyan")
            {
                colour = ColorTranslator.FromHtml("#B1F8F2");
                size2 = 4;
            }
        }

        private void drawing_FormClosing(object sender, FormClosingEventArgs e)
        {
            
        }

        public void disposeIMG()
        {
            if (funy == "ye")
            {
                pictureBox1.Image.Dispose();
                funy = "no";
            }
            g.Clear(Color.White);
            funy = "ye";
            pictureBox1.Load("bnk.png");
        }

        public void loadpanfile(Bitmap bitmap)
        {
            number2 = number2 + 1;
            if (funy == "ye")
            {
                pictureBox1.Image.Dispose();
                funy = "no";
            }
            g.Clear(Color.White);
            funy = "ye";
            bitmap.Save("tempfiles/temp" + number2 + ".png", ImageFormat.Png);
            pictureBox1.Load("tempfiles/temp" + number2 + ".png");
            bitmap.Dispose();
        }
    }
}