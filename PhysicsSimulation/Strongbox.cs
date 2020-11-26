using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhysicsSimulation
{
    class Strongbox
    {
        public System.Drawing.PointF[] POINT_array {get; set;}
        public System.Drawing.PointF POINT_offset { get; set; }
        public System.Drawing.Pen PEN_pen { get
            {
                return new System.Drawing.Pen(System.Drawing.Color.Black, 2);
            } 
        }
        public System.Drawing.PointF POINT_last { get; set; }
    }
}
