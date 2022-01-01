using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
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

        public int[] polyArray;

        public static Graphics draw;
        public Color color;
        public Boolean fill;


        static int endIfLineNumber = 0;
        static int ifLineNumber = 0;
        public static int ifConditionStatus= 0;
        public Dictionary<string, int> varDictionary = new Dictionary<string, int>();


        /// <summary>
        /// checks if command passed is within possibleCommand array
        /// </summary>
        /// <param name="possibleCommands">all possible commands</param>
        /// <param name="command">the command to be checked</param>
        /// <returns>returns true if the array passed contains the command passed and vice versa</returns>
        public bool isPossibleCommand(string[] possibleCommands, string command)
        {
            if (possibleCommands.Contains((string)command.ToUpper()))
            {
                return true;
            }
            return false;
        }


        /// <summary>
        /// extract lineNUmber and all commands from data dictionary, catches and displays errors, displays shapes and reads commandLine
        /// </summary>
        /// <param name="possibleCommands">all possible commands</param>
        /// <param name="dictionary">dictionary that holds</param>
        /// <param name="errorDisplayBox">textBox to display all errors</param>
        /// <param name="drawingArea">pictureBox to display all shapes</param>
        /// <param name="commandLine">get instance of single line command</param>
        public void checkForKeywords(string[] possibleCommands, Dictionary<int, string> mainDictionary, RichTextBox errorDisplayBox, PictureBox drawingArea, TextBox commandLine)
        {
            //flag to break the outer foreach loop
            int breakLoopFlag = 0;
            ifConditionStatus= 0;
            
            foreach (KeyValuePair<int, string> pair in mainDictionary)
            {
                if (ifConditionStatus == 1)
                {
                    if (pair.Key < endIfLineNumber && pair.Key > ifLineNumber)
                    {
                        continue;
                    }
                }

                ifConditionStatus = 0;
                //strips value from dictionary splits it using delimeters then stores the result
                string[] singleLine = pair.Value.Trim().Split(new char[] { ',', ' ' }, StringSplitOptions.RemoveEmptyEntries);
                
                //key acts as line number
                int lineNumber = pair.Key;

                foreach (string element in singleLine)
                {
                    //checks if singleLine[0] is one of the possible commands
                    // || singleLine[1] == "="
                    if (isPossibleCommand(possibleCommands, singleLine[0]) == true || singleLine[1] == "=")
                    {
                        if ((string)singleLine[0].ToUpper() == "IF")
                        {
                            int countArrayNum = singleLine.Length;
                            int varToCompare = 0;
                            string compOperator = "";
                            int lastElement = 0;
                            //int endIfLineNumber = 0;
                            //int ifLineNumber = lineNumber;
                            ifLineNumber = lineNumber;

                            //check if line has 3 components beside the actual keyword
                            if (countArrayNum - 1 == 3)
                            {
                                //get the line number of ENDIF
                                foreach (var row in mainDictionary)
                                {
                                    //ENDIF bhetena bhane k garne
                                    if (row.Value.ToUpper() == "ENDIF")
                                    {
                                        endIfLineNumber = row.Key;
                                    }
                                }

                                //get the value of the variable
                                string[] parameter = getValueFromDictionary(varDictionary, singleLine[1]);
                                try
                                {
                                    //check if the value of variable is positive
                                    if (int.Parse(parameter[0]) >= 0)
                                    {
                                        varToCompare = int.Parse(parameter[0]);

                                        //check if statement has "=="
                                        //|| singleLine[2].ToUpper() == ">=" || singleLine[2].ToUpper() == "<=" || singleLine[2].ToUpper() == "!="
                                        if (singleLine[2].ToUpper() == "==")
                                        {
                                            compOperator = singleLine[2];

                                            //check if RHS element is an integer
                                            try
                                            {
                                                string[] rhsParam = getValueFromDictionary(varDictionary, singleLine[3]);
                                                lastElement = int.Parse(rhsParam[0]);

                                                //check if condition not true (aru condition() check garne using SWITCH CASE or IF ELSE)
                                                if (varToCompare == lastElement)
                                                {

                                                }
                                                else
                                                {
                                                    errorDisplayBox.Text += "\nIF condition false";
                                                    ifConditionStatus = 1;
                                                    //break;

                                                    int tempLinenumber = lineNumber + 1;

                                                }
                                            }
                                            catch (FormatException)
                                            {
                                                displayErrorMsg(errorDisplayBox, lineNumber, "right hand side of == should be number", "IF <variable> == <some integer value>");
                                                breakLoopFlag = 1;
                                                break;
                                            }
                                        }
                                        else
                                        {
                                            displayErrorMsg(errorDisplayBox, lineNumber, "Wrong comparison operator used", "== OR <= OR >= OR !=");
                                            breakLoopFlag = 1;
                                            break;
                                        }
                                    }
                                }
                                catch (FormatException)
                                {
                                    displayErrorMsg(errorDisplayBox, lineNumber, "left hand side of == should be number", "IF <variable> == <some integer value>");
                                    breakLoopFlag = 1;
                                    break;
                                }
                            }
                            else
                            {
                                displayErrorMsg(errorDisplayBox, lineNumber, "Wrong syntax for IF statement", "IF <variable> == <some integer value>");
                                breakLoopFlag = 1;
                                break;
                            }
                        }
                        ///////////////////////////////////////

                        //if (ifConditionStatus== 1)
                        //{
                        //    break;
                        //}










                        //checks for moveto
                        if ((string)singleLine[0].ToUpper() == "MOVETO")
                        {
                            //gets the current length of line
                            int countArrayNum = singleLine.Length;
                            // checks if the required number of parameters are met (i.e. array.length - 1 = number of parameters)
                            if (countArrayNum - 1 == 2)
                            {
                                string[] parameter = getValueFromDictionary(varDictionary, singleLine[1], singleLine[2]);
                                //checks if both the parameters passed are integers
                                try
                                {
                                    if (int.Parse(parameter[0]) >= 0 && int.Parse(parameter[1]) >= 0)
                                    {
                                        //stores params as coordinates
                                        penX = int.Parse(parameter[0]);
                                        penY = int.Parse(parameter[1]);
                                    }

                                }
                                catch (IndexOutOfRangeException)
                                {
                                    break;
                                }
                                catch (FormatException)
                                //catch params that are not integers
                                {
                                    displayErrorMsg(errorDisplayBox, lineNumber, "Both parameters should be positive integer", "MOVETO x,y");
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
                        else if ((string)singleLine[0].ToUpper() == "CIRCLE")
                        {
                            //gets the current length of line
                            int countArrayNum = singleLine.Length;
                            // checks if the required number of parameters are met (i.e. array.length - 1 = number of parameters)
                            if (countArrayNum - 1 == 1)
                            {
                                string[] parameter = getValueFromDictionary(varDictionary, singleLine[1]);
                                try
                                {
                                    if (int.Parse(parameter[0]) >= 0)
                                    {
                                        int radius = int.Parse(parameter[0]); // stores radius
                                        getAndAddShape(color, fill, factory, (string)singleLine[0].ToUpper(), shapes, penX, penY, radius);//creates and adds the shape
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
                        else if ((string)singleLine[0].ToUpper() == "RECTANGLE")
                        {
                            //gets the current length of line
                            int countArrayNum = singleLine.Length;
                            // checks if the required number of parameters are met (i.e. array.length - 1 = number of parameters)
                            if (countArrayNum - 1 == 2)
                            {
                                string[] parameter = getValueFromDictionary(varDictionary, singleLine[1], singleLine[2]);
                                try
                                {
                                    if (int.Parse(parameter[0]) >= 0 && int.Parse(parameter[1]) >= 0)
                                    {
                                        int height = int.Parse(parameter[0]);
                                        int width = int.Parse(parameter[1]);
                                        getAndAddShape(color, fill, factory, (string)singleLine[0].ToUpper(), shapes, penX, penY, height, width);//creates and adds the shape
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
                        else if ((string)singleLine[0].ToUpper() == "TRIANGLE")
                        {
                            int countArrayNum = singleLine.Length;
                            if (countArrayNum - 1 == 2)
                            {
                                string[] parameter = getValueFromDictionary(varDictionary, singleLine[1], singleLine[2]);
                                try
                                {
                                    if (int.Parse(parameter[0]) >= 0 && int.Parse(parameter[1]) >= 0)
                                    {
                                        int bases = int.Parse(parameter[0]);
                                        int height = int.Parse(parameter[1]);
                                        getAndAddShape(color, fill, factory, (string)singleLine[0].ToUpper(), shapes, penX, penY, bases, height);//creates and adds the shape
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

                        else if ((string)singleLine[0].ToUpper() == "POLYGON")
                        {
                            int countArrayNum = singleLine.Length;
                            shape = factory.getShape((string)singleLine[0].ToUpper());

                            if (countArrayNum - 1 >= 2)
                            {
                                polyArray = new int[countArrayNum - 1];
                                //errorDisplayBox.Text += "\npoly array size : " + polyArray.Length;

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
                                        if (int.Parse(parameter[index]) >= 0)
                                        {
                                            polyArray[index] = int.Parse(parameter[index]);
                                        }
                                    }
                                    catch (ArgumentNullException)
                                    {
                                        displayErrorMsg(errorDisplayBox, lineNumber, "ARGUMENT NULL EXCEPTION", "");
                                        breakLoopFlag = 1;
                                        break;
                                    }
                                }

                                shape.setPoly(color, fill, penX, penY, polyArray);
                                shapes.Add(shape);
                            }
                            else
                            {
                                displayErrorMsg(errorDisplayBox, lineNumber, "Wrong number of parameters for keyword POLYGON", "POLYGON  23,2,32,5");
                                breakLoopFlag = 1;
                                break;
                            }
                        }

                        //checks for DRAWTO
                        else if ((string)singleLine[0].ToUpper() == "DRAWTO")
                        {
                            int countArrayNum = singleLine.Length;
                            if (countArrayNum - 1 == 2)
                            {
                                string[] parameter = getValueFromDictionary(varDictionary, singleLine[1], singleLine[2]);
                                try
                                {
                                    if (int.Parse(singleLine[1]) >= 0 && int.Parse(singleLine[2]) >= 0)
                                    {
                                        //if 'moveTo' is written before 'drawTo'
                                        int drawFromX = penX;
                                        int drawFromY = penY;

                                        //store params
                                        int drawToX = int.Parse(parameter[0]);
                                        int drawToY = int.Parse(parameter[1]);

                                        //the new co-ordinates for MOVETO params 
                                        penX = drawToX;
                                        penY = drawToY;

                                        //creates and adds the shape
                                        getAndAddShape(color, fill, factory, (string)singleLine[0].ToUpper(), shapes, drawFromX, drawFromY, drawToX, drawToY);

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


                        else if ((string)singleLine[0].ToUpper() == "PEN")
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
                                    displayErrorMsg(errorDisplayBox, lineNumber, "Such color doesnt exist ", "red OR green OR blue");
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

                        else if ((string)singleLine[0].ToUpper() == "FILL")
                        {
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

                        try
                        {
                            //variable chalna chodyo bhane else hataune if matra rakhne
                            if (singleLine[1] == "=")
                            //checks for variabless
                            {
                                int indexOfEqualsSign = Array.IndexOf(singleLine, "="); //var = 10+20+30
                                string varName = "";
                                string output = ""; //to store all the things that need to be calculated

                                //create string to calculate value to put in the dictionary
                                string[] varValueArray = new string[100];
                                Array.Copy(singleLine, indexOfEqualsSign + 1, varValueArray, 0, singleLine.Length - 2);
                                foreach (string input in varValueArray)
                                {
                                    //stores something like : 10+20+30 OR height+20
                                    output += input;
                                }

                                //replace variable with its value
                                string[] splitOutput = output.Split(new char[] { '+', '-', '/', '*' });

                                foreach (string operand in splitOutput)
                                {
                                    string opp = operand.Trim().ToUpper();
                                    if (varDictionary.ContainsKey(opp))
                                    {
                                        int valueOfOperand = varDictionary[opp];
                                        output = output.Replace(operand, valueOfOperand.ToString());
                                    }
                                }

                                //compute variable name and value for dictionary
                                try
                                {
                                    var result = new DataTable().Compute(output, null);
                                    varName = singleLine[indexOfEqualsSign - 1];

                                    //check if result returns a positive integer
                                    if (Convert.ToInt32(result) >= 0)
                                    {
                                        //store the result
                                        int varValue = Convert.ToInt32(result);

                                        //check if variable already exists
                                        if (varDictionary.ContainsKey(varName.Trim().ToUpper()))
                                        {
                                            //update value
                                            varDictionary[varName.Trim().ToUpper()] = varValue;
                                        }
                                        else
                                        {
                                            //add value
                                            varDictionary.Add(varName.Trim().ToUpper(), varValue);
                                        }
                                    }
                                }
                                catch (FormatException)
                                {
                                    displayErrorMsg(errorDisplayBox, lineNumber, "variable names cannot be a number", "VAR count = 10");
                                    breakLoopFlag = 1;
                                    break;
                                }
                            }
                        }
                        catch (IndexOutOfRangeException)
                        {

                        }
                    }
                    else
                    //checks for invalid keywords
                    {
                        displayErrorMsg(errorDisplayBox, lineNumber, "Keyword does not exist", "circle OR triangle OR rectangle OR drawto OR moveto");
                        breakLoopFlag = 1;
                        break;
                    }
                }
                //|| ifConditionStatus== 1
                if (breakLoopFlag == 1 )
                {
                    break;
                }
            }

            //just for debugging purposes
            Debug.WriteLine("\n=============");
            foreach (KeyValuePair<string, int> dict in varDictionary)
            {
                Debug.WriteLine("\nkey: " + dict.Key + " value: " + dict.Value);

            }

            if (breakLoopFlag == 0)
            {
                errorDisplayBox.Text += "\n✔ Command executed successfully";
                commandLine.Clear();
            }

            //reset dictionary and pictureBox
            drawingArea.Refresh();
            mainDictionary.Clear();
        }


        /// <summary>
        /// displays the error message along with line number and the correct format in the specified textBox with some extra formatting
        /// </summary>
        /// <param name="errorDisplayBox">richTextBox to display errors</param>
        /// <param name="lineNumber">the error of the line number</param>
        /// <param name="error">the name of the error</param>
        /// <param name="correctFormat">the correct format of the command</param>
        public void displayErrorMsg(RichTextBox errorDisplayBox, int lineNumber, String error,  String correctFormat)
        {
            errorDisplayBox.Text += "\n⚠️ Line " + lineNumber + " : " + error + "❗  |" + "  EXPECTED:: " + correctFormat;
        }


        /// <summary>
        /// draws the current position of the MOVETO co-ordinates
        /// </summary>
        /// <param name="e">paint event argument to cal the Graphics object</param>
        /// <param name="penX">x-coordinate</param>
        /// <param name="penY">y-coordinate</param>
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
        /// <param name="shapes">array that stores the shapes</param>
        /// <param name="shape">the actual shape</param>
        /// <param name="draw">Graphics instance to draw the shape</param>
        /// <param name="fill">fill state of shape</param>
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
        ///  gets the shapes from the factory class and stores it in a variable called shape of type Shape. Then sets the color and parameter of the shapes and finally adds it to the arraylist
        /// </summary>
        /// <param name="color">color of the shape</param>
        /// <param name="fill">specifies the fill state of the shape</param>
        /// <param name="factory"> returns the shape</param>
        /// <param name="keyword">the shape to be returned</param>
        /// <param name="shapes">arraylis to store the shapes</param>
        /// <param name="list">list of parameters for the given shape</param>
        public void getAndAddShape(Color color, Boolean fill, ShapeFactory factory, String keyword, ArrayList shapes, params int[] list)
        {
            //get the specific shape from factory class
            shape = factory.getShape((String)keyword);

            //set the color and parameter of the shape
            shape.set(color, fill, list);

            //add shape to the array
            shapes.Add(shape);
        }



        
        public static string[] getValueFromDictionary( Dictionary<string, int> varDictionary, params string[] list)
        {
            string[] ParamNumList = new string[100];

            for (int i = 0; i<list.Length;i++)
            {
                string tempVar = list[i].Trim().ToUpper();

                if (varDictionary.ContainsKey(tempVar))
                {
                    int valueOfOperand = varDictionary[tempVar];
                    ParamNumList[i] = tempVar.Replace(tempVar, valueOfOperand.ToString());
                }
                else
                {
                    ParamNumList[i] = tempVar;
                }
            }
            return ParamNumList;
        }
    }
}
