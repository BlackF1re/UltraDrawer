using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace UltraDrawer
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            SetSize();
        }
        private class ArrayOfPoints //private
        {
            private int index;
            private Point[] points;
            public ArrayOfPoints(int size)
            {
                if (size <= 0)
                {
                    size = 2;
                }
                points = new Point[size];
            }

            public void SetPoint(int x, int y)
            {
                if (index >= points.Length)
                {
                    index = 0;
                }
                points[index] = new Point(x, y);
                index++;
            }

            public void ResetPoints()
            {
                index = 0;
            }

            public int GetCountOfPoints()
            {
                return index;
            }

            public Point[] GetPoints()
            {
                return points;
            }
        }

        private bool isMouseDown = false;
        private ArrayOfPoints arrayOfPoints = new ArrayOfPoints(2);
        Bitmap bitmap = new Bitmap(100, 100);
        Graphics graphics;
        Pen pen = new Pen(Color.Black, 3f);

        private void SetSize()
        {
            Rectangle rectangle = Screen.PrimaryScreen.Bounds;
            bitmap = new Bitmap(rectangle.Width, rectangle.Height);
            graphics = Graphics.FromImage(bitmap);

            pen.StartCap = System.Drawing.Drawing2D.LineCap.Round;
            pen.EndCap = System.Drawing.Drawing2D.LineCap.Round;

        }

        private void PictureBox_MouseDown(object sender, MouseEventArgs e)
        {
            isMouseDown = true;

        }

        private void PictureBox_MouseUp(object sender, MouseEventArgs e)
        {
            isMouseDown = false;
            arrayOfPoints.ResetPoints();
        }    
        
        private void PictureBox_MouseMove(object sender, MouseEventArgs e)
        {
            if (!isMouseDown)
                return;
            arrayOfPoints.SetPoint(e.X, e.Y);
            if (arrayOfPoints.GetCountOfPoints() >= 2)
                graphics.DrawLines(pen, arrayOfPoints.GetPoints());
            PictureBox.Image = bitmap;
            arrayOfPoints.SetPoint(e.X, e.Y);
        }

        private void color1_Click(object sender, EventArgs e)
        {
            pen.Color = ((Button)sender).BackColor;
        }


        private void saveBtn_Click(object sender, EventArgs e)
        {
            saveFileDialog1.Filter = "JPG(*.JPG)|*.jpg";
            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                if(PictureBox.Image != null)
                {
                    PictureBox.Image.Save(saveFileDialog1.FileName);
                }
            }
        }

        private void clearBtn_Click(object sender, EventArgs e)
        {
            graphics.Clear(Color.White); //PictureBox.BackColor
            PictureBox.Image = bitmap;
        }

        private void paletteOpen_Click(object sender, EventArgs e)
        {
            if (colorDialog1.ShowDialog() == DialogResult.OK)
            {
                pen.Color = colorDialog1.Color;
                ((Button)sender).BackColor = colorDialog1.Color;
            }
        }

        private void brushSize_ValueChanged(object sender, EventArgs e)
        {
            pen.Width = brushSize.Value;
        }
    }
}
