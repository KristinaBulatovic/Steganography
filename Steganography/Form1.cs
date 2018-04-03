using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Steganography
{
    public partial class Form1 : Form
    {
        SteganographyProjectClass spc;
        Form2 form2;
        string url = "";
        public Form1()
        {
            InitializeComponent();
            spc = new SteganographyProjectClass();
        }

        private void label1_Click(object sender, EventArgs e)
        {
            url = spc.OpenPicture();
            if (url != "")
            {
                pictureBox1.ImageLocation = url;
                pictureBox1.Visible = true;
            }
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            url = spc.OpenPicture();
            if (url != "")
            {
                pictureBox1.ImageLocation = url;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (url != "")
            {
                //string key = spc.Key(textBox1.Text);
                Bitmap img = new Bitmap (pictureBox1.Image);
                form2 = new Form2(img,textBox1.Text);
                form2.ShowDialog();
                textBox1.Text = form2.a;
                pictureBox1.ImageLocation = form2.a;
                pictureBox1.Visible = false;
                url = "";
            }
            else MessageBox.Show("No image to decode", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (url != "")
            {
                Bitmap img = new Bitmap(pictureBox1.Image);
                Color lpixel = img.GetPixel(img.Width - 1, img.Height - 1);
                int messlen = lpixel.B;
                string message = "";

                for (int i = 0; i < img.Width; i++)
                {
                    for (int j = 0; j < img.Height; j++)
                    {
                        Color pixel = img.GetPixel(i, j);
                        //RGB
                        if (i < 1 && j < messlen)
                        {
                            //Console.WriteLine("R[" + i + "][" + j + "] : " + pixel.R);
                            //Console.WriteLine("G[" + i + "][" + j + "] : " + pixel.G);
                            //Console.WriteLine("B[" + i + "][" + j + "] : " + pixel.B);
                            int value = pixel.B;
                            //Console.WriteLine("Value: " + value);
                            char c = Convert.ToChar(value);

                            //string letter = System.Text.Encoding.ASCII.GetString(new byte[] { Convert.ToByte(c) });
                            string letter = c.ToString();

                            message += letter;
                        }
                    }
                }
                //string key = spc.Key(textBox1.Text);
                string text = spc.Decrypt_TriplDES (textBox1.Text, message);
                if (text != "")
                {
                    Form3 form3 = new Form3(text);
                    form3.ShowDialog();
                    textBox1.Text = form3.a;
                    pictureBox1.ImageLocation = form3.a;
                    url = "";
                    pictureBox1.Visible = false;
                }
                else MessageBox.Show("Insert the right key!!!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else MessageBox.Show("No image to decode", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}
