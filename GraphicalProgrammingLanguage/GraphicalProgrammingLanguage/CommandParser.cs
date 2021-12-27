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

        ShapeFactory factory = new ShapeFactory(); //to return the shapes 
        public ArrayList shapes = new ArrayList(); //stores shapes
        public Shape shape;//shape of type Shape

        public static int penX; //X-coordinate of MOVETO
        public static int penY; //Y-coordinate of MOVETO

        public static int drawToX = 0;//X1-coordinate of DRAWTO
        public static int drawToY = 0;//Y1-coordinate of DRAWTO

        public static int drawFromX = 0;//X2-coordinate of DRAWTO
        public static int drawFromY = 0;//Y2-coordinate of DRAWTO

        public static Graphics draw;
        public Color color;
        public Boolean fill;

        //public static void HighlightLine(RichTextBox inputBox, int index, Color color)
        //{
        //    inputBox.SelectAll();
        //    inputBox.SelectionBackColor = inputBox.BackColor;
        //    var lines = inputBox.Lines;
        //    if (index < 0 || index >= lines.Length)
        //        return;
        //    var start = inputBox.GetFirstCharIndexFromLine(index);  // Get the 1st char index of the appended text
        //    var length = lines[index].Length;
        //    inputBox.Select(start, length);                 // Select from there to the end
        //    inputBox.SelectionBackColor = color;
        //}

        public bool isPossibleCommand(string[] possibleCommands, string command)
        {
            if (possibleCommands.Contains((string)command.ToUpper()))
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// extracts
        /// </summary>
        /// <param name="possibleCommands"></param>
        /// <param name="dictionary"></param>
        /// <param name="errorDisplayBox"></param>
        /// <param name="drawingArea"></param>
        public void checkForKeywords(string[] possibleCommands, Dictionary<int, string> dictionary, RichTextBox errorDisplayBox, PictureBox drawingArea, RichTextBox codeArea, TextBox commandLine)
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
                    if (isPossibleCommand(possibleCommands, element) == true)
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
                                    //catch params that are not integers
                                    {
                                        displayErrorMsg(errorDisplayBox, lineNumber, "Both parameters should be of type integer", "MOVETO x,y");
                                        breakLoopFlag = 1;
                                        break;
                                    }
                                }
                                else
                                {
                                    //if wrong number of parameters are passed
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
                                            getAndAddShape(color, fill, shape, factory, (string)element.ToUpper(), shapes, penX, penY, radius);//creates and adds the shape
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
                                //check if required number of parameters are passed
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
                                        if (int.Parse(singleLine[1]) >= 0 && int.Parse(singleLine[2]) >= 0)
                                        {
                                            int height = int.Parse(singleLine[1]);
                                            int width = int.Parse(singleLine[2]); 
                                            getAndAddShape(color, fill, shape, factory, (string)element.ToUpper(), shapes, penX, penY, height, width);//creates and adds the shape
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
                                int countArrayNum = singleLine.Length;
                                if (countArrayNum - 1 == 2)
                                {
                                    try
                                    {
                                        if (int.Parse(singleLine[1]) >= 0)
                                        {
                                            int bases = int.Parse(singleLine[1]);
                                            int height = int.Parse(singleLine[2]);
                                            getAndAddShape(color, fill, shape, factory, (string)element.ToUpper(), shapes, penX, penY, bases, height);//creates and adds the shape
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
                                int countArrayNum = singleLine.Length;
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
                                            getAndAddShape(color, fill, shape, factory, (string)element.ToUpper(), shapes, drawFromX, drawFromY, drawToX, drawToY);

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

                            if ((string)element.ToUpper() == "PEN")
                            {
                                int countArrayNum = singleLine.Length;
                                if (countArrayNum - 1 == 1)
                                {

                                    if (singleLine[1].ToUpper() == "RED" || singleLine[1].ToUpper() == "YELLOW" || singleLine[1].ToUpper() == "BLUE")
                                    {

                                        if (singleLine[1].ToUpper() == "RED")
                                        {
                                            color = Color.Red;
                                        }
                                        if (singleLine[1].ToUpper() == "YELLOW")
                                        {
                                            color = Color.Yellow;
                                        }
                                        if (singleLine[1].ToUpper() == "BLUE")
                                        {
                                            color = Color.Blue;
                                        }
                                    }
                                    else
                                    {
                                        displayErrorMsg(errorDisplayBox, lineNumber, "Such color doesnt exist " , "red OR green OR blue");
                                        breakLoopFlag = 1;
                                        break;
                                    }

                                }
                                else
                                {
                                    displayErrorMsg(errorDisplayBox, lineNumber, "Wrong number of parameters for keyword DRAWTO", "PEN red");
                                    breakLoopFlag = 1;
                                    break;
                                }
                            }

                            if ((string)element.ToUpper() == "FILL")
                            {
                                string expectedMsg = " \"expected fill <boolean > \"";
                                int countArrayNum = singleLine.Length;
                                if (countArrayNum - 1 == 1)
                                {
                                    if (singleLine[1].ToUpper() == "ON" || singleLine[1].ToUpper() == "OFF")
                                    {
                                        if (singleLine[1].ToUpper() == "ON")
                                        {
                                            fill = true;
                                        }
                                        if (singleLine[1].ToUpper() == "OFF")
                                        {
                                            fill = false;
                                        }
                                    }
                                    else
                                    {
                                        displayErrorMsg(errorDisplayBox, lineNumber, "Wrong parameter for keyword FILL", "FILL on");
                                        breakLoopFlag = 1;
                                        break;
                                    }
                                }
                                else
                                {
                                    displayErrorMsg(errorDisplayBox, lineNumber, "Wrong number of parameters for keyword FILL", "FILL on");
                                    breakLoopFlag = 1;
                                    break;
                                }

                            }
                        }
                    }
                    //checks for invalid keywords
                    else
                    {
                        try
                        {
                            if (singleLine[0].ToUpper() == "RED" || singleLine[0].ToUpper() == "BLUE" || singleLine[0].ToUpper() == "YELLOW" || singleLine[0].ToUpper() == "ON" || singleLine[0].ToUpper() == "OFF")
                            {
                                displayErrorMsg(errorDisplayBox, lineNumber, "Keyword does not exist", "circle OR triangle OR rectangle OR drawto OR moveto");
                                breakLoopFlag = 1;
                                break;
                            }
                            if (element.ToUpper() == "RED" || element.ToUpper() == "YELLOW" || element.ToUpper() == "BLUE" || element.ToUpper() == "ON" || element.ToUpper() == "OFF")
                            {

                            }
                            else if (int.Parse(element) > 0)
                            {

                            }
                        }
                        //check if keyword exists
                        catch (FormatException)
                        {
                            displayErrorMsg(errorDisplayBox, lineNumber, "Keyword does not exist", "circle OR triangle OR rectangle OR drawto OR moveto");
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

            if (breakLoopFlag == 0)
            {
                errorDisplayBox.Text += "\n✔ Command executed successfully";
                commandLine.Clear();
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
            errorDisplayBox.Text += "\n⚠️ Line " + lineNumber + " : " + error + "❗  |  EXPECTED:: " + correctFormat;
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
        public void drawShapes(ArrayList shapes, Shape shape, Graphics draw, Boolean fill)
        {
            // draw all shapes stored in the 'shapes' arralist
            for (int i = 0; i < shapes.Count; i++)
            {
                //cast all shape as type "Shape"
                shape = (Shape)shapes[i];

                //checks until the end
                if (shape != null) 
                {
                    shape.draw(draw, fill); //draw the actual shape
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
        public void getAndAddShape(Color color, Boolean fill, Shape shape, ShapeFactory factory, String keyword, ArrayList shapes, params int[] list)
        {
            //get the specific shape from factory class
            shape = factory.getShape((String)keyword);

            //set the color and parameter of the shape
            shape.set(color, fill, list);

            //add shape to the array
            shapes.Add(shape);
        }
    }
}
