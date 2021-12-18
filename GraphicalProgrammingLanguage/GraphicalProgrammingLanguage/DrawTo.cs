using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphicalProgrammingLanguage
{
    class DrawTo : Shape
    {
        int xCor, yCor;

        public DrawTo()
        {

        }
        public DrawTo(Color colour, int x, int y, int xCor, int yCor) : base(colour, x, y)
        {
            this.xCor = xCor; //the only thing that is different from shape
            this.yCor = yCor;
        }
        public override void set(Color colour, params int[] list)
        {
            //list[0] is x, list[1] is y, list[2] is width, list[3] is height
            base.set(colour, list[0], list[1]);
            this.xCor = list[2];
            this.yCor = list[3];

        }

        public override void draw(Graphics g)
        {
            Pen p = new Pen(Color.Black, 2);
            SolidBrush b = new SolidBrush(colour);
            g.DrawLine(p, x, y, xCor, yCor);
        }
        public override string ToString() //all classes inherit from object and ToString() is abstract in object
        {
            return base.ToString() + "  " + this.xCor + "  " + this.yCor;
        }
    }
}
