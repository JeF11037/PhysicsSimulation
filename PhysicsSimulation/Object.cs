using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PhysicsSimulation
{
    class Object
    {
        bool OnGround = false;
        double Ground = 644;
        PointF Lower = new PointF(1000, 1000);
        PointF[] array;
        Graphics canvas;
        Pen pen;
        Pen pens;
        public Object(Graphics c, Pen p, Pen ps, PointF[] a)
        {
            array = a;
            canvas = c;
            pen = p;
            pens = ps;
            canvas.DrawLines(pen, array);
        }

        public void Simulate()
        {
            while (!OnGround)
            {
                GetLower();
                if (Ground - Lower.Y > 0)
                {
                    UseGravity();
                }
                else
                {
                    OnGround = true;
                }
                System.Threading.Thread.Sleep(10);
            }
        }

        public void UseGravity()
        {
            DrawNewPosition(0, GetGravity(Ground - Lower.Y));
        }

        public void GetLower()
        {
            foreach (var el in array)
            {
                PointF point = el;
                if (point.Y < Lower.Y)
                {
                    Lower = point;
                }
            }
        }

        public void DrawNewPosition(double X, double Y)
        {
            foreach (var el in array)
            {
                PointF point = el;
                point.X += (float)X;
                point.Y += (float)Y;
            }
            canvas.DrawLines(pen, array);
            canvas.DrawLines(pens, array);
        }

        public double GetGravity(double r)
        {
            return 6.67 * (double)array.Length / (Math.Pow(r, 2) * Math.Pow(10, -1));
        }
    }
}
