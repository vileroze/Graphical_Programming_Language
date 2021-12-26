using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphicalProgrammingLanguage
{
    public class Circle : Shape
    {
        int radius;

        public Circle()
        {
        }

        public Circle(Color colour, int x, int y, int radius) : base(colour, x, y)
        {
            this.radius = radius; //the only thing that is different from shape
        }

        public override void set(Color colour, params int[] list)
        {
            //list[0] is x, list[1] is y, list[2] is radius
            base.set(colour, list[0], list[1]);
            this.radius = list[2];

        }

        public override void draw(Graphics g)
        {
            Pen p = new Pen(Color.Black, 2);
            SolidBrush b = new SolidBrush(colour);
            g.FillEllipse(b, x - radius, y - radius, radius * 2, radius * 2);
            g.DrawEllipse(p, x - radius, y - radius, radius * 2, radius * 2);
        }
        public override string ToString() //all classes inherit from object and ToString() is abstract in object
        {
            return base.ToString() + "  " + this.radius;
        }
    }
}
