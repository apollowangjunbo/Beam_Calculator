using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace cal
{
    public partial class Form3 : Form
    {
        public Form3()
        {
            InitializeComponent();
        }
        private int num=0;
        public Form3(int a,int x, int y)
        {
            InitializeComponent();
            label2.Text = a.ToString();
            this.StartPosition = FormStartPosition.Manual;
            this.Location = new Point(x, y);
            num=a-1;
        }
        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text != "" && Form2.checkDouble(textBox1.Text))
            {
                if (Convert.ToDouble(textBox1.Text) <= 0)
                    MessageBox.Show("invalid input", "error");
                else
                {
                    Form1.layersE[num] = Convert.ToDouble(textBox1.Text);
                    this.Close();
                }
            }
            else MessageBox.Show("invalid input","error");
            
        }

        private void checkInput(object sender, KeyPressEventArgs e)
        {
            if (!(Char.IsNumber(e.KeyChar)) && e.KeyChar != (char)13 && e.KeyChar != (char)8 && e.KeyChar != '.')
            {
                e.Handled = true;
            }
        }

        private void Form3_Load(object sender, EventArgs e)
        {

        }
    }
    
}
