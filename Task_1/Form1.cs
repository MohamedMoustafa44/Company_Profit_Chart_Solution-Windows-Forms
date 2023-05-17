using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Reflection;
using System.Runtime.ConstrainedExecution;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Task_1
{
    public partial class Form1 : Form
    {
        string CompName;
        Font StrFont;
        int StrFontSize;
        string FontFamily;
        SolidBrush StrBrush;
        Color strColor;

        Point TableUPoint;
        int TableWidth;
        int TableHeight;
        int cellHeight;
        Rectangle Table;

        Point ChartsUPoint;
        int ChartsRectWidth;
        int ChartsRectHeight;
        Point YStartPoint;
        Point YEndPoint;
        Point XStartPoint;
        Point XEndPoint;

        int[] keys;
        int[] values;
        int[] YValues;
        int range;

        List<Point> points;
        Color LineColor;
        Pen LinePen;
        DashStyle LineStyle;
        List<Rectangle> bars;
        Color BarsColor;
        HatchStyle BarsStyle;
        HatchBrush BarsBrush;
        
        public Form1()
        {
            InitializeComponent();
            CompName = "ABC";
            StrFontSize = 20;
            FontFamily = "Times New Roman";
            strColor = Color.Red;

            TableUPoint = new Point(1100,100);
            TableWidth = 400;
            TableHeight = 550;
            cellHeight= 50;
            Table = new Rectangle(TableUPoint, new Size(TableWidth, TableHeight));

            ChartsUPoint = new Point(80, 150);
            ChartsRectWidth = 660;
            ChartsRectHeight = 550;
            YStartPoint = new Point(ChartsUPoint.X, ChartsUPoint.Y);
            YEndPoint = new Point(ChartsUPoint.X, ChartsUPoint.Y + ChartsRectHeight);
            XStartPoint = new Point(ChartsUPoint.X, ChartsUPoint.Y + ChartsRectHeight);
            XEndPoint = new Point(ChartsUPoint.X + ChartsRectWidth, ChartsUPoint.Y + ChartsRectHeight);

            keys = new int[] {1988, 1989, 1990, 1991, 1992, 1993, 1994, 1995, 1996, 1997};
            values = new int[] {150, 170, 180, 175, 200, 250, 210, 240, 280, 140};
            YValues = new int[] { 28, 56, 84, 112, 140, 168, 196, 224, 252, 280 };
            range = values.Max(x => x)/values.Length;


            points = new List<Point>();
            bars = new List<Rectangle>();

            for(int index = 0; index < values.Length; index++)
            {
                points.Add(new Point(YEndPoint.X + (ChartsRectWidth/(values.Length + 1) * (index + 1)), YEndPoint.Y - ((ChartsRectHeight/(values.Length + 1)) * values[index]/range)));
            }
            LineColor = Color.Blue;
            LineStyle = DashStyle.Solid;
            for (int index = 0; index < values.Length; index++)
            {
                bars.Add(new Rectangle(new Point(YEndPoint.X + (ChartsRectWidth/22)*index + ((ChartsRectWidth/22) * (index + 1)), YEndPoint.Y - (ChartsRectHeight/11 * values[index] / range)), new Size(ChartsRectWidth/11 - 5, YEndPoint.Y - (YEndPoint.Y - ((ChartsRectHeight/11) * values[index] / range)) )));
            }
            BarsColor = Color.Gold;
            BarsStyle = HatchStyle.Horizontal;
            

        }

        protected override void OnPaint(PaintEventArgs e)
        {
            StrFont = new Font(FontFamily, StrFontSize, FontStyle.Underline);
            StrBrush = new SolidBrush(strColor);
            DisplayText(CompName, StrFont,StrBrush, new Point((this.Width - 25 * "ABC".Length)/2,100));
            DisplayText("Annual Profits", StrFont, StrBrush, new Point((this.Width - 13 * "Annual Profits".Length) / 2, 150));
            DisplayRect(Table);
            DisplayLine(TableUPoint.X + TableWidth/2, TableUPoint.Y, TableUPoint.X + TableWidth/2, TableUPoint.Y+TableHeight);
            AddRows(keys.Length, TableUPoint.X, TableUPoint.Y, TableUPoint.X + TableWidth, TableUPoint.Y, cellHeight);
            DisplayText("Year", new Font("Times New Roman", 15), new SolidBrush(Color.DarkGreen), new Point(TableUPoint.X + 10, TableUPoint.Y + 5));
            DisplayText("Profits", new Font("Times New Roman", 15), new SolidBrush(Color.DarkGreen), new Point(TableUPoint.X + 10 + TableWidth/2, TableUPoint.Y + 5));
            for (int cel = 0; cel < keys.Length; cel++)
            {
                DisplayText($"{keys[cel]}", new Font("Times New Roman", 10), new SolidBrush(Color.Green), new Point(TableUPoint.X + 10, TableUPoint.Y + 5 + ((cel+1) * cellHeight)));
            }
            for (int cel = 0; cel < keys.Length; cel++)
            {
                DisplayText($"{values[cel]}", new Font("Times New Roman", 10), new SolidBrush(Color.Green), new Point(TableUPoint.X + 10 + TableWidth/2, TableUPoint.Y + 5 + ((cel + 1) * cellHeight)));
            }
            DisplayLine(YStartPoint.X, YStartPoint.Y, YEndPoint.X, YEndPoint.Y);
            DisplayLine(YStartPoint.X, YStartPoint.Y, YStartPoint.X - 15, YStartPoint.Y + 15);
            DisplayLine(YStartPoint.X, YStartPoint.Y, YStartPoint.X + 15, YStartPoint.Y + 15);
            DivideAxises(keys.Length, ChartsRectHeight/11, 5, YEndPoint.X, YEndPoint.Y, YEndPoint.X, YEndPoint.Y, YValues, "y");
            DisplayText("Profit", new Font("Times New Roman", 15), new SolidBrush(Color.DarkGreen), new Point(YStartPoint.X - 20, YStartPoint.Y - 25));

            DisplayLine(XStartPoint.X, XStartPoint.Y, XEndPoint.X, XEndPoint.Y);
            DisplayLine(XEndPoint.X, XEndPoint.Y, XEndPoint.X - 15, XEndPoint.Y - 15);
            DisplayLine(XEndPoint.X, XEndPoint.Y, XEndPoint.X - 15, XEndPoint.Y + 15);
            DivideAxises(keys.Length, ChartsRectWidth/11, 5, XStartPoint.X, XStartPoint.Y, XStartPoint.X, XStartPoint.Y, keys, "x");
            DisplayText("Year", new Font("Times New Roman", 15), new SolidBrush(Color.DarkGreen), new Point(XEndPoint.X + 10, XEndPoint.Y));
            BarChart(bars.ToArray());
            LineChart(points.ToArray());


        }
        public void DisplayText(string str, Font strFont, Brush strBrush, Point strLocation)
        {
            Graphics g = this.CreateGraphics();
            g.DrawString(str, strFont, strBrush, strLocation);
        }

        public void DisplayLine(float p1x, float p1y, float p2x, float p2y)
        {
            Graphics g = this.CreateGraphics();
            g.DrawLine(new Pen(Color.Green, 5), p1x, p1y, p2x, p2y);
        }
        public void DisplayRect(Rectangle rect)
        {
            Graphics g = this.CreateGraphics();
            g.DrawRectangle(new Pen(Color.Green, 5), rect);
            
        }
        public void AddRows(int numberOfCells, float startX, float startY, float endX, float endY, int cellHeight)
        {
            for(int cel = 1; cel <= numberOfCells; cel++)
            {
                DisplayLine(startX, startY + (cel * cellHeight), endX, endY + (cel * cellHeight));
            }
        }
        public void DivideAxises(int numOfDivs, int divHeight, int lineWidth, float p1X, float p1Y, float p2X, float p2Y, int[] values, string axis)
        {
            if(axis == "y")
            {
                for (int div = 1; div <= numOfDivs; div++)
                {
                    DisplayLine(p1X - lineWidth, p1Y - (div * divHeight), p2X + lineWidth, p2Y - (div * divHeight));
                    DisplayText($"{values[div-1]}", new Font("Times New Roman", 15), new SolidBrush(Color.DarkGreen), new Point((int)(p1X - lineWidth * 10), (int)(p1Y - (div * divHeight) - 10)));

                }
            }
            else if(axis == "x")
            {
                for (int div = 1; div <= numOfDivs; div++)
                {
                    DisplayLine(p1X + (div * divHeight), p1Y - lineWidth, p2X + (div * divHeight), p2Y + lineWidth);
                    DisplayText($"{values[div - 1]}", new Font("Times New Roman", 15), new SolidBrush(Color.DarkGreen), new Point((int)(p1X + (div * divHeight) - 15), (int)(p1Y + lineWidth * 5)));
                }
            }
        }
        public void LineChart( Point[] values)
        {
            Graphics g = this.CreateGraphics();
            LinePen = new Pen(LineColor, 3);
            LinePen.DashStyle = LineStyle;
            g.DrawLines(LinePen, values);
        }
        public void BarChart(Rectangle[] bars)
        {
            Graphics g = this.CreateGraphics();
            BarsBrush = new HatchBrush(BarsStyle, BarsColor);
            g.FillRectangles(BarsBrush, bars);
        }

        private void Form1_KeyPress(object sender, KeyPressEventArgs e)
        {
            switch ((int) e.KeyChar)
            {
                case 2:
                    LineColor = Color.Blue;
                    break;
                case 7:
                    LineColor = Color.Green;
                    break;
                case 18:
                    LineColor = Color.Red;
                    break;
            }
            Invalidate();
        }

        private void Form1_MouseClick(object sender, MouseEventArgs e)
        {
            if(e.Button == MouseButtons.Left && e.X >= YStartPoint.X && e.Y >= YStartPoint.Y && e.X <= XEndPoint.X && e.Y <= XEndPoint.Y)
            {
                int revenue = (YEndPoint.Y - e.Y) * range / (ChartsRectHeight / (values.Length + 1));
                int year = 0;
                for(int i = 0; i < keys.Length; i++)
                {
                    if (e.X >= YEndPoint.X + 30 + 5 * i && e.X <= YEndPoint.X + 30 + 60*(i+1))
                    {
                        year = keys[i];
                        break;
                    }
                }
                MessageBox.Show("Revenue = " + revenue + "\nYear = " + year);
            }
        }

        private void redToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LineColor = Color.Red;
            redToolStripMenuItem.Checked = true;
            greenToolStripMenuItem.Checked = false;
            blueToolStripMenuItem.Checked = false;
            Invalidate();
        }

        private void greenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LineColor = Color.Green;
            redToolStripMenuItem.Checked = false;
            greenToolStripMenuItem.Checked = true;
            blueToolStripMenuItem.Checked = false;
            Invalidate();
        }

        private void blueToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LineColor = Color.Blue;
            redToolStripMenuItem.Checked = false;
            greenToolStripMenuItem.Checked = false;
            blueToolStripMenuItem.Checked = true;
            Invalidate();
        }

        private void solidToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LineStyle = DashStyle.Solid;
            solidToolStripMenuItem.Checked = true;
            dashToolStripMenuItem.Checked = false;
            dotToolStripMenuItem.Checked = false;
            Invalidate();
        }

        private void dashToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LineStyle = DashStyle.Dash;
            solidToolStripMenuItem.Checked = false;
            dashToolStripMenuItem.Checked = true;
            dotToolStripMenuItem.Checked = false;
            Invalidate();
        }

        private void dotToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LineStyle= DashStyle.Dot;
            solidToolStripMenuItem.Checked = false;
            dashToolStripMenuItem.Checked = false;
            dotToolStripMenuItem.Checked = true;
            Invalidate();
        }

        private void redToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            BarsColor= Color.Red;
            redToolStripMenuItem1.Checked = true;
            greenToolStripMenuItem1.Checked = false;
            blueToolStripMenuItem1.Checked = false;
            Invalidate();
        }

        private void greenToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            BarsColor = Color.Green;
            redToolStripMenuItem1.Checked = false;
            greenToolStripMenuItem1.Checked = true;
            blueToolStripMenuItem1.Checked = false;
            Invalidate();
        }

        private void blueToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            BarsColor = Color.Blue;
            redToolStripMenuItem1.Checked = false;
            greenToolStripMenuItem1.Checked = false;
            blueToolStripMenuItem1.Checked = true;
            Invalidate();
        }

        private void dottedDimondToolStripMenuItem_Click(object sender, EventArgs e)
        {
            BarsStyle = HatchStyle.DottedDiamond;
            dottedDimondToolStripMenuItem.Checked = true;
            backwardDiagonalToolStripMenuItem.Checked = false;
            diagonalCrossToolStripMenuItem.Checked = false;
            Invalidate();
        }

        private void backwardDiagonalToolStripMenuItem_Click(object sender, EventArgs e)
        {
            BarsStyle = HatchStyle.BackwardDiagonal;
            dottedDimondToolStripMenuItem.Checked = false;
            backwardDiagonalToolStripMenuItem.Checked = true;
            diagonalCrossToolStripMenuItem.Checked = false;
            Invalidate();
        }

        private void diagonalCrossToolStripMenuItem_Click(object sender, EventArgs e)
        {
            BarsStyle = HatchStyle.DiagonalCross;
            dottedDimondToolStripMenuItem.Checked = false;
            backwardDiagonalToolStripMenuItem.Checked = false;
            diagonalCrossToolStripMenuItem.Checked = true;
            Invalidate();
        }

        private void Form1_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right && e.X >= YStartPoint.X && e.Y >= YStartPoint.Y && e.X <= XEndPoint.X && e.Y <= XEndPoint.Y)
            {
                this.ContextMenuStrip = contextMenuStrip2;
            }
            else
            {
                this.ContextMenuStrip = contextMenuStrip1;
            }
        }

        private void Form1_MouseMove(object sender, MouseEventArgs e)
        {
            int revenue = (YEndPoint.Y - e.Y) * range / (ChartsRectHeight / (values.Length + 1));
            int year = 0;
            for (int i = 0; i < keys.Length; i++)
            {
                if (e.X >= YEndPoint.X + 30 + 5 * i && e.X <= YEndPoint.X + 30 + 60 * (i + 1))
                {
                    year = keys[i];
                    break;
                }
            }
            if (e.X >= YStartPoint.X && e.Y >= YStartPoint.Y && e.X <= XEndPoint.X && e.Y <= XEndPoint.Y)
            {
                toolStripStatusLabel1.Text = $"Revenue = {revenue}, Year = {year}";
            }
        }

        private void companyNameToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormatDialogBox FormatDialog = new FormatDialogBox();
            DialogResult FormatDialogResult;
            FormatDialog.CompanyName = CompName;
            FormatDialog.FontSize = StrFontSize;
            FormatDialog.FontFamily = FontFamily;
            FormatDialog.Col = strColor;
            FormatDialogResult = FormatDialog.ShowDialog();
            if(FormatDialogResult == DialogResult.OK)
            {
                CompName = FormatDialog.CompanyName;
                StrFontSize = FormatDialog.FontSize;
                FontFamily = FormatDialog.FontFamily;
                strColor = FormatDialog.Col;
                Invalidate();
            }
        }
    }
}
