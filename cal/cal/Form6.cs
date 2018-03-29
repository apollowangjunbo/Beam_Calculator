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
    public partial class Form6 : Form
    {
        public Form6(double[,] p, int x, int y)
        {
            InitializeComponent();
           /* this.StartPosition = FormStartPosition.Manual;
            this.Location = new Point(x, y);
           //找出最大的正应力放到t中
            double t;
            t=Math.Abs(p[0,0]);
            for (int i = 0; i < Form1.layerNum; i++)
                for (int j = 0; j < 2; j++)
                    if (Math.Abs(p[i, j]) > t)
                        t = Math.Abs(p[i, j]);

            //绘图
            double k = 10;
            Graphics pic1 = panel1.CreateGraphics();
            Pen p1 = new Pen(Color.Red, 2);
            p1.DashPattern = new float[] { 2, 1 };
            pic1.DrawLine(p1, 5, 100, 165, 100);
           /* Graphics pic1 = panel1.CreateGraphics();
            Pen p1 = new Pen(Color.Red, 2);
             pic1.DrawLine(p1, 3,50,6,90);
            //p1.DashPattern = new float[] { 2, 1 };
            /*for (int i = 0; i < Form1.layerNum;i++ )
                pic1.DrawLine(p1, (int)(82 + p[i, 0] / t * 75), (int)(k), (int)(82 + p[i, 1] / t * 75), (int)(k = k + Form1.layersHeight[i]));    */ 
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    /*    private void greatPainter1(object sender, PaintEventArgs e)
        {
            double t;
            t = Math.Abs(Form1.P[0, 0]);
            for (int i = 0; i < Form1.layerNum; i++)
                for (int j = 0; j < 2; j++)
                    if (Math.Abs(Form1.P[i, j]) > t)
                        t = Math.Abs(Form1.P[i, j]);

            double k = 10;
            Graphics pic1 = panel1.CreateGraphics();
            Pen p1 = new Pen(Color.Red, 1);
            p1.DashPattern = new float[] { 2, 1 };
            for (int i = 0; i < Form1.layerNum; i++)
                pic1.DrawLine(p1, (int)(82 + Form1.P[i, 0] / t * 75), (int)(k), (int)(82 + Form1.P[i, 1] / t * 75), (int)(k = k + Form1.layersHeight[i]));
        }*/
        private void Form6_Load(object sender, EventArgs e)
        {

        }
    }
}
