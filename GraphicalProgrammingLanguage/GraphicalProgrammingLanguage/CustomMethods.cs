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
    public class CustomMethods
    {
        public Shape shape;//shape of type Shape

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
        /// displays the error message along with line number and the correct format in the specified textBox with some extra formatting
        /// </summary>
        /// <param name="errorDisplayBox">richTextBox to display errors</param>
        /// <param name="lineNumber">the error of the line number</param>
        /// <param name="error">the name of the error</param>
        /// <param name="correctFormat">the correct format of the command</param>
        public void displayErrorMsg(RichTextBox errorDisplayBox, int lineNumber, String error, String correctFormat)
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
            Pen p = new Pen(Color.Green, 1);
            movePos.DrawRectangle(p, penX - (4 / 2), penY - (4 / 2), 4, 4);
            movePos.FillRectangle(new SolidBrush(Color.Green), penX - (4 / 2), penY - (4 / 2), 4, 4);
        }


        /// <summary>
        ///  gets the shapes from the factory class and stores it in a variable called shape of type Shape. Then sets the color and parameter of the shapes and finally adds it to the arraylist
        /// </summary>
        /// <param name="color">color of the shape</param>
        /// <param name="fill">specifies the fill state of the shape</param>
        /// <param name="flash">flash state of shape</param>
        /// <param name="primaryColor">primary color for flash</param>
        /// <param name="secondaryColor">secondary color for flash</param>
        /// <param name="factory"> returns the shape</param>
        /// <param name="keyword">the shape to be returned</param>
        /// <param name="shapes">arraylis to store the shapes</param>
        /// <param name="list">list of parameters for the given shape (e.g. height, width, radius etc)</param>
        public void getAndAddShape(Color color, Boolean fill, bool flash, Color primaryColor, Color secondaryColor, ShapeFactory factory, String keyword, ArrayList shapes, params int[] list)
        {
            //get the specific shape from factory class
            shape = factory.getShape((String)keyword);

            //set the color and parameter of the shape
            shape.set(color, fill, flash, primaryColor, secondaryColor, list);

            //add shape to the array
            shapes.Add(shape);
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
        /// checks if the dictionary contains the list passed, if it does returns its numerical value , if not returns the list as is
        /// </summary>
        /// <param name="varDictionary">dictionary that contains all variables and its value</param>
        /// <param name="list">all variables that we need the value of</param>
        /// <returns></returns>
        public static string[] getValueFromDictionary(Dictionary<string, int> varDictionary, params string[] list)
        {
            string[] ParamNumList = new string[100];

            for (int i = 0; i < list.Length; i++)
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


        /// <summary>
        /// checks if the dictionary passed contains a WHILE statement
        /// </summary>
        /// <param name="dictionary">dictionary to be checked</param>
        /// <returns></returns>
        public Boolean hasWhile(Dictionary<int, string> dictionary)
        {
            Boolean whileExists = true;
            foreach (KeyValuePair<int, string> res in dictionary)
            {
                string allLines = "";
                allLines += res.Value;

                string[] alllinesArray = allLines.Trim().ToUpper().Split(new char[] { ' ', '+', '=', '+', '-', '/', '*', '<', '>', '!', ',' },
                                                                                StringSplitOptions.RemoveEmptyEntries);
                if (alllinesArray.Contains("WHILE")) { whileExists = true; break; }
                else { whileExists = false; }
            }
            return whileExists;
        }


        /// <summary>
        /// returns true if any of the condition mathces with the predefined IF conditions else returns false
        /// </summary>
        /// <param name="opp">the opertor to be used in the comparison</param>
        /// <param name="varToCompare">one of the operands (LHS)</param>
        /// <param name="lastElement">other operand (RHS)</param>
        /// <returns></returns>
        public Boolean checkForOperator(string opp, int varToCompare , int lastElement)
        {
            Boolean chk = false;
            if (opp.ToUpper() == "==")
            {
                if (varToCompare == lastElement)
                {
                    chk = true;
                }
            }
            else if (opp.ToUpper() == ">")
            {
                if (varToCompare > lastElement)
                {
                    chk = true;
                }
            }
            else if (opp.ToUpper() == "<")
            {
                if (varToCompare < lastElement)
                {
                    chk = true;
                }
            }
            else if (opp.ToUpper() == ">=")
            {
                if (varToCompare >= lastElement)
                {
                    chk = true;
                }
            }
            else if (opp.ToUpper() == "<=")
            {
                if (varToCompare <= lastElement)
                {
                    chk = true;
                }
            }
            else if (opp.ToUpper() == "!=")
            {
                if (varToCompare != lastElement)
                {
                    chk = true;
                }
            }
            else
            {
                chk = false;
            }

            return chk;
        }
    }
}
