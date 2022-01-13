using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphicalProgrammingLanguage
{
    public class Polygon : Shape
    {
        public Polygon() : base()
        {

        }

        public Polygon(Color colour, int x, int y, int[] polyArray) : base(colour, x, y)
        {
            this.polyArray = polyArray;
        }

        public override void setPoly(Color colour, Boolean fill, bool flash, Color primaryColor, Color secondaryColor, int xAxis, int yAxis, int[] polyArray)
        {
            base.colour = colour;
            base.set(colour, fill, flash, primaryColor, secondaryColor, xAxis, yAxis);
            base.polyArray = polyArray;
        }

        public override void draw(Graphics graphics, bool fill)
        {
            //created two separate arrays to store X and Y coordinates
            int[] polyArrayX = new int[30];
            int[] polyArrayY = new int[30];

            //to store the total elements in polyArray (from base)
            int totalCoor = 0;

            //to store new coordinates
            List<Point> pointList = new List<Point>();

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

            //separating X and Y coordinates using index (i.e. even index for X and odd for Y)
            for (int index = 0; index < polyArray.Length; index++)
            {
                if (index % 2 == 0)
                {
                    polyArrayX[index] = polyArray[index];
                }
                else
                {
                    polyArrayY[index] = polyArray[index];
                }
                totalCoor++;
            }
            
            //added on of the points to be the current posi of moveTo
            pointList.Add(new Point(x, y));


            for (int index = 0; index < totalCoor; index++)
            {
                try
                {
                    //take only evenly placed elements
                    if (index % 2 == 0)
                    {
                        //doesn't work if 1 is not added to polyArrayY[index] because of the way points are stored in the list
                        pointList.Add(new Point(polyArrayX[index], polyArrayY[index + 1]));
                    }
                }
                catch (IndexOutOfRangeException)
                {
                    Debug.WriteLine("pointList.Add garda IndexOutOfRange");
                }
            }

            //convert the list to an array as DrawPolygon only accepts array
            Point[] pointArray = pointList.ToArray();

            graphics.DrawPolygon(pen, pointArray);
            graphics.FillPolygon(brush, pointArray);
        }
    }
}
 