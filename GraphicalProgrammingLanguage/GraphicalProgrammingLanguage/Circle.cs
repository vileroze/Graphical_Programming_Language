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

        public Circle() : base()
        {
        }

        public Circle(Color colour, int x, int y, int radius) : base(colour, x, y)
        {
            this.radius = radius; //the only thing that is different from shape
        }

        public override void set(Color colour, Boolean fill, bool flash, Color primaryColor, Color secondaryColor, params int[] list)
        {
            base.colour = colour;
            //list[0] is x, list[1] is y, list[2] is radius
            base.set(colour, fill, flash, primaryColor, secondaryColor, list[0], list[1]);
            this.radius = list[2];
        }

        public override void draw(Graphics g, Boolean fill)
        {
            SolidBrush brush = new SolidBrush(Color.Transparent);
            Pen pen = new Pen(base.colour, 2);

            if (base.fill == true)
            {
                brush = new SolidBrush(base.colour);
            }
            else
            {
                brush = new SolidBrush(Color.Transparent);
            }

            g.FillEllipse(brush, x - radius, y - radius, radius * 2, radius * 2);
            g.DrawEllipse(pen, x - radius, y - radius, radius * 2, radius * 2);
        }

        public override string ToString() //all classes inherit from object and ToString() is abstract in object
        {
            return base.ToString() + "  " + this.radius;
        }
    }
}
