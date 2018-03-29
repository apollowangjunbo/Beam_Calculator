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
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        public static int layerNum=0;
        public static double[] layersHeight;
        public static double layerWidth = 0;
        public static double[] layersE;
        public static bool cls=false;
        public static double layersM = 1;
        public static string shape = "";
        public static double Yc = 0;
   
        public Point pic1 = new Point(250, 40);
        public Point pic2 = new Point(450, 40);
        public int ii = 0;
        private void button1_Click(object sender, EventArgs e)
        {
            Form2 f2 = new Form2(new Point(this.Location.X+50,this.Location.Y+50));
            f2.ShowDialog();
            //MessageBox.Show(" ");
        }
        
        private void button4_Click(object sender, EventArgs e)
        {
            Console.WriteLine("layerNum="+layerNum.ToString());
            Console.WriteLine("layerWidth="+layerWidth.ToString());
            Console.WriteLine("layersHeight:");
            for (int i = 0; i < layerNum; i++)
                Console.Write(layersHeight[i].ToString() + " ");
            Console.WriteLine();
            Console.WriteLine("layersE:");
            for (int i = 0; i < layerNum; i++)
                Console.Write(layersE[i].ToString() + " ");
            Console.WriteLine();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (layerNum == 0) MessageBox.Show("请先进行截面几何性质输入", "error");
            for (int i = 0; i < layerNum; i++)
            {
                Form3 f3 = new Form3(i + 1,this.Location.X+50,this.Location.Y+50);
               
                f3.ShowDialog();
            }
        }

        private void close(object sender, EventArgs e)
        {
                this.Close();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (layersE[0] == 0) MessageBox.Show("请先输入数据", "error");
            else
            {
                string str = "";
                for (int i = 1; i <= layerNum; i++)
                {
                    str += "第" + i.ToString() + "层的弹性模量是" + layersE[i - 1].ToString() + "GPa\n";
                }
                MessageBox.Show(str, "查看数据");
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            layerNum = 0;
            shape = "";
            layersE = new double[0];
            layersHeight = new double[0];
            layerWidth = 0;
            layersM = 1;
            
            Graphics pic1 = panel1.CreateGraphics();
            Graphics pic2 = panel2.CreateGraphics();
            Graphics pic3 = panel7.CreateGraphics();
            pic1.Clear(Color.White);
            pic2.Clear(Color.White);
            pic3.Clear(Color.White);
            MessageBox.Show("已经初始化", "信息");
        }

        private void help(object sender, EventArgs e)
        {
            string str = "";
            str += "欢迎使用复合梁强度计算器\n";
            str += "操作步骤如下：\n";
            str += "I.输入截面几何性质数据\n";
            str += "II.输入材料力学性质数据\n";
            str += "III.点击计算后得到输出结果\n";
            str += "如需第二次计算点击初始化后重复以上操作\n";
            str += "如需退出请直接点击右上角关闭按钮，或菜单栏退出按钮\n";
            MessageBox.Show(str, "简易帮助");
        }

        private void tryAgain(object sender, FormClosingEventArgs e)
        {
            if (cls == false)
            {
                closeWindow cw = new closeWindow(this.Location);
                cw.ShowDialog();
            }
            if (cls == false) e.Cancel = true;
            else e.Cancel = false;
        }
        private void button3_Click(object sender, EventArgs e)
        {
            if (layerNum == 0)
                MessageBox.Show("请先输入截面几何性质数据", "error");
            //
            //圆形截面
            //
            else if(shape=="圆形截面")
            {

                Form5 inputm = new Form5(this.Location.X + 50, this.Location.Y + 50);
                inputm.ShowDialog();
               
                //b——截面宽                                  y——截面形心位置（以第一个截面上面为坐标原点）
                //h——截面高                                  Yc——整个等效截面图的形心
                //E——截面弹性模量                            Sq——形心坐标与面积的积的和
                //S——截面面积                                S1——总面积
                //I——截面抗弯截面系数
                //输入变量
                double[] h = new double[layerNum];
               //中间变量
                double [] Mi=new double [layerNum ],EI=new double[layerNum];
                double EIz=0;
                //输出变量
                double[] I = new double[layerNum];
                double[,] P = new double[layerNum, 2];
                double[,] Q = new double[layerNum, 2];

                //排序
               double t=0;
                for(int j=0;j<layerNum-1;j++)
                   for(int i=0;i<layerNum-1;i++)
                      if(layersHeight[i]>layersHeight[i+1])
                      { t = layersHeight[i]; layersHeight[i] = layersHeight[i + 1]; layersHeight[i + 1] = t;
                      t = layersE[i]; layersE[i] = layersE[i + 1]; layersE[i + 1] = t;
                      }
                //计算等截面抗弯界面系数
                I[0] = 3.1415926 / 64 * (layersHeight[0] *layersHeight[0] *layersHeight[0] *layersHeight[0] );
                for (int i = 1; i < layerNum; i++)
                    I[i] = 3.1415926 / 64 * (Math.Pow(layersHeight[i], 4) - Math.Pow(layersHeight[i-1], 4));
                //计算每个截面的EI
                for (int i = 0; i < layerNum; i++)
                {
                    EI[i] = layersE[i] * I[i];
                    EIz=EIz+EI[i];}
                //计算每个截面的Mi
                for (int k = 0; k < layerNum; k++)
                
                    Mi[k]=layersM/EIz*EI[k];
                
                //正应力计算
                for (int i = 0; i < layerNum; i++)
                {
                    P[i, 0] = Mi[i] * layersHeight[i] / 2 / I[i] / 1000000;
                    P[i, 1] = -Mi[i] * layersHeight[i] / 2 / I[i] / 1000000;
                }
                    Q[0, 0] = 0;
                    Q[0, 1] = 0;
                for (int i = 1; i < layerNum; i++)
                {
                    Q[i, 0] = Mi[i] * layersHeight[i-1] / 2 / I[i] / 1000000;
                    Q[i, 1] = -Mi[i] * layersHeight[i-1] / 2 / I[i] / 1000000;
                }
                //输出图形
                int temp2 = 0;
                int temp3 = 0;
                Graphics pic1 = panel1.CreateGraphics();
                pic1.Clear(Color.White);
                for (ii = 0; ii < layerNum; ii++)
                {
                    int temp = (int)(150 * layersHeight[layerNum-ii-1] / layersHeight[layerNum - 1]);
                    temp2 = 85 - temp/2;
                    temp3 = 100 - temp/2;
                    switch (ii % 5)
                    {
                        case 0:
                            pic1.FillEllipse(Brushes.DarkGray, temp2, temp3, temp, temp); break;
                        case 1:
                            pic1.FillEllipse(Brushes.LimeGreen,temp2, temp3, temp, temp); break;
                        case 2:
                            pic1.FillEllipse(Brushes.Blue, temp2, temp3, temp, temp); break;
                        case 3:
                            pic1.FillEllipse(Brushes.Orchid, temp2, temp3, temp, temp); break;
                        case 4:
                            pic1.FillEllipse(Brushes.MediumPurple,temp2, temp3, temp, temp); break;
                        default:
                            break;
                    }
                }
                Pen p = new Pen(Color.Red, 2);
                p.DashPattern = new float[] { 2, 1 };
                pic1.DrawLine(p, 5, 100, 165, 100);



                //绘图
                {
                    double t1;
                    t1 = Math.Abs(P[0, 0]);
                    for (int i = 0; i < Form1.layerNum; i++)
                        
                            if (Math.Abs(P[i, 0]) > t1)
                                t1 = Math.Abs(P[i, 0]);

                    double k = 100;
                    Graphics pic3 = panel7.CreateGraphics();
                    pic3.Clear(Color.White);
                    Pen p1 = new Pen(Color.Black, 2);
                    Pen p4 = new Pen(Color.Black, 1);
                    //坐标轴
                    pic3.DrawLine(p1, 82, 5, 82, 195);
                    pic3.DrawLine(p4, 5, 100, 165, 100);

                    //折线图
                    Pen p2 = new Pen(Color.Blue, 3);
                  
                    //填充颜色
                        k = 100;
                        for (int i = 0; i < Form1.layerNum; i++)
                        {
                            Point[] sbx = new Point[]{
                new Point(82,  (int)(k)),
                new Point((int)(82 + Q[i, 1] / t1 * 75),(int)(k)),
                new Point( (int)(82 + P[i, 1] / t1 * 75), (int)(k = 100- 90 *layersHeight[i] / layersHeight[layerNum -1])),
                new Point(82,  (int)(k)),
                
                                                      };
                            pic3.FillPolygon(Brushes.Aquamarine, sbx);
                        }
                    
                    //填充颜色
                    k = 100;
                    for (int i = 0; i < Form1.layerNum; i++)
                    {
                        Point[] sbx = new Point[]{
                new Point(82,  (int)(k)),
                new Point((int)(82 + Q[i, 0] / t1 * 75),(int)(k)),
                new Point( (int)(82 + P[i, 0] / t1 * 75), (int)(k = 100+ 90 *layersHeight[i] / layersHeight[layerNum -1])),
                new Point(82,  (int)(k)),
                
                                                      };
                        pic3.FillPolygon(Brushes.Aquamarine, sbx);
                    }
                    //折线
                    k = 100;
                    for (int i = 0; i < layerNum; i++)
                    {
                        pic3.DrawLine(p2, (int)(82 + Q[i, 1] / t1 * 75), (int)(k), (int)(82 + P[i, 1] / t1 * 75), (int)(k = 100 - 90 * layersHeight[i] / layersHeight[layerNum - 1]));
                    }
                    k = 100;
                    for (int i = 0; i < layerNum; i++)
                        pic3.DrawLine(p2, (int)(82 + Q[i, 0] / t1 * 75), (int)(k), (int)(82 + P[i, 0] / t1 * 75), (int)(k = 100 + 90 * layersHeight[i] / layersHeight[layerNum - 1]));
                    Pen p5 = new Pen(Color.Black, 2);
                    p5.DashPattern = new float[] { 2, 2 };
                    k = 100-90 *layersHeight[0] / layersHeight[layerNum -1];
                    for (int i = 0; i < layerNum - 1; i++)
                    {
                        pic3.DrawLine(p5, (int)(82 + P[i, 1] / t1 * 75), (int)(k), (int)(82 + Q[i + 1, 1] / t1 * 75), (int)(k));
                        k = 100  - 90 * layersHeight[i+1] / layersHeight[layerNum - 1];
                    }
                    k = 100 + 90 * layersHeight[0] / layersHeight[layerNum - 1];
                    for (int i = 0; i < layerNum - 1; i++)
                    {
                        pic3.DrawLine(p5, (int)(82 + P[i, 0] / t1 * 75), (int)(k), (int)(82 + Q[i + 1, 0] / t1 * 75), (int)(k));
                        k = 100+ 90 * layersHeight[i + 1] / layersHeight[layerNum - 1];
                    }
                    pic3.DrawLine(p5, (int)(82 + P[layerNum - 1, 1] / t1 * 75), 10, 82, 10);
                    pic3.DrawLine(p5, (int)(82 - P[layerNum - 1, 1] / t1 * 75), 190, 82, 190);


                    Font font = new Font("宋体", 9f);
                    PointF pointF = new PointF(82, 10);
                    PointF pointF1 = new PointF(105, 10);
                    PointF pointF2 = new PointF(135, 10);

                    pic3.DrawString("Yc=", font, Brushes.Black, pointF);
                    pic3.DrawString(Math.Round(layersHeight[layerNum-1 ]/2 , 3).ToString(), font, Brushes.Black, pointF1);
                    pic3.DrawString("(m)", font, Brushes.Black, pointF2);
                    /*

                    p1.DashPattern = new float[] { 2, 2 };
                    k = 10 + 180 * Form1.layersHeight[0] / layersHeight[layerNum - 1];
                    for (int i = 0; i < Form1.layerNum - 1; i++)
                    {
                        pic3.DrawLine(p1, (int)(82 + P[i, 1] / t * 75), (int)(k), (int)(82 + P[i + 1, 0] / t * 75), (int)(k));
                        k = k + 180 * Form1.layersHeight[i] / layersHeight[layerNum - 1];
                    }
                    pic3.DrawLine(p1, (int)(82 + P[0, 0] / t * 75), 10, 82, 10);
                    pic3.DrawLine(p1, (int)(82 + P[layerNum - 1, 1] / t * 75), 190, 82, 190);
                    //填充颜色
                    k = 10;
                    for (int i = 0; i < Form1.layerNum; i++)
                    {
                        Point[] sbx = new Point[]{
                new Point((int)(82 + P[i, 0] / t * 75),(int)(k)),
                new Point( (int)(82 + P[i, 1] / t * 75), (int)(k = k + 180 *layersHeight[i] / layersHeight[layerNum -1])),
                new Point(82,  (int)(k)),
                new Point(82, (int)(k - 180 *layersHeight[i] /layersHeight[layerNum -1]))
                                          };
                        pic3.FillPolygon(Brushes.Aquamarine, sbx);
                    }*/
                }



                //输出正应力值
                Form4 WindowsForShow = new Form4(P, this.Location.X, this.Location.Y,I);
                WindowsForShow.Show();
            }
            
            
            
            
            
            
            
            
            
            
            
            
            
            
            
            
            
            
            //矩形截面
            else
            {

                Form5 inputm = new Form5(this.Location.X + 50, this.Location.Y + 50);
                inputm.ShowDialog();
                int posOfEm = 0;
                for (int i = 1; i < layerNum; i++)
                {
                    if (layersE[i] >= layersE[posOfEm]) posOfEm = i;
                }
                //b——截面宽                                  y——截面形心位置（以第一个截面上面为坐标原点）
                //h——截面高                                  Yc——整个等效截面图的形心
                //E——截面弹性模量                            Sq——形心坐标与面积的积的和
                //S——截面面积                                S1——总面积
                //I——截面抗弯截面系数
                //输入变量
                double[] b = new double[layerNum];
                //中间变量
                double[] S = new double[layerNum], y = new double[layerNum];
                double Sq = 0, S1 = 0;
                //输出变量
                double[] I = new double[layerNum];
                double[,] P = new double[layerNum, 2];
                double Ic = 0;

                //计算等效截面宽
                for (int i = 0; i < layerNum; i++)
                    b[i] = layerWidth * layersE[i] / layersE[posOfEm];
                //计算每个截面的形心位置
                y[0] = 0.5 * layersHeight[0];
                for (int j = 1; j < layerNum; j++)
                    y[j] = 0.5 * layersHeight[j] + y[j - 1] + 0.5 * layersHeight[j - 1];
                //计算每个截面的面积
                for (int k = 0; k < layerNum; k++)
                {
                    S[k] = b[k] * layersHeight[k];
                    Sq = Sq + S[k];
                    S1 = S1 + S[k] * y[k];
                }
                //计算中性面y坐标
                Yc = S1 / Sq;
                //计算每个截面的抗弯界面系数
                for (int i = 0; i < layerNum; i++)
                {
                    I[i] = b[i] * layersHeight[i] * layersHeight[i] * layersHeight[i] / 12 + S[i] * (y[i] - Yc) * (y[i] - Yc);
                    Ic = Ic + I[i];
                }
                //正应力计算
                for (int i = 0; i < layerNum; i++)
                {
                    P[i, 0] = layersE[i] / layersE[posOfEm] * layersM * (y[i] - 0.5 * layersHeight[i] - Yc) / Ic / 1000000;
                    P[i, 1] = layersE[i] / layersE[posOfEm] * layersM * (y[i] + 0.5 * layersHeight[i] - Yc) / Ic / 1000000;
                }
                //输出图形



                int temp2 = 0;
                Graphics pic1 = panel1.CreateGraphics();
                pic1.Clear(Color.White);
                for (ii = 0; ii < layerNum; ii++)
                {
                    int temp = (int)(180 * layersHeight[ii] / (y[layerNum - 1] + layersHeight[layerNum - 1] / 2));
                    Rectangle rct = new Rectangle(10, 10 + temp2, 150, temp);
                    temp2 += temp;
                    switch (ii % 5)
                    {
                        case 0:
                            pic1.FillRectangle(Brushes.DarkGray, rct); break;
                        case 1:
                            pic1.FillRectangle(Brushes.LimeGreen, rct); break;
                        case 2:
                            pic1.FillRectangle(Brushes.Blue, rct); break;
                        case 3:
                            pic1.FillRectangle(Brushes.Orchid, rct); break;
                        case 4:
                            pic1.FillRectangle(Brushes.MediumPurple, rct); break;
                        default:
                            break;
                    }
                }
                Graphics pic2 = panel2.CreateGraphics();
                pic2.Clear(Color.White);
                temp2 = 0;
                for (ii = 0; ii < layerNum; ii++)
                {
                    int temp = (int)(180 * layersHeight[ii] / (y[layerNum - 1] + layersHeight[layerNum - 1] / 2));
                    Rectangle rct = new Rectangle(82 - (int)(75 * layersE[ii] / layersE[posOfEm]), 10 + temp2, (int)(150 * layersE[ii] / layersE[posOfEm]), temp);
                    temp2 += temp;
                    switch (ii % 5)
                    {
                        case 0:
                            pic2.FillRectangle(Brushes.DarkGray, rct); break;
                        case 1:
                            pic2.FillRectangle(Brushes.LimeGreen, rct); break;
                        case 2:
                            pic2.FillRectangle(Brushes.Blue, rct); break;
                        case 3:
                            pic2.FillRectangle(Brushes.Orchid, rct); break;
                        case 4:
                            pic2.FillRectangle(Brushes.MediumPurple, rct); break;
                        default:
                            break;
                    }

                }
                Pen p = new Pen(Color.Red, 2);
                p.DashPattern = new float[] { 2, 1 };
                pic2.DrawLine(p, 5, (int)(180 * Yc / (y[layerNum - 1] + layersHeight[layerNum - 1] / 2)) + 10, 165, (int)(180 * Yc / (y[layerNum - 1] + layersHeight[layerNum - 1] / 2)) + 10);
                //输出正应力值
                Form4 WindowsForShow = new Form4(P, this.Location.X, this.Location.Y, I);
                WindowsForShow.Show();


                //绘图
            {  double t;
            t=Math.Abs(P[0,0]);
            for (int i = 0; i < Form1.layerNum; i++)
                for (int j = 0; j < 2; j++)
                    if (Math.Abs(P[i, j]) > t)
                        t = Math.Abs(P[i, j]);

            double k = 10;
            Graphics pic3 = panel7.CreateGraphics();
            pic3.Clear(Color.White);
            Pen p1 = new Pen(Color.Black,2);
            Pen p4= new Pen(Color.Black,1);
                //坐标轴
            pic3.DrawLine(p1, 82 , 5, 82,195);
            pic3.DrawLine(p4, 5, (int)(180 * Yc / (y[layerNum - 1] + layersHeight[layerNum - 1] / 2)) + 10, 165, (int)(180 * Yc / (y[layerNum - 1] + layersHeight[layerNum - 1] / 2)) + 10);
               
                //折线图
            Pen p2 = new Pen(Color.Blue,3);
            for (int i = 0; i < Form1.layerNum;i++ )
                pic3.DrawLine(p2, (int)(82 + P[i, 0] / t * 75), (int)(k), (int)(82 + P[i, 1] / t * 75), (int)(k = (k + 180 * Form1.layersHeight[i] / (y[layerNum - 1] + layersHeight[layerNum - 1] / 2))));
           
                    
                p1.DashPattern = new float[] { 2, 2 };
            k = 10 + 180 * Form1.layersHeight[0] / (y[layerNum - 1] + layersHeight[layerNum - 1] / 2);
            for (int i =0; i < Form1.layerNum-1; i++)
                {
                pic3.DrawLine(p1, (int)(82 + P[i, 1] / t * 75), (int)(k), (int)(82 + P[i+1, 0] / t * 75), (int)(k));
                k = k + 180 * Form1.layersHeight[i] / (y[layerNum - 1] + layersHeight[layerNum - 1] / 2);
                }
            pic3.DrawLine(p1, (int)(82 + P[0,0] / t * 75), 10, 82, 10);
            pic3.DrawLine(p1, (int)(82 + P[layerNum-1 , 1] / t * 75), 190, 82, 190);
               //填充颜色
            k = 10;
            for (int i = 0; i < Form1.layerNum; i++)
            {
                Point[] sbx = new Point[]{
                new Point((int)(82 + P[i, 0] / t * 75),(int)(k)),
                new Point( (int)(82 + P[i, 1] / t * 75), (int)(k = (k + 180 *layersHeight[i] / (y[layerNum - 1] + layersHeight[layerNum - 1] / 2)))),
                new Point(82,  (int)(k)),
                new Point(82, (int)(k - 180 *layersHeight[i] / (y[layerNum - 1] + layersHeight[layerNum - 1] / 2)))
                                          };
                pic3.FillPolygon(Brushes.Aquamarine, sbx);



                //形心轴位置


                
                Font font = new Font("宋体", 9f);
                PointF pointF = new PointF(82, 10);
                PointF pointF1 = new PointF(105, 10);
                PointF pointF2 = new PointF(135, 10);
               
                pic3.DrawString("Yc=", font, Brushes.Black, pointF);
                pic3.DrawString(Math.Round(Yc,3).ToString()  , font, Brushes.Black, pointF1);
                pic3.DrawString("(m)", font, Brushes.Black, pointF2);

            }
            }
                /* Form6 WindowsForShow1 = new Form6(P, this.Location.X + 50, this.Location.Y + 200);
                 WindowsForShow1.Show();*/
            }
        }

        private void greatPainter1(object sender, PaintEventArgs e)
        {
            if (shape == "圆形截面")
            {
                int temp2 = 0;
                int temp3 = 0;
                Graphics pic1 = panel1.CreateGraphics();
               
                for (ii = 0; ii < layerNum; ii++)
                {
                    int temp = (int)(150 * layersHeight[layerNum - ii - 1] / layersHeight[layerNum - 1]);
                    temp2 = 85 - temp / 2;
                    temp3 = 100 - temp / 2;
                    switch (ii % 5)
                    {
                        case 0:
                            pic1.FillEllipse(Brushes.DarkGray, temp2, temp3, temp, temp); break;
                        case 1:
                            pic1.FillEllipse(Brushes.LimeGreen, temp2, temp3, temp, temp); break;
                        case 2:
                            pic1.FillEllipse(Brushes.Blue, temp2, temp3, temp, temp); break;
                        case 3:
                            pic1.FillEllipse(Brushes.Orchid, temp2, temp3, temp, temp); break;
                        case 4:
                            pic1.FillEllipse(Brushes.MediumPurple, temp2, temp3, temp, temp); break;
                        default:
                            break;
                    }
                }
                Pen p = new Pen(Color.Red, 2);
                p.DashPattern = new float[] { 2, 1 };
                pic1.DrawLine(p, 5, 100, 165, 100);
            }
                //
                //矩形截面
                //
            else
            {
                if (layerNum > 0)
                {
                    int temp2 = 0;
                    Graphics pic1 = panel1.CreateGraphics();
                    
                    double[] y = new double[layerNum];
                    y[0] = 0.5 * layersHeight[0];
                    for (int j = 1; j < layerNum; j++)
                        y[j] = 0.5 * layersHeight[j] + y[j - 1] + 0.5 * layersHeight[j - 1];
                    for (ii = 0; ii < layerNum; ii++)
                    {
                        int temp = (int)(180 * layersHeight[ii] / (y[layerNum - 1] + layersHeight[layerNum - 1] / 2));
                        Rectangle rct = new Rectangle(10, 10 + temp2, 150, temp);
                        temp2 += temp;
                        switch (ii % 5)
                        {
                            case 0:
                                pic1.FillRectangle(Brushes.DarkGray, rct); break;
                            case 1:
                                pic1.FillRectangle(Brushes.LimeGreen, rct); break;
                            case 2:
                                pic1.FillRectangle(Brushes.Blue, rct); break;
                            case 3:
                                pic1.FillRectangle(Brushes.Orchid, rct); break;
                            case 4:
                                pic1.FillRectangle(Brushes.MediumPurple, rct); break;
                            default:
                                break;
                        }
                    }
                }
            }
        }

        private void greatPainter2(object sender, PaintEventArgs e)
        {
            if (shape == "圆形截面") 
            {Graphics pic2 = panel2.CreateGraphics();
            pic2.Clear(Color.White);
            }
            else
            {
                if (layerNum > 0 && layersE[0] != 0)
                {
                    Graphics pic2 = panel2.CreateGraphics();
                    int temp2 = 0;
                    double[] y = new double[layerNum];
                    y[0] = 0.5 * layersHeight[0];
                    int posOfEm = 0;
                    for (int i = 1; i < layerNum; i++)
                    {
                        if (layersE[i] >= layersE[posOfEm]) posOfEm = i;
                    }
                    for (int j = 1; j < layerNum; j++)
                        y[j] = 0.5 * layersHeight[j] + y[j - 1] + 0.5 * layersHeight[j - 1];

                    for (ii = 0; ii < layerNum; ii++)
                    {

                        int temp = (int)(180 * layersHeight[ii] / (y[layerNum - 1] + layersHeight[layerNum - 1] / 2));
                        Rectangle rct = new Rectangle(82 - (int)(75 * layersE[ii] / layersE[posOfEm]), 10 + temp2, (int)(150 * layersE[ii] / layersE[posOfEm]), temp);
                        temp2 += temp;
                        switch (ii % 5)
                        {
                            case 0:
                                pic2.FillRectangle(Brushes.DarkGray, rct); break;
                            case 1:
                                pic2.FillRectangle(Brushes.LimeGreen, rct); break;
                            case 2:
                                pic2.FillRectangle(Brushes.Blue, rct); break;
                            case 3:
                                pic2.FillRectangle(Brushes.Orchid, rct); break;
                            case 4:
                                pic2.FillRectangle(Brushes.MediumPurple, rct); break;
                            default:
                                break;
                        }

                    }
                    Pen p = new Pen(Color.Red, 2);
                    p.DashPattern = new float[] { 2, 1 };
                    pic2.DrawLine(p, 5, (int)(180 * Yc / (y[layerNum - 1] + layersHeight[layerNum - 1] / 2)) + 10, 165, (int)(180 * Yc / (y[layerNum - 1] + layersHeight[layerNum - 1] / 2)) + 10);
                }
            }
            
        }
  
        private void 帮助ToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void 关于ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
            string str = "";
            str += "复合梁强度计算器 V4.3\n";
            str += "\n作者：\n";
            str += "王俊博  学号：12131234\n";
            str += "郭鑫     学号：12131226\n";
            str += "黎泽慧  学号：12131201\n";
            str += "\n";
            str += "指导教师：张彤\n";
            MessageBox.Show(str, "关于");
        
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void button8_Click(object sender, EventArgs e)
        {
            string str = "";
            str += "复合梁强度计算器 V4.3\n";
            str += "\n作者：\n";
            str += "王俊博  学号：12131234\n";
            str += "郭鑫     学号：12131226\n";
            str += "黎泽慧  学号：12131201\n";
            str += "\n";
            str += "指导教师：张彤\n";
            MessageBox.Show(str, "关于");
        }

        private void 退出ToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void button7_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}