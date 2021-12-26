using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GraphicalProgrammingLanguage
{
    public class CommandParser
    {

        //Form1 ko varaible haru
        ShapeFactory factory = new ShapeFactory(); //to return the shapes 
        public ArrayList shapes = new ArrayList(); //stores shapes
        public Shape shape;

        //public static int lineNumber;

        public static int penX; //X-coordinate of MOVETO
        public static int penY; //Y-coordinate of MOVETO

        public static int drawToX = 0;//X1-coordinate of DRAWTO
        public static int drawToY = 0;//Y1-coordinate of DRAWTO

        public static int drawFromX = 0;//X2-coordinate of DRAWTO
        public static int drawFromY = 0;//Y2-coordinate of DRAWTO

        public static Graphics draw;

        /// <summary>
        /// extracts
        /// </summary>
        /// <param name="possibleCommands"></param>
        /// <param name="dictionary"></param>
        /// <param name="errorDisplayBox"></param>
        /// <param name="drawingArea"></param>
        public void checkForKeywords(string[] possibleCommands, Dictionary<int, string> dictionary, RichTextBox errorDisplayBox, PictureBox drawingArea)
        {
            //flag to break the outer foreach loop
            int breakLoopFlag = 0;
            foreach (KeyValuePair<int, string> pair in dictionary)
            {
                int commandInstance = 0;
                //strips value from dictionary splits it using delimeters then stores the result
                string[] singleLine = pair.Value.Trim().Split(new char[] { ',', ' ' }, StringSplitOptions.RemoveEmptyEntries);
                
                //key acts as line number
                int lineNumber = pair.Key;

                foreach (string element in singleLine)
                {
                    //checks if element is one of the possible commands
                    if (possibleCommands.Contains((string)element.ToUpper()))
                    {
                        //increases if command is found
                        commandInstance++;
                        // checks if a single line has multiple commands, then dispays error if it does
                        if (commandInstance > 1)
                        {
                            errorDisplayBox.Text += "\n Cannot enter more than 1 command in this line : " + lineNumber + " ";
                            commandInstance = 0;
                            breakLoopFlag = 1;
                            break;
                        }
                        else
                        {
                            //checks for moveto
                            if ((string)element.ToUpper() == "MOVETO")
                            {
                                //gets the current length of line
                                int countArrayNum = singleLine.Length;
                                // checks if the required number of parameters are met (i.e. array.length - 1 = number of parameters)
                                if (countArrayNum - 1 == 2)
                                {
                                    //checks if both the parameters passed are integers
                                    try
                                    {
                                        if (int.Parse(singleLine[1]) >= 0 && int.Parse(singleLine[2]) >= 0)
                                        {
                                            //stores params as coordinates
                                            penX = int.Parse(singleLine[1]); 
                                            penY = int.Parse(singleLine[2]);
                                        }
                                    }
                                    catch (IndexOutOfRangeException) { break; }
                                    catch (FormatException)
                                    {
                                        displayErrorMsg(errorDisplayBox, lineNumber, "Both parameters should be of type integer", "MOVETO x,y");
                                        breakLoopFlag = 1;
                                        break;
                                    }
                                }
                                else
                                {
                                    displayErrorMsg(errorDisplayBox, lineNumber, "Wrong number of parameters for keyword MOVETO", "MOVETO x,y");
                                    breakLoopFlag = 1;
                                    break;
                                }
                            }

                            //checks for CIRCLE
                            if ((string)element.ToUpper() == "CIRCLE")
                            {
                                //gets the current length of line
                                int countArrayNum = singleLine.Length;
                                // checks if the required number of parameters are met (i.e. array.length - 1 = number of parameters)
                                if (countArrayNum - 1 == 1)
                                {
                                    try
                                    {
                                        if (int.Parse(singleLine[1]) >= 0)
                                        {
                                            int radius = int.Parse(singleLine[1]); // stores radius
                                            getAndAddShape(shape, factory, (string)element.ToUpper(), shapes, penX, penY, radius);//creates and adds the shape

                                        }
                                    }
                                    catch (IndexOutOfRangeException) { break; }
                                    catch (FormatException)
                                    {
                                        displayErrorMsg(errorDisplayBox, lineNumber, "Parameter should be of type integer", "CIRCLE radius");
                                        breakLoopFlag = 1;
                                        break;
                                    }
                                }
                                else
                                {
                                    displayErrorMsg(errorDisplayBox, lineNumber, "Wrong number of parameters for keyword CIRCLE", "CIRCLE radius");
                                    breakLoopFlag = 1;
                                    break;
                                }
                            }

                            //checks for RECTANGLE
                            if ((string)element.ToUpper() == "RECTANGLE")
                            {
                                //gets the current length of line
                                int countArrayNum = singleLine.Length;
                                // checks if the required number of parameters are met (i.e. array.length - 1 = number of parameters)
                                if (countArrayNum - 1 == 2)
                                {
                                    try
                                    {
                                        if (int.Parse(singleLine[1]) >= 0)
                                        {
                                            int height = int.Parse(singleLine[1]); // adds index as integer on codeSplitArrayList
                                            int width = int.Parse(singleLine[2]); // adds index as integer on codeSplitArrayList
                                            getAndAddShape(shape, factory, (string)element.ToUpper(), shapes, penX, penY, height, width);//creates and adds the shape
                                        }

                                    }
                                    catch (IndexOutOfRangeException) { break; }
                                    catch (FormatException)
                                    {
                                        displayErrorMsg(errorDisplayBox, lineNumber, "Both parameters should be of type integer", "RECTANGLE height, width");
                                        breakLoopFlag = 1;
                                        break;
                                    }
                                }
                                else
                                {
                                    displayErrorMsg(errorDisplayBox, lineNumber, "Wrong number of parameters for keyword RECTANGLE", "RECTANGLE height, width");
                                    breakLoopFlag = 1;
                                    break;
                                }
                            }

                            //checks for TRIANGLE
                            if ((string)element.ToUpper() == "TRIANGLE")
                            {
                                //gets the current length of line
                                int countArrayNum = singleLine.Length;
                                // checks if the required number of parameters are met (i.e. array.length - 1 = number of parameters)
                                if (countArrayNum - 1 == 2)
                                {
                                    try
                                    {
                                        if (int.Parse(singleLine[1]) >= 0)
                                        {
                                            int bases = int.Parse(singleLine[1]); // adds index as integer on codeSplitArrayList
                                            int height = int.Parse(singleLine[2]); // adds index as integer on codeSplitArrayList
                                            getAndAddShape(shape, factory, (string)element.ToUpper(), shapes, penX, penY, bases, height);//creates and adds the shape
                                        }

                                    }
                                    catch (IndexOutOfRangeException) { break; }
                                    catch (FormatException)
                                    {
                                        displayErrorMsg(errorDisplayBox, lineNumber, "Both parameters should be of type integer", "TRIANGLE base, height");
                                        breakLoopFlag = 1;
                                        break;
                                    }
                                }
                                else
                                {
                                    displayErrorMsg(errorDisplayBox, lineNumber, "Wrong number of parameters for keyword TRIANGLE", "TRIANGLE base, height");
                                    breakLoopFlag = 1;
                                    break;
                                }
                            }

                            //checks for DRAWTO
                            if ((string)element.ToUpper() == "DRAWTO")
                            {
                                //gets the current length of line
                                int countArrayNum = singleLine.Length;
                                // checks if the required number of parameters are met (i.e. array.length - 1 = number of parameters)
                                if (countArrayNum - 1 == 2)
                                {
                                    try
                                    {
                                        if (int.Parse(singleLine[1]) >= 0 && int.Parse(singleLine[2]) >= 0)
                                        {
                                            //if 'moveTo' is written before 'drawTo'
                                            int drawFromX = penX;
                                            int drawFromY = penY;

                                            //store params
                                            int drawToX = int.Parse(singleLine[1]);
                                            int drawToY = int.Parse(singleLine[2]);

                                            //the new co-ordinates for MOVETO params 
                                            penX = drawToX;
                                            penY = drawToY;

                                            //creates and adds the shape
                                            getAndAddShape(shape, factory, (string)element.ToUpper(), shapes, drawFromX, drawFromY, drawToX, drawToY);

                                            //makes the ending point of the previous drawTo the starting point of the next drawTo
                                            drawFromX = drawToX;
                                            drawFromY = drawToY;
                                        }
                                    }
                                    catch (IndexOutOfRangeException) { break; }
                                    catch (FormatException)
                                    {
                                        displayErrorMsg(errorDisplayBox, lineNumber, "Both parameters should be of type integer", "DRAWTO x,y");
                                        breakLoopFlag = 1;
                                        break;
                                    }
                                }
                                else
                                {
                                    displayErrorMsg(errorDisplayBox, lineNumber, "Wrong number of parameters for keyword DRAWTO", "DRAWTO x,y");
                                    breakLoopFlag = 1;
                                    break;
                                }
                            }
                        }
                    }
                    else
                    {
                        //checks for invalid keywords
                        try
                        {
                            if (int.Parse(element) > 0)
                            {
                                //do nothing if element is an integer
                            }
                        }
                        catch (FormatException)
                        {
                            displayErrorMsg(errorDisplayBox, lineNumber, "Keyword does not exist", "circle or triangle or rectangle or drawto or moveto");
                            breakLoopFlag = 1;
                            break;
                        }
                    }
                }
                if (breakLoopFlag == 1)
                {
                    break;
                }
            }
            //reset dictionary and pictureBox
            drawingArea.Refresh();
            dictionary.Clear();
        }


        /// <summary>
        /// displays the error message along with line number and the correct format in the specified textBox with some extra formatting
        /// </summary>
        /// <param name="errorDisplayBox"></param>
        /// <param name="lineNumber"></param>
        /// <param name="error"></param>
        /// <param name="correctFormat"></param>
        public void displayErrorMsg(RichTextBox errorDisplayBox, int lineNumber, String error,  String correctFormat)
        {
            errorDisplayBox.Text += "\nLine " + lineNumber + " : " + error + "  |  EXPECTED:: " + correctFormat;
        }


        /// <summary>
        /// draws the current position of the MOVETO co-ordinates
        /// </summary>
        /// <param name="e"></param>
        /// <param name="penX"></param>
        /// <param name="penY"></param>
        public void drawCurrMoveToPos(PaintEventArgs e, int penX, int penY)
        {
            Graphics movePos = e.Graphics;
            //color of pen
            Pen p = new Pen(Color.Red, 1);
            movePos.DrawRectangle(p, penX - (4 / 2), penY - (4 / 2), 4, 4);
            movePos.FillRectangle(new SolidBrush(Color.Red), penX - (4 / 2), penY - (4 / 2), 4, 4);
        }


        /// <summary>
        /// takes eah shape from arraylist and casts it to the type of 'Shape' and uses graphics to draw the actual shape
        /// </summary>
        /// <param name="shapes"></param>
        /// <param name="s"></param>
        /// <param name="draw"></param>
        public void drawShapes(ArrayList shapes, Shape s, Graphics draw)
        {
            // draw all shapes stored in the 'shapes' arralist
            for (int i = 0; i < shapes.Count; i++)
            {
                //cast all shape as type "Shape"
                s = (Shape)shapes[i];

                //checks until the end
                if (s != null) 
                {
                    s.draw(draw); //draw the actual shape
                }
            }
        }


        /// <summary>
        /// gets the shapes from the factory class and stores it in a variable called shape of type Shape. Then sets the color and parameter of the shapes and finally adds it to the arraylist
        /// </summary>
        /// <param name="s"></param>
        /// <param name="factory"></param>
        /// <param name="keyword"></param>
        /// <param name="shapes"></param>
        /// <param name="list"></param>
        public void getAndAddShape(Shape s, ShapeFactory factory, String keyword, ArrayList shapes, params int[] list)
        {
            //get the specific shape from factory class
            s = factory.getShape((String)keyword);

            //set the color of shape
            Color circleColour = Color.Transparent;

            //set the color and parameter of the shape
            s.set(circleColour, list);

            //add shape to the array
            shapes.Add(s);
        }
    }
}
