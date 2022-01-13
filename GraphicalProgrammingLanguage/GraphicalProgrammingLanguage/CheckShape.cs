using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GraphicalProgrammingLanguage
{
    class CheckShape
    {
        CustomMethods custom = new CustomMethods();
        public static int checkIfFlashIsCalled = 0;

        public static bool flash = false;
        public static Color primaryColor;
        public static Color secondaryColor;

        public void checkForShape(string[] singleLine, Dictionary<string, int> varDictionary, ShapeFactory factory, ArrayList shapes,  RichTextBox errorDisplayBox, int lineNumber, Shape shape, int[] polyArray)
        {

            //checks for moveto
            if ((string)singleLine[0].ToUpper() == "MOVETO")
            {
                //gets the current length of line
                int countArrayNum = singleLine.Length;
                // checks if the required number of parameters are met (i.e. array.length - 1 = number of parameters)
                if (countArrayNum - 1 == 2)
                {
                    string[] parameter = CustomMethods.getValueFromDictionary(varDictionary, singleLine[1], singleLine[2]);
                    //checks if both the parameters passed are integers
                    try
                    {
                        if (isPositiveNumber(int.Parse(parameter[0])) && isPositiveNumber(int.Parse(parameter[1])))
                        {
                            //stores params as coordinates
                            CommandParser.penX = int.Parse(parameter[0]);
                            CommandParser.penY = int.Parse(parameter[1]);
                        }

                    }
                    catch (IndexOutOfRangeException)
                    {
                        CommandParser.breakLoopFlag = 1;
                    }
                    catch (FormatException)
                    //catch params that are not integers
                    {
                        custom.displayErrorMsg(errorDisplayBox, lineNumber, "Both parameters should be integer", "MOVETO x,y");
                        CommandParser.breakLoopFlag = 1;
                        CommandParser.breakFlag = 1;
                    }
                    catch (NegativeNumberException)
                    {
                        custom.displayErrorMsg(errorDisplayBox, lineNumber, "Parameters should be positive integer", "MOVETO x,y");
                        CommandParser.breakLoopFlag = 1;
                        CommandParser.breakFlag = 1;
                    }
                }
                else
                {
                    //if wrong number of parameters are passed
                    custom.displayErrorMsg(errorDisplayBox, lineNumber, "Wrong number of parameters for keyword MOVETO", "MOVETO x,y");
                    CommandParser.breakLoopFlag = 1;
                    CommandParser.breakFlag = 1;
                }
            }


            if ((string)singleLine[0].ToUpper() == "CIRCLE")
            {
                //gets the current length of line
                int countArrayNum = singleLine.Length;
                // checks if the required number of parameters are met (i.e. array.length - 1 = number of parameters)
                if (countArrayNum - 1 == 1)
                {
                    string[] parameter = CustomMethods.getValueFromDictionary(varDictionary, singleLine[1]);
                    try
                    {
                        if (isPositiveNumber(int.Parse(parameter[0])))
                        {
                            int radius = int.Parse(parameter[0]); // stores radius
                            custom.getAndAddShape(CommandParser.color, CommandParser.fill, flash, primaryColor, secondaryColor, factory, (string)singleLine[0].ToUpper(), shapes, CommandParser.penX, CommandParser.penY, radius);//creates and adds the shape
                        }
                    }
                    catch (IndexOutOfRangeException) 
                    { 
                        CommandParser.breakLoopFlag = 1; 
                    }
                    catch (FormatException)
                    {
                        custom.displayErrorMsg(errorDisplayBox, lineNumber, "Parameter should be of type integer", "CIRCLE radius");
                        CommandParser.breakLoopFlag = 1;
                        CommandParser.breakFlag = 1;
                    }
                    catch (NegativeNumberException)
                    {
                        custom.displayErrorMsg(errorDisplayBox, lineNumber, "Parameters should be positive integer", "CIRCLE radius");
                        CommandParser.breakLoopFlag = 1;
                        CommandParser.breakFlag = 1;
                    }
                }
                //check if required number of parameters are passed
                else
                {
                    custom.displayErrorMsg(errorDisplayBox, lineNumber, "Wrong number of parameters for keyword CIRCLE", "CIRCLE radius");
                    
                    CommandParser.breakLoopFlag = 1;
                    CommandParser.breakFlag = 1;
                }
            }


            //checks for RECTANGLE
            if ((string)singleLine[0].ToUpper() == "RECTANGLE")
            {
                //gets the current length of line
                int countArrayNum = singleLine.Length;
                // checks if the required number of parameters are met (i.e. array.length - 1 = number of parameters)
                if (countArrayNum - 1 == 2)
                {
                    string[] parameter = CustomMethods.getValueFromDictionary(varDictionary, singleLine[1], singleLine[2]);
                    try
                    {
                        //isPositiveNumber(int.Parse(parameter[0]));
                        //if (int.Parse(parameter[0]) >= 0 && int.Parse(parameter[1]) >= 0)
                        if (isPositiveNumber(int.Parse(parameter[0])) && isPositiveNumber(int.Parse(parameter[1])))
                        {
                            int height = int.Parse(parameter[0]);
                            int width = int.Parse(parameter[1]);
                            custom.getAndAddShape(CommandParser.color, CommandParser.fill, flash, primaryColor, secondaryColor, factory, (string)singleLine[0].ToUpper(), shapes, CommandParser.penX, CommandParser.penY, height, width);//creates and adds the shape
                        }
                    }
                    catch (IndexOutOfRangeException) 
                    { 
                        CommandParser.breakLoopFlag = 1; 
                    }
                    catch (FormatException)
                    {
                        custom.displayErrorMsg(errorDisplayBox, lineNumber, "Both parameters should be of type integer", "RECTANGLE height, width");
                        CommandParser.breakLoopFlag = 1;
                        CommandParser.breakFlag = 1;
                    }
                    catch (NegativeNumberException)
                    {
                        custom.displayErrorMsg(errorDisplayBox, lineNumber, "Parameters should be positive integer", "RECTANGLE height, width");
                        CommandParser.breakLoopFlag = 1;
                        CommandParser.breakFlag = 1;
                    }
                }
                else
                {
                    custom.displayErrorMsg(errorDisplayBox, lineNumber, "Wrong number of parameters for keyword RECTANGLE", "RECTANGLE height, width");
                    CommandParser.breakLoopFlag = 1;
                    CommandParser.breakFlag = 1;
                }
            }

            //checks for TRIANGLE
            if ((string)singleLine[0].ToUpper() == "TRIANGLE")
            {
                int countArrayNum = singleLine.Length;
                if (countArrayNum - 1 == 2)
                {
                    string[] parameter = CustomMethods.getValueFromDictionary(varDictionary, singleLine[1], singleLine[2]);
                    try
                    {
                        if (isPositiveNumber(int.Parse(parameter[0])) && isPositiveNumber(int.Parse(parameter[1])))
                        {
                            int bases = int.Parse(parameter[0]);
                            int height = int.Parse(parameter[1]);
                            custom.getAndAddShape(CommandParser.color, CommandParser.fill, flash, primaryColor, secondaryColor, factory, (string)singleLine[0].ToUpper(), shapes, CommandParser.penX, CommandParser.penY, bases, height);//creates and adds the shape
                        }

                    }
                    catch (IndexOutOfRangeException) 
                    { 
                        CommandParser.breakLoopFlag = 1; 
                    }
                    catch (FormatException)
                    {
                        custom.displayErrorMsg(errorDisplayBox, lineNumber, "Both parameters should be of type integer", "TRIANGLE base, height");
                        CommandParser.breakLoopFlag = 1;
                        CommandParser.breakFlag = 1;
                    }
                    catch (NegativeNumberException)
                    {
                        custom.displayErrorMsg(errorDisplayBox, lineNumber, "Parameters should be positive integer", "TRIANGLE base, height");
                        CommandParser.breakLoopFlag = 1;
                        CommandParser.breakFlag = 1;
                    }
                }
                else
                {
                    custom.displayErrorMsg(errorDisplayBox, lineNumber, "Wrong number of parameters for keyword TRIANGLE", "TRIANGLE base, height");
                    CommandParser.breakLoopFlag = 1;
                    CommandParser.breakFlag = 1;
                }
            }


            if ((string)singleLine[0].ToUpper() == "POLYGON")
            {
                int countArrayNum = singleLine.Length;
                shape = factory.getShape((string)singleLine[0].ToUpper());

                //must have atleast 4 coordinates
                if ((((countArrayNum - 1) % 2) == 0) && (countArrayNum - 1 >= 4))
                {
                    polyArray = new int[countArrayNum - 1];

                    string[] parameter = new string[singleLine.Length - 1];
                    //errorDisplayBox.Text += "\nparameter array size : " + parameter.Length;

                    for (int index = 0; index < singleLine.Length - 1; index++)
                    {
                        string tempVar = singleLine[index + 1].Trim().ToUpper();

                        if (varDictionary.ContainsKey(tempVar))
                        {
                            int valueOfOperand = varDictionary[tempVar];
                            parameter[index] = tempVar.Replace(tempVar, valueOfOperand.ToString());
                        }
                        else
                        {
                            parameter[index] = tempVar;
                        }
                    }

                    for (int index = 0; index < parameter.Length; index++)
                    {
                        try
                        {
                            if (isPositiveNumber(int.Parse(parameter[0])))
                            {
                                polyArray[index] = int.Parse(parameter[index]);
                            }
                        }
                        catch (ArgumentNullException)
                        {
                            custom.displayErrorMsg(errorDisplayBox, lineNumber, "ARGUMENT NULL EXCEPTION", "");
                            CommandParser.breakLoopFlag = 1;
                            CommandParser.breakFlag = 1;
                        }
                        catch (FormatException)
                        {
                            custom.displayErrorMsg(errorDisplayBox, lineNumber, "Parameter '" + parameter[index] + "' was never declared", "");
                            CommandParser.breakLoopFlag = 1;
                            CommandParser.breakFlag = 1;
                        }
                        catch (NegativeNumberException)
                        {
                            custom.displayErrorMsg(errorDisplayBox, lineNumber, "Parameters should be positive integer", "Polygon x,y,x,y");
                            CommandParser.breakLoopFlag = 1;
                            CommandParser.breakFlag = 1;
                        }
                    }

                    shape.setPoly(CommandParser.color, CommandParser.fill, flash, primaryColor, secondaryColor, CommandParser.penX, CommandParser.penY, polyArray);
                    shapes.Add(shape);
                }
                else
                {
                    custom.displayErrorMsg(errorDisplayBox, lineNumber, "Wrong number of parameters for keyword POLYGON", "POLYGON  23,2,32,5 (must be in pairs (i.e divisible by two))");
                    CommandParser.breakLoopFlag = 1;
                    CommandParser.breakFlag = 1;
                }
            }

            //checks for DRAWTO
            if ((string)singleLine[0].ToUpper() == "DRAWTO")
            {
                int countArrayNum = singleLine.Length;
                if (countArrayNum - 1 == 2)
                {
                    string[] parameter = CustomMethods.getValueFromDictionary(varDictionary, singleLine[1], singleLine[2]);
                    try
                    {
                        //isPositiveNumber(int.Parse(parameter[0]))
                        if (isPositiveNumber(int.Parse(singleLine[1])) && isPositiveNumber(int.Parse(singleLine[2])))
                        {
                            //if 'moveTo' is written before 'drawTo'
                            int drawFromX = CommandParser.penX;
                            int drawFromY = CommandParser.penY;

                            //store params
                            int drawToX = int.Parse(parameter[0]);
                            int drawToY = int.Parse(parameter[1]);

                            //the new co-ordinates for MOVETO params 
                            CommandParser.penX = drawToX;
                            CommandParser.penY = drawToY;

                            //creates and adds the shape
                            custom.getAndAddShape(CommandParser.color, CommandParser.fill, flash, primaryColor, secondaryColor, factory, (string)singleLine[0].ToUpper(), shapes, drawFromX, drawFromY, drawToX, drawToY);

                            //makes the ending point of the previous drawTo the starting point of the next drawTo
                            drawFromX = drawToX;
                            drawFromY = drawToY;
                        }
                    }
                    catch (IndexOutOfRangeException) 
                    { 
                        CommandParser.breakLoopFlag = 1; 
                    }
                    catch (FormatException)
                    {
                        custom.displayErrorMsg(errorDisplayBox, lineNumber, "Both parameters should be of type integer", "DRAWTO x,y");
                        CommandParser.breakLoopFlag = 1;
                        CommandParser.breakFlag = 1;
                    }
                    catch (NegativeNumberException)
                    {
                        custom.displayErrorMsg(errorDisplayBox, lineNumber, "Parameters should be positive integer", "DRAWTO x,y");
                        CommandParser.breakLoopFlag = 1;
                        CommandParser.breakFlag = 1;
                    }
                }
                else
                {
                    custom.displayErrorMsg(errorDisplayBox, lineNumber, "Wrong number of parameters for keyword DRAWTO", "DRAWTO x,y");
                    CommandParser.breakLoopFlag = 1;
                    CommandParser.breakFlag = 1;
                }
            }

            if ((string)singleLine[0].ToUpper() == "PEN")
            {
                int countArrayNum = singleLine.Length;
                if (countArrayNum - 1 == 1)
                {

                    if (singleLine[1].ToUpper() == "RED" || singleLine[1].ToUpper() == "YELLOW" || singleLine[1].ToUpper() == "BLUE" || singleLine[1].ToUpper() == "REDGREEN" || singleLine[1].ToUpper() == "BLACKWHITE")
                    {

                        if (singleLine[1].ToUpper() == "RED")
                        {
                            CommandParser.color = Color.Red;
                        }

                        if (singleLine[1].ToUpper() == "YELLOW")
                        {
                            CommandParser.color = Color.Yellow;
                        }

                        if (singleLine[1].ToUpper() == "BLUE")
                        {
                            CommandParser.color = Color.Blue;
                        }

                        if (singleLine[1].ToUpper() == "REDGREEN")
                        {
                            primaryColor = Color.Red;
                            secondaryColor = Color.Green;
                            flash = true;
                        }

                        if (singleLine[1].ToUpper() == "BLACKWHITE")
                        {
                            primaryColor = Color.Black;
                            secondaryColor = Color.White;
                            flash = true;
                        }
                    }
                    else
                    {
                        custom.displayErrorMsg(errorDisplayBox, lineNumber, "Such color doesnt exist ", "red OR green OR blue");
                        CommandParser.breakLoopFlag = 1;
                        CommandParser.breakFlag = 1;
                    }
                }
                else
                {
                    custom.displayErrorMsg(errorDisplayBox, lineNumber, "Wrong number of parameters for keyword DRAWTO", "PEN red");
                    CommandParser.breakLoopFlag = 1;
                    CommandParser.breakFlag = 1;
                }
            }


            if ((string)singleLine[0].ToUpper() == "FILL")
            {
                int countArrayNum = singleLine.Length;
                if (countArrayNum - 1 == 1)
                {
                    if (singleLine[1].ToUpper() == "ON" || singleLine[1].ToUpper() == "OFF")
                    {
                        if (singleLine[1].ToUpper() == "ON")
                        {
                            CommandParser.fill = true;
                        }
                        if (singleLine[1].ToUpper() == "OFF")
                        {
                            CommandParser.fill = false;
                        }
                    }
                    else
                    {
                        custom.displayErrorMsg(errorDisplayBox, lineNumber, "Wrong parameter for keyword FILL", "FILL on");
                        CommandParser.breakLoopFlag = 1;
                        CommandParser.breakFlag = 1;
                    }
                }
                else
                {
                    custom.displayErrorMsg(errorDisplayBox, lineNumber, "Wrong number of parameters for keyword FILL", "FILL on");
                    CommandParser.breakLoopFlag = 1;
                    CommandParser.breakFlag = 1;
                }
            }
        }


        /// <summary>
        /// method that throws user defined exception if negative number is passed
        /// </summary>
        /// <param name="param">an integer</param>
        /// <returns></returns>
        static bool isPositiveNumber(int param)
        {
            if (param < 0)
            {
                throw new NegativeNumberException("The prameter has to be a positive number");
            }
            else
            {
                return true;
            }
        }

    }
}
