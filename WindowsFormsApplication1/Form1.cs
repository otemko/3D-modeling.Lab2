using System;
using System.Drawing;
using System.Windows.Forms;

namespace WindowsFormsApplication1
{
    public partial class Form1 : Form
    {
        private Draw drawStartPoints;
        private int numberPoints, startNumber = 0;
        private Graphics formGraphics;

        public Form1()
        {
            InitializeComponent();
            drawStartPoints = new Draw();
            formGraphics = this.CreateGraphics();

        }

        private void Form1_MouseDown(object sender, MouseEventArgs e)
        {
            if (numberPoints > 0 && numberPoints != startNumber)
            {
                var point = new PointOfFigure(new Point(e.X, e.Y), startNumber + 1);
                drawStartPoints.AddPointInList(point);
                drawStartPoints.DrawPoint(Color.Blue, formGraphics, point);
                startNumber++;
            }

            if (numberPoints > 0 && numberPoints == startNumber)
            {
                drawStartPoints.DrawLines(Color.Blue, formGraphics, drawStartPoints.ListPoints);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            bool result = Int32.TryParse(textBox1.Text, out numberPoints);
            if (result && numberPoints > 0)
            {
                textBox1.Enabled = false;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            int dx, dy;
            bool resultDx = Int32.TryParse(textBox2.Text, out dx);
            bool resultDy = Int32.TryParse(textBox3.Text, out dy);
            if (resultDx && resultDx)
            {
                var listMovement = drawStartPoints.MoveMentAllPoints(dx, dy, drawStartPoints.ListPoints);
                drawStartPoints.DrawListElements(Color.Red, listMovement, formGraphics);
            }

        }

        private void button3_Click(object sender, EventArgs e)
        {
            double sx, sy;
            int point;

            bool resultDx = double.TryParse(textBox5.Text, out sx);
            bool resultDy = double.TryParse(textBox4.Text, out sy);
            bool resultPoint = Int32.TryParse(textBox7.Text, out point);
            if (resultDx && resultDx && resultPoint)
            {
                var listScale = drawStartPoints.ScaleAllPoints(sx, sy, point);
                drawStartPoints.DrawListElements(Color.Green, listScale, formGraphics);
            }
        }
        private void TextboxKeyPressOnlyNumersWithMinus(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar <= 47 || e.KeyChar >= 59) && e.KeyChar != 8 && e.KeyChar != 45 && e.KeyChar != 44)
                e.Handled = true;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            double alpha;
            int point;

            bool resultAlpha = double.TryParse(textBox6.Text, out alpha);
            bool resultPoint = Int32.TryParse(textBox8.Text, out point);
            if (resultAlpha && resultPoint)
            {
                var listRotate = drawStartPoints.RotationAllPoints(alpha, point);
                drawStartPoints.DrawListElements(Color.Black, listRotate, formGraphics);
            }
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            Pen pen = new Pen(Color.Chocolate, 3);
            var graphics = e.Graphics;
            graphics.DrawLine(pen, this.Size.Width / 2, 0, this.Size.Width / 2, this.Size.Height);
            graphics.DrawLine(pen, 0, this.Size.Height / 2, this.Size.Width, this.Size.Height/2);
            graphics.DrawLine(pen, this.Size.Width/2 + 20, this.Size.Height / 2 - 10, this.Size.Width / 2 + 20, this.Size.Height / 2 + 10);
            graphics.DrawString("20", new Font("Times New Roman", 18), new SolidBrush(Color.Chocolate), this.Size.Width / 2 + 5, this.Size.Height / 2 + 10);
        }

        private void TextboxKeyPressOnlyNumersWithOutMinus(object sender, KeyPressEventArgs e)
        {

        }


    }
}
