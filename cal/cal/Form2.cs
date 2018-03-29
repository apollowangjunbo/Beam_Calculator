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
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }
        public bool change = false;
        public Form2(Point p)
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.Manual;
            this.Location = p;
            counter = 0;
            change = false;
            for (int i = 0; i < Form1.layerNum; i++)
            {
                addItems(Form1.layersHeight[i]);
            }
            if(Form1.layerWidth!=0)
                textBox1.Text = Form1.layerWidth.ToString();
        }
        public int counter=0;
        public void addItems(double a)
        {
            counter++;
            ListViewItem lv = new ListViewItem();
            lv.SubItems[0].Text = counter.ToString();
            lv.SubItems.Add(a.ToString());
            listView1.Items.Add(lv);
        }
        public static bool checkDouble(String str)
        {
            int len = str.Length;
            int c = 0;
            char[] charArr=new char[len];
            charArr=str.ToCharArray(0,len);
            if(charArr[0]=='.'||charArr[len-1]=='.') return false;
            for (int i = 0; i < len; i++)
            {
                if (charArr[i] == '.') c++;
            }
            if (c >= 2) return false;
            else return true;
        }
        private void button4_Click(object sender, EventArgs e)
        {
            if (textBox2.Text != ""&&checkDouble(textBox2.Text))
            {
                counter++;
                ListViewItem lv = new ListViewItem();
                lv.SubItems[0].Text = counter.ToString();
                lv.SubItems.Add(textBox2.Text);
                listView1.Items.Add(lv);
                textBox2.Text = "";
                change = true;
            }
            else MessageBox.Show("invalid input","error");
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count == 0)
                MessageBox.Show("no selection", "error");
            else
            {
                int pos = listView1.SelectedItems[0].Index;
                listView1.Items[pos].Remove();
                for (int i = pos; i < counter - 1; i++)
                {
                    listView1.Items[i].SubItems[0].Text = (i + 1).ToString();
                }
                counter--;
                change = true;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count == 0)
                MessageBox.Show("no selection", "error");
            else
            {
                int pos = listView1.SelectedItems[0].Index;
                if (pos >= 1)
                {
                    ListViewItem lv = new ListViewItem();
                    lv.SubItems[0].Text = (pos).ToString();
                    lv.SubItems.Add(listView1.SelectedItems[0].SubItems[1].Text);
                    listView1.Items[pos].SubItems[0].Text = (pos + 1).ToString();
                    listView1.Items[pos].SubItems[1].Text = listView1.Items[pos - 1].SubItems[1].Text;
                    listView1.Items[pos - 1] = lv;
                    change = true;
                }
                else MessageBox.Show("illegal operation", "error");
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count == 0)
                MessageBox.Show("no selection","error");
            else
            {
                int pos = listView1.SelectedItems[0].Index;
                if (pos < counter-1)
                {
                    ListViewItem lv = new ListViewItem();
                    lv.SubItems[0].Text = (pos+2).ToString();
                    lv.SubItems.Add(listView1.SelectedItems[0].SubItems[1].Text);
                    listView1.Items[pos].SubItems[0].Text = (pos + 1).ToString();
                    listView1.Items[pos].SubItems[1].Text = listView1.Items[pos + 1].SubItems[1].Text;
                    listView1.Items[pos + 1] = lv;
                    change = true;
                }
                else MessageBox.Show("illegal operation","error");
            }
        }

        private void checkInput(object sender, KeyPressEventArgs e)
        {
            if (!(Char.IsNumber(e.KeyChar)) && e.KeyChar != (char)13 && e.KeyChar != (char)8 && e.KeyChar != '.')
            {
                e.Handled = true;
            } 
        }
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            Form1.shape = this.comboBox1.Text;
            switch (Form1.shape)
            {
                case "矩形截面":
                    this.label1.Visible =true;
                    this.textBox1.Visible = true;
                    this.height.Text = "高度(m)";
                    this.label2.Text = "高度(m)";
                    break;
                  
                case "圆形截面":
                    this.label1.Visible = false;
                    this.textBox1.Visible = false;
                    this.height.Text = "直径(m)";
                    this.label2.Text = "直径(m)";
                    break;

                default:
                    MessageBox.Show("请选择截面形状", "error");
                    break;

            };

        }
        private void button5_Click(object sender, EventArgs e)
        {//yuanx
            if (Form1.shape == "圆形截面")
            {  if (counter != 0)
                {
                   
                    if (change == true )
                    {
                        Form1.layerNum = counter;
                        
                        Form1.layersHeight = new double[counter];
                        Form1.layersE = new double[counter];
                        double[] tempArr = new double[counter];
                        for (int i = 0; i < counter; i++)
                        {
                            tempArr[i] = Convert.ToDouble(listView1.Items[i].SubItems[1].Text);
                        }
                        Form1.layersHeight = tempArr;
                        this.Close();
                    }
                    else this.Close();
                }
                else MessageBox.Show("invalid input", "error");
             }
                //矩形截面
            else
            {

                if (counter != 0 && textBox1.Text != "" && checkDouble(textBox1.Text))
                {
                    if (Convert.ToDouble(textBox1.Text) <= 0)
                        MessageBox.Show("invalid input", "error");
                    else if (change == true || Form1.layerWidth.ToString() != textBox1.Text)
                    {
                        Form1.layerNum = counter;
                        Form1.layerWidth = Convert.ToDouble(textBox1.Text);
                        Form1.layersHeight = new double[counter];
                        Form1.layersE = new double[counter];
                        double[] tempArr = new double[counter];
                        for (int i = 0; i < counter; i++)
                        {
                            tempArr[i] = Convert.ToDouble(listView1.Items[i].SubItems[1].Text);
                        }
                        Form1.layersHeight = tempArr;
                        this.Close();
                    }
                    else this.Close();
                }
                else MessageBox.Show("invalid input", "error");
            }
        }



        private void label1_Click(object sender, EventArgs e)
        {

        }
      /*  private void button_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyValue == 13)     //检测否按Enter键
            {

                button4_Click();

            }
        }*/
        private void Form2_Load(object sender, EventArgs e)
        {

        }
    }
}
