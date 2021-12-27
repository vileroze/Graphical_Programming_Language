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
        protected Color colour = Color.Black;
        protected Boolean fill;
        protected int x, y;
        public Shape()
        {

        }

        public Shape(Color colour, int x, int y)
        {
            this.colour = colour; //shape's colour
            this.x = x; //its x pos
            this.y = y; //its y pos
        }

        public abstract void draw(Graphics graphics, Boolean fill);

        public void draw(Graphics graphics, Color color)
        {
            throw new NotImplementedException();
        }

        public virtual void set(Color colour, Boolean fill, params int[] list)
        {
            this.fill = fill;
            this.colour = colour;
            this.x = list[0];
            this.y = list[1];
        }
        public override string ToString()
        {
            return base.ToString() + "    " + this.x + "," + this.y + " : ";
        }

    }
}
