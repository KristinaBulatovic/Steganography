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
    public partial class Form2 : Form
    {
        SteganographyProjectClass spc;
        Bitmap img;
        string key;
        public string a { get; set; }
        public Form2(Bitmap image, string k)
        {
            InitializeComponent();
            spc = new SteganographyProjectClass();
            img = new Bitmap(image);
            key = k;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string text = spc.Encrypt_TriplDES(key, textBox1.Text);
            for (int i = 0; i < img.Width; i++)
            {
                for (int j = 0; j < img.Height; j++)
                {
                    Color pixel = img.GetPixel(i, j);
                    //RGB
                    if (i < 1 && j < text.Length)
                    {
                        //Console.WriteLine("R[" + i + "][" + j + "] : " + pixel.R);
                        //Console.WriteLine("G[" + i + "][" + j + "] : " + pixel.G);
                        //Console.WriteLine("B[" + i + "][" + j + "] : " + pixel.B);
                        char letter = Convert.ToChar(text.Substring(j, 1));
                        int value = Convert.ToInt32(letter);
                        //Console.WriteLine("Letter: " + letter + "\n Value: " + value);
                        img.SetPixel(i, j, Color.FromArgb(pixel.R, pixel.G, value));
                    }
                    if (i == img.Width - 1 && j == img.Height - 1)
                    {
                        img.SetPixel(i, j, Color.FromArgb(pixel.R, pixel.G, text.Length));
                    }
                }
            }

            spc.SavePicture(img);
            a = null;
            Close();
        }
    }
}
