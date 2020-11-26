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
        public bool OnGround = false;
        public double Ground = 600;
        public PointF Lower = new PointF(0, 0);
        public PointF[] array;
        public Graphics canvas;
        public Pen pen;
        public double Speed;
        public double Time = 0;
        public Object(Graphics c, Pen p, PointF[] a)
        {
            array = a;
            canvas = c;
            pen = p;
        }

        public void UseGravity(double impulse)
        {
            Speed = GetGravity(Time);
            impulse += Speed * array.Length * Math.Pow(10, -3);
            DrawNewPosition(impulse, Speed);
        }

        public void GetLower()
        {
            foreach (var el in array)
            {
                PointF point = el;
                if (point.Y > Lower.Y)
                {
                    Lower = point;
                }
            }
        }

        public void DrawNewPosition(double X, double Y)
        {
            canvas.Clear(Color.Wheat);
            for (int tick = 0; tick < array.Length; tick++)
            {
                if (array[tick].X + (float)X <= 900 && array[tick].X + (float)X >= 0)
                {
                    array[tick].X += (float)X;
                }
                else
                {
                    OnGround = true;
                    Time = 0;
                }
                if (array[tick].Y + (float)Y <= 600 && array[tick].Y + (float)Y >= 0)
                {
                    array[tick].Y += (float)Y;
                }
                else
                {
                    OnGround = true;
                    Time = 0;
                }
            }
            canvas.DrawLines(pen, array);
        }

        public double GetGravity(double t)
        {
            return t + (9.8 * Math.Pow(t, 2) / 2) * Math.Pow(10, -10);
        }
    }
}
