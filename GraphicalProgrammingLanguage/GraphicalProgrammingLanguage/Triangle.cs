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
        int height, bases;

        public Triangle()
        {

        }
        public Triangle(Color colour, int x, int y, int height, int bases) : base(colour, x, y)
        {
            this.height = height; //the only thing that is different from shape
            this.bases = bases;
        }
        public override void set(Color colour, params int[] list)
        {
            //list[0] is x, list[1] is y, list[2] is height, list[3] is bases
            bases.set(colour, list[0], list[1]);
            this.height = list[2];
            this.bases = list[3];

        }

        public override void draw(Graphics g)
        {
            Pen p = new Pen(Color.Black, 2);
            SolidBrush b = new SolidBrush(colour);
            g.FillTriangle(b, x - height, y - bases, height, bases);
            g.DrawTriangle(p, x - height, y - bases, height, bases);
        }
        public override string ToString() //all classes inherit from object and ToString() is abstract in object
        {
            return bases.ToString() + "  " + this.bases + "  " + this.height;
        }


    }
}
