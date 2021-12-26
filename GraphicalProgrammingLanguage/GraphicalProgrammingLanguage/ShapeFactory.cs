﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphicalProgrammingLanguage
{
    public class ShapeFactory
    {
        public Shape getShape(String shapeType)// parameter for shape passed
        {
            shapeType = shapeType.ToUpper().Trim(); //yoi could argue that you want a specific word string to create an object but I'm allowing any case combination


            if (shapeType.Equals("CIRCLE"))
            {
                
                return new Circle();

            }
            else if (shapeType.Equals("RECTANGLE"))
            {
                return new Rectangle();

            }
            else if (shapeType.Equals("TRIANGLE"))
            {
                return new Triangle();

            }
            else if (shapeType.Equals("DRAWTO"))
            {
                return new DrawTo();

            }
            else
            {
                throw new System.ArgumentException("Factory error: " + shapeType + " does not exist");
            }

        }
    }
}
