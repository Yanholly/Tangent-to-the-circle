using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Kasatelnaya
{
    public partial class Form1 : Form
    {
        bool isCircClicked = false;
        bool isPointClicked = false;
        int dX = 0;
        int dY = 0;
        int circx = 550 ,circy = 250, pointx = 104, pointy = 104, rad = 100;

        Point peres = new Point();
  
        public Form1()
        {

            InitializeComponent();
             
        }

     

        private void FindPeres()
        {
            int Dx = Math.Abs(circx-pointx);
            int Dy = Math.Abs(pointy - circy);
            int L = (int)Math.Sqrt(Math.Pow(Math.Abs(circx - pointx), 2) + Math.Pow(Math.Abs(pointy - circy), 2));
            int le = L - rad;
            int dxe = (Dx * le) / L;
            int dye = (Dy * le) / L;
            if (circx > pointx)
                peres.X = dxe + pointx;
            else
                peres.X = pointx - dxe;
            if (circy > pointy)
                peres.Y = dye + pointy;
            else
                peres.Y = pointy - dye;
        }

        private float FindX(int sign)
        {
            FindPeres();
            float L = (float)Math.Sqrt(Math.Pow(Math.Abs(circx - pointx), 2) + Math.Pow(Math.Abs(pointy - circy), 2));
            float X = circx + (peres.X - circx) * (rad / L) - (peres.Y - circy) * sign * (float)Math.Sqrt(1 - Math.Pow(rad / L, 2));
            return X;
        }

        private float FindY(int sign)
        {
            FindPeres();
            float L = (float)Math.Sqrt(Math.Pow(Math.Abs(circx - pointx), 2) + Math.Pow(Math.Abs(pointy - circy), 2));
            float Y = circy + (peres.X - circx) * sign * (float)Math.Sqrt(1 - Math.Pow(rad / L, 2)) + (peres.Y - circy) * (rad / L);
            return Y;
        }


        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            rad = (int)numericUpDown1.Value;
            pictureBox1.Invalidate();
        }

        private void numericUpDown2_ValueChanged(object sender, EventArgs e)
        {
            circx = (int)numericUpDown2.Value;
            pictureBox1.Invalidate();
        }

        private void numericUpDown3_ValueChanged(object sender, EventArgs e)
        {
            circy = (int)numericUpDown3.Value;
            pictureBox1.Invalidate();
        }

        private void numericUpDown4_ValueChanged(object sender, EventArgs e)
        {
            pointx = (int)numericUpDown4.Value;
            pictureBox1.Invalidate();
        }

        private void numericUpDown5_ValueChanged(object sender, EventArgs e)
        {
            pointy = (int)numericUpDown5.Value;
            pictureBox1.Invalidate();
        }

      

        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {
            FindPeres();
            Pen Circpen = new Pen(Color.Blue, 2);
            Pen Linecpen = new Pen(Color.Gray, 3);
            SolidBrush brush = new SolidBrush(Color.Red);
            e.Graphics.DrawEllipse(Circpen, circx - rad, circy - rad, rad * 2, rad * 2);
      
            e.Graphics.DrawLine(Linecpen, pointx, pointy, peres.X, peres.Y);

            e.Graphics.DrawLine(Linecpen, pointx, pointy, FindX(1), FindY(1));
            e.Graphics.DrawLine(Linecpen, pointx, pointy, FindX(-1), FindY(-1));

            e.Graphics.FillEllipse(brush, pointx - 4, pointy - 4, 8, 8);
            e.Graphics.FillEllipse(brush, peres.X - 4, peres.Y - 4, 8, 8);
            e.Graphics.FillEllipse(brush, FindX(1) - 4, FindY(1) - 4, 8, 8);
            e.Graphics.FillEllipse(brush, FindX(-1) - 4, FindY(-1) - 4, 8, 8);

           
        }

        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {

            if (Math.Pow((e.X - pointx), 2) + Math.Pow((e.Y - pointy), 2) <= Math.Pow(4, 2))
            {
                isPointClicked = true;
                dX = e.X - pointx;
                dY = e.Y - pointy;
            }

            if (Math.Pow((e.X - circx), 2) + Math.Pow((e.Y - circy), 2) <= Math.Pow(rad, 2))
            {
                isCircClicked = true;
                dX = e.X - circx;
                dY = e.Y - circy;
            }

            
        }

        private void pictureBox1_MouseUp(object sender, MouseEventArgs e)
        {
            isCircClicked = false;
            isPointClicked = false;
        }

        private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            if (isPointClicked)
            {
                pointx = e.X - dX;
                pointy = e.Y - dY;
                pictureBox1.Invalidate();
            }

            if (isCircClicked)
            {
                circx = e.X - dX;
                circy = e.Y - dY;
                pictureBox1.Invalidate();
            }
           
        }
    }
}
