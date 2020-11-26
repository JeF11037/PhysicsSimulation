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
    public partial class Main : Form
    {
        Graphics GRAPHICS_CANVAS_drawCanvas;
        Graphics GRAPHICS_CANVAS_dragCanvas;
        Strongbox strongbox = new Strongbox();
        Object figure;
        public Main()
        {
            InitializeComponent();
            strongbox.POINT_array = new PointF[] { };
            GRAPHICS_CANVAS_dragCanvas = pictureBox1.CreateGraphics();
            GRAPHICS_CANVAS_drawCanvas = pictureBox2.CreateGraphics();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            ClearCanvas();
        }

        private void ClearCanvas()
        {
            GRAPHICS_CANVAS_drawCanvas.Clear(Color.Wheat);
            strongbox.POINT_last = new PointF(0, 0);
            strongbox.POINT_array = new PointF[] { };
        }

        private void pictureBox1_MouseUp(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            if (figure != null)
            {
                figure.OnGround = false;
                figure.Time = 0;
                Simulate();
            }
        }

        private void pictureBox1_MouseDown(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            if (figure != null)
            {
                figure.OnGround = true;
            }
        }

        private void pictureBox2_MouseMove(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            switch (e.Button)
            {
                case System.Windows.Forms.MouseButtons.Left:
                    if (strongbox.POINT_last.X != 0 && strongbox.POINT_last.Y != 0)
                    {
                        GRAPHICS_CANVAS_drawCanvas.DrawLine(strongbox.PEN_pen, strongbox.POINT_last.X, strongbox.POINT_last.Y, e.X, e.Y);
                        System.Drawing.PointF[] TEMP_array = strongbox.POINT_array;
                        TEMP_array = TEMP_array.Concat(new System.Drawing.PointF[] { new System.Drawing.PointF(e.X, e.Y) }).ToArray();
                        strongbox.POINT_array = TEMP_array;
                    }
                    strongbox.POINT_last = new System.Drawing.Point(e.X, e.Y);
                    break;
            }
            strongbox.POINT_offset = e.Location;
        }

        private void pictureBox1_MouseMove(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            switch (e.Button)
            {
                case MouseButtons.Left:
                    if (strongbox.POINT_last.X != 0 && strongbox.POINT_last.Y != 0)
                    {
                        figure.DrawNewPosition(e.X - strongbox.POINT_last.X, e.Y - strongbox.POINT_last.Y);
                    }
                    strongbox.POINT_last = new System.Drawing.Point(e.X, e.Y);
                    break;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            figure = new Object(GRAPHICS_CANVAS_dragCanvas, strongbox.PEN_pen, strongbox.POINT_array);
            ClearCanvas();
            Simulate();
        }

        private async void Simulate()
        {
            try
            {
                while (!figure.OnGround)
                {
                    UseGravity();
                    strongbox.POINT_last = new System.Drawing.PointF(figure.Lower.X, figure.Lower.Y);
                    await Task.Delay(100);
                }
            }
            catch (Exception)
            {
            }
        }

        private void UseGravity()
        {
            figure.GetLower();
            if (figure.Ground - figure.Lower.Y > 0)
            {
                figure.UseGravity();
                ShowStat(figure);
                figure.Time++;
            }
            else
            {
                figure.DrawNewPosition(figure.Lower.X, figure.Ground);
                figure.OnGround = true;
                figure.Time = 0;
            }
        }

        private void ShowStat(Object obj)
        {
            label3.Text = "X: " + Math.Round(obj.Lower.X);
            label4.Text = "Y: " + Math.Round(obj.Lower.Y);
            label5.Text = "OnGround: " + obj.OnGround;
            label6.Text = "Speed: " + Math.Round(obj.Speed, 2) + " M/s";
        }
    }
}
