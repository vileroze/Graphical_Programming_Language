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

        public DrawTo() : base()
        {

        }
        public DrawTo(Color colour, int x, int y, int xCor, int yCor) : base(colour, x, y)
        {
            this.xCor = xCor; //the only thing that is different from shape
            this.yCor = yCor;
        }

        public override void set(Color colour, Boolean fill, bool flash, Color primaryColor, Color secondaryColor, params int[] list)
        {
            base.set(colour, fill, flash, primaryColor, secondaryColor, list[0], list[1]);
            this.xCor = list[2];
            this.yCor = list[3];
        }

        public override void draw(Graphics g, Boolean fill)
        {
            Pen pen = new Pen(base.colour, 2);
            g.DrawLine(pen, x, y, xCor, yCor);
        }

        public override string ToString() //all classes inherit from object and ToString() is abstract in object
        {
            return base.ToString() + "  " + this.xCor + "  " + this.yCor;
        }
    }
}
