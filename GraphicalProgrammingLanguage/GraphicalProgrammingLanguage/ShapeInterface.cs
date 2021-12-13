using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphicalProgrammingLanguage
{
    interface ShapeInterface
    {
        void set(Color c, params int[] list); // no need to declare the size of array if "params" is used
        void draw(Graphics g);
    }
}
