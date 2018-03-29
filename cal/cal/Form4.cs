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
    public partial class Form4 : Form
    {
        public Form4()
        {
            InitializeComponent();
        }
        public Form4(double[,] p,int x,int y,double[] I)
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.Manual;
            this.Location = new Point(x, y);
            for (int i = 0; i < Form1.layerNum; i++)
            {
                ListViewItem lv = new ListViewItem();
                lv.SubItems[0].Text = (i + 1).ToString();
                lv.SubItems.Add(p[i, 0].ToString());
                lv.SubItems.Add(p[i, 1].ToString());
                lv.SubItems.Add(I[i].ToString());
                listView1.Items.Add(lv);
            }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Form4_Load(object sender, EventArgs e)
        {

        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
