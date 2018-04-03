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
    public partial class Form3 : Form
    {
        public string a;
        private string A
        {
            get  { return a; }
            set { a = null; }
        }
        public Form3(string message)
        {
            InitializeComponent();
            textBox1.Text = message;
        }
    }
}
