using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Task_1
{
    public partial class FormatDialogBox : Form
    {
        int fontSize;
        string fontFamily;
        Color col;
        public FormatDialogBox()
        {
            InitializeComponent();
        }
        public string CompanyName
        {
            set { textBox1.Text = value; }
            get
            {
                string str;
                if (textBox2.Text == "")
                {
                    str = textBox1.Text;
                }
                else
                {
                    str = textBox2.Text;
                }
                return str;
            }
        }
        public int FontSize
        {
            set
            {
               fontSize = value;
                if (fontSize == 16)
                {

                    radioButton4.Checked = true;
                }
                else if (fontSize == 20)
                {
                    radioButton5.Checked = true;
                }
                else if (fontSize == 24)
                {
                    radioButton6.Checked = true;
                }
            }
            get
            {
                if (radioButton4.Checked)
                {
                    fontSize = 16;
                }
                else if (radioButton5.Checked)
                {
                    fontSize = 20;
                }
                else if (radioButton6.Checked)
                {
                    fontSize = 24;
                }
                return fontSize;
            }
        }
        public string FontFamily
        {
            set
            {
                fontFamily = value;
                if (fontFamily == "Times New Roman")
                {

                    radioButton1.Checked = true;
                }
                else if (fontFamily == "Arial")
                {
                    radioButton2.Checked = true;
                }
                else if (fontFamily == "Courier")
                {
                    radioButton3.Checked = true;
                }
            }
            get
            {
                if (radioButton1.Checked)
                {
                    fontFamily = "Times New Roman";
                }
                else if (radioButton2.Checked)
                {
                    fontFamily = "Arial";
                }
                else if (radioButton3.Checked)
                {
                    fontFamily = "Courier";
                }
                return fontFamily;
            }
        }
        public Color Col
        {
            set
            {
                col = value;
                colorDialog1.Color = col;
            }
            get
            {
                col = colorDialog1.Color;
                return col;
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            colorDialog1 = new ColorDialog();
            colorDialog1.ShowDialog();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

    }
}
