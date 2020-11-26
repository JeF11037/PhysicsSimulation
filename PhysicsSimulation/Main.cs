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

        private void button1_Click(object sender, EventArgs e)
        {
            Object figure = new Object(GRAPHICS_CANVAS_dragCanvas, strongbox.PEN_pen, strongbox.PEN_pens, strongbox.POINT_array);
            ClearCanvas();
            figure.Simulate();
        }
    }
}
