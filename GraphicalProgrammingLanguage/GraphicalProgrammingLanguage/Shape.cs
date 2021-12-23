using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphicalProgrammingLanguage
{
    public abstract class Shape : ShapeInterface
    {
        protected Color colour; //shape's colour
        protected int x, y; //not I could use c# properties for this

        public Shape()
        {

        }

        public Shape(Color colour, int x, int y)
        {
            this.colour = colour; //shape's colour
            this.x = x; //its x pos
            this.y = y; //its y pos
        }

        public abstract void draw(Graphics g);

        public virtual void set(Color c, params int[] list)
        {
            this.colour = c;
            this.x = list[0];
            this.y = list[1];
        }
        public override string ToString()
        {
            return base.ToString() + "    " + this.x + "," + this.y + " : ";
        }
    }
}
