using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphicalProgrammingLanguage
{
    class Rectangle : Shape
    {
        int width, height;

        public Rectangle()
        {
        }

        public Rectangle(Color colour, int x, int y, int width, int height) : base(colour, x, y)
        {
            this.width = width; //the only thing that is different from shape
            this.height = height;
        }

        public override void set(Color colour, Boolean fill, bool flash, Color primaryColor, Color secondaryColor, params int[] list)
        {
            base.colour = colour;
            //list[0] is x, list[1] is y, list[2] is width, list[3] is height
            base.set(colour, fill, flash, primaryColor, secondaryColor, list[0], list[1]);
            this.width = list[2];
            this.height = list[3];
        }

        public override void draw(Graphics graphics, Boolean fill)
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
            graphics.FillRectangle(brush, x - width, y - height, width * 2, height * 2);
            graphics.DrawRectangle(pen, x - width, y - height, width * 2, height * 2);
        }

        public override string ToString() //all classes inherit from object and ToString() is abstract in object
        {
            return base.ToString() + "  " + this.height + "  " + this.width;
        }
    }
}
