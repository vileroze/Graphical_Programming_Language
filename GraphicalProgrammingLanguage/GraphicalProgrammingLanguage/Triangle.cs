using System;
using System.Collections.Generic;
using System.Linq;
using System.Drawing;
using System.Text;
using System.Threading.Tasks;

namespace GraphicalProgrammingLanguage
{
    public class Triangle : Shape
    {
        int hypotenuse, perpendicular;

        public Triangle() : base()
        {
        }

        public Triangle(Color colour, int x, int y, int hypotenuse, int perpendicular) : base(colour, x, y)
        {
            this.hypotenuse = hypotenuse;
            this.perpendicular = perpendicular;
        }

        public override void set(Color colour, Boolean fill, params int[] list)
        {
            base.colour = colour;
            //list[0] is x, list[1] is y, list[2] is hypotenuse, list[3] is perpendicular
            base.set(colour, fill, list[0], list[1]);
            this.hypotenuse = list[2];
            this.perpendicular = list[3];
        }

        public override void draw(Graphics graphics, Boolean fill)
        {
            Point[] point = new Point[3];
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

            //right point
            point[0].X = x + perpendicular;
            point[0].Y = y + hypotenuse;

            //top point
            point[1].X = x;
            point[1].Y = y - (hypotenuse);

            //left point
            point[2].X = x - perpendicular;
            point[2].Y = y + hypotenuse;

            graphics.DrawPolygon(pen, point);
            graphics.FillPolygon(brush, point);
        }
    }
}
