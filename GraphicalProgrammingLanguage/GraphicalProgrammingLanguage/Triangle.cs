using System;
using System.Collections.Generic;
using System.Linq;
using System.Drawing;
using System.Text;
using System.Threading.Tasks;

namespace GraphicalProgrammingLanguage
{
    class Triangle : Shape
    {
        int hypotenuse;
        int perpendicular;
        public Triangle()
        {

        }
        public Triangle(Color colour, int x, int y, int hypotenuse, int perpendicular) : base(colour, x, y)
        {
            this.hypotenuse = hypotenuse;
            this.perpendicular = perpendicular;
        }
        public override void set(Color colour, params int[] list)
        {
            //list[0] is x, list[1] is y, list[2] is hypotenuse, list[3] is perpendicular
            base.set(colour, list[0], list[1]);
            this.hypotenuse = list[2];
            this.perpendicular = list[3];

        }

        public override void draw(Graphics g)
        {
            Point[] pnt = new Point[3];
            Pen p = new Pen(Color.Black, 2);
            SolidBrush b = new SolidBrush(colour);

            //right point
            pnt[0].X = x + perpendicular;
            pnt[0].Y = y + hypotenuse;

            //top point
            pnt[1].X = x;
            pnt[1].Y = y - (hypotenuse);

            //left point
            pnt[2].X = x - perpendicular;
            pnt[2].Y = y + hypotenuse;

            g.DrawPolygon(p, pnt);
            g.FillPolygon(b, pnt);
            // g.DrawPolygon(p, x, y, width, height);
        }
    }
}
