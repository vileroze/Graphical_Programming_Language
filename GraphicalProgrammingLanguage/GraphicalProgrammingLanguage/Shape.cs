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
        public Color colour = Color.Black;
        public  Boolean fill;
        protected int x, y;
        protected int[] polyArray = new int[40];
        public Color primaryColor;
        public Color secondaryColor;
        public bool flash;

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

        public virtual void set(Color colour, Boolean fill, bool flash, Color primaryColor, Color secondaryColor,  params int[] list)
        {
            this.fill = fill;
            this.colour = colour;
            this.x = list[0];
            this.y = list[1];

            this.primaryColor = primaryColor;
            this.secondaryColor = secondaryColor;
            this.flash = flash;
        }

        public virtual void setPoly(Color colour, Boolean fill, bool flash, Color primaryColor, Color secondaryColor, int x, int y, int[] polyArray)
        {
            this.fill = fill;
            this.colour = colour;
            this.x = x;
            this.y = y;
            this.polyArray = polyArray;
        }

        public override string ToString()
        {
            return base.ToString() + "    " + this.x + "," + this.y + " : ";
        }

    }
}
