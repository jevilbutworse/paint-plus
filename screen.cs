using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace paint_
{
    public partial class screen : Form
    {
        public static screen instance;
        public drawing insideForm = new drawing();
        public screen()
        {
            InitializeComponent();
            instance = this;
        }

        Graphics g;

        private void screen_Load(object sender, EventArgs e)
        {
            Directory.CreateDirectory("tempfiles/");
            File.SetAttributes("tempfiles/", FileAttributes.Normal);
            insideForm.TopLevel = false;
            frmContainer.Controls.Add(insideForm);
            insideForm.Show();
            this.StartPosition = FormStartPosition.Manual;
            this.Location = new Point(-10, 0);
        }

        private void saveToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            // create new dialog
            var sfd = new SaveFileDialog();
            sfd.Filter = "Image(*.jpg)|*.jpg|(*.*)|*.*";
            if (sfd.ShowDialog() == DialogResult.OK)
            {
                // create new window point and hide everything
                this.Location = new Point(-10, 0);
                label1.Visible = false;
                label2.Visible = false;
                label3.Visible = false;
                menu.Visible = false;
                this.FormBorderStyle = FormBorderStyle.None;

                // Take screenshot using function.
                Bitmap bmpScreenshot = Screenshot();
                this.BackgroundImage = bmpScreenshot;
                // Save screen shot as file name, and jpeg
                bmpScreenshot.Save(sfd.FileName, ImageFormat.Jpeg);

                // Turn all objects visible again
                label1.Visible = true;
                label2.Visible = true;
                label3.Visible = true;
                menu.Visible = true;
                this.FormBorderStyle = FormBorderStyle.Sizable;
            }
            // END.
        }

        private Bitmap Screenshot()
        {

            // This is where we will store a snapshot of the screen
            Bitmap bmpScreenshot =
                new Bitmap(frmContainer.Width, frmContainer.Height);

            // Creates a graphic object so we can draw the screen in the bitmap (bmpScreenshot);
            Graphics g = Graphics.FromImage(bmpScreenshot);

            // Copy from screen into the bitmap we created
            g.CopyFromScreen(0, 0, 0, 0, frmContainer.Size);

            // Return the screenshot
            return bmpScreenshot;
        }

        // COLOUR AREA

        private void redToolStripMenuItem_Click(object sender, EventArgs e)
        {
            insideForm.changeColour("Red");
        }

        private void blueToolStripMenuItem_Click(object sender, EventArgs e)
        {
            insideForm.changeColour("Blue");
        }

        private void blackToolStripMenuItem_Click(object sender, EventArgs e)
        {
            insideForm.changeColour("Black");
        }

        private void eraserToolStripMenuItem_Click(object sender, EventArgs e)
        {
            insideForm.changeColour("Rubber");
        }

        private void orangeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            insideForm.changeColour("Orange");
        }

        private void greenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            insideForm.changeColour("Green");
        }

        private void purpleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            insideForm.changeColour("Purple");
        }

        private void brownToolStripMenuItem_Click(object sender, EventArgs e)
        {
            insideForm.changeColour("Brown");
        }

        private void cyanToolStripMenuItem_Click(object sender, EventArgs e)
        {
            insideForm.changeColour("Cyan");
        }

        // Form closing area

        private void screen_FormClosing(object sender, FormClosingEventArgs e)
        {

            
            // show dialog
            DialogResult dialogResult = MessageBox.Show("Save drawing before closing?", "Save Drawing", MessageBoxButtons.YesNo);

            if (dialogResult == DialogResult.No)
            {
                insideForm.disposeIMG();
                try
                {
                    var dir = new DirectoryInfo("tempfiles/");
                    dir.Attributes = dir.Attributes & ~FileAttributes.ReadOnly;
                    dir.Delete(true);
                }
                catch (IOException ex)
                {
                    MessageBox.Show(ex.Message);
                }
                // end program (NO CODE NEEDED)
            }

            if (dialogResult == DialogResult.Yes)
            {
                // show dialog to save, saves then exits
                var sfd = new SaveFileDialog();
                sfd.Filter = "Paint+ File (*.panfile)|*.panfile";
                if (sfd.ShowDialog() == DialogResult.OK)
                {

                    this.Location = new Point(-10, 0);
                    label1.Visible = false;
                    label2.Visible = false;
                    label3.Visible = false;
                    menu.Visible = false;
                    this.FormBorderStyle = FormBorderStyle.None;

                    Bitmap bmpScreenshot = Screenshot();
                    this.BackgroundImage = bmpScreenshot;
                    bmpScreenshot.Save(sfd.FileName, ImageFormat.Png);
                    Path.ChangeExtension(sfd.FileName, sfd.Filter);


                    label1.Visible = true;
                    label2.Visible = true;
                    label3.Visible = true;
                    menu.Visible = true;
                    this.FormBorderStyle = FormBorderStyle.Sizable;
                }

                insideForm.disposeIMG();
                try
                {
                    var dir = new DirectoryInfo("tempfiles/");
                    dir.Attributes = dir.Attributes & ~FileAttributes.ReadOnly;
                    dir.Delete(true);
                }
                catch (IOException ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // show dialog to save, saves then exits
            var sfd = new SaveFileDialog();
            sfd.Filter = "PNG File (*.png)|*.png|Paint+ File (*.panfile)|*.panfile|(*.*)|*.*";
            if (sfd.ShowDialog() == DialogResult.OK)
            {

                this.Location = new Point(-10, 0);
                label1.Visible = false;
                label2.Visible = false;
                label3.Visible = false;
                menu.Visible = false;
                this.FormBorderStyle = FormBorderStyle.None;

                Bitmap bmpScreenshot = Screenshot();
                this.BackgroundImage = bmpScreenshot;
                bmpScreenshot.Save(sfd.FileName, ImageFormat.Png);
                Path.ChangeExtension(sfd.FileName, sfd.Filter);


                label1.Visible = true;
                label2.Visible = true;
                label3.Visible = true;
                menu.Visible = true;
                this.FormBorderStyle = FormBorderStyle.Sizable;
            }
        }

        private void loadToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var timetime = new OpenFileDialog();
            timetime.Filter = "PNG File (*.png)|*.png|Paint+ File (*.panfile)|*.panfile|(*.*)|*.*";
            if (timetime.ShowDialog() == DialogResult.OK)
            {

                insideForm.loadpanfile(new Bitmap(timetime.FileName));
            }
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var about = new About();
            about.ShowDialog();
        }

        private void clearToolStripMenuItem_Click(object sender, EventArgs e)
        {
            insideForm.clear();
        }
    }
}
