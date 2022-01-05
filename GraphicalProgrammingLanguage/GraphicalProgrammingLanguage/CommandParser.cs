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
        public static ArrayList shapes = new ArrayList(); //stores shapes
        public Shape shape;//shape of type Shape

        public static int penX; //X-coordinate of MOVETO
        public static int penY; //Y-coordinate of MOVETO

        public static int drawToX = 0;//X1-coordinate of DRAWTO
        public static int drawToY = 0;//Y1-coordinate of DRAWTO

        public static int drawFromX = 0;//X2-coordinate of DRAWTO
        public static int drawFromY = 0;//Y2-coordinate of DRAWTO

        public int[] polyArray;

        public static Graphics draw;
        public static Color color = Color.Black;
        public static bool fill;

        public static int endIfLineNumber = 0;
        public static int ifLineNumber = 0;
        public static int ifConditionStatus = 0;

        public static int endLoopLineNumber = 0;
        public static int whileLineNumber = 0;
        public static int whileConditionStatus = 0;

        public static int breakLoopFlag = 0; //flag to break the outer foreach loop
        public static int breakFlag = 0; //flag to break the main for loop of mainDictionary
        public static int lineNumber = 0;

        string[] singleLine;

        public Dictionary<string, int> varDictionary = new Dictionary<string, int>();

        CustomMethods custom = new CustomMethods();
        CheckVariable checkVar = new CheckVariable();
        CheckConditionalStatements checkStatement = new CheckConditionalStatements();
        CheckShape checkShape = new CheckShape();



        /// <summary>
        /// extract lineNUmber and all commands from data dictionary, catches and displays errors, displays shapes and reads commandLine
        /// </summary>
        /// <param name="possibleCommands">all possible commands</param>
        /// <param name="dictionary">dictionary that holds</param>
        /// <param name="errorDisplayBox">textBox to display all errors</param>
        /// <param name="drawingArea">pictureBox to display all shapes</param>
        /// <param name="commandLine">get instance of single line command</param>
        public void checkForKeywords(string[] possibleCommands, Dictionary<int, string> mainDictionary, RichTextBox errorDisplayBox, PictureBox drawingArea, TextBox commandLine, int lineNumber, string[] singleLine)
        {
            foreach (string element in singleLine)
            {
                //checks if singleLine[0] is one of the possible commands
                if (custom.isPossibleCommand(possibleCommands, singleLine[0]) == true)
                {

                    // check for conditional statements
                    checkStatement.checkForConditionalStatements(singleLine, mainDictionary, varDictionary, errorDisplayBox, lineNumber);
                    
                    //if (CommandParser.whileConditionStatus == 0)
                    //{
                    //    Debug.WriteLine("endLoopLineNumber : " + CommandParser.endLoopLineNumber);
                    //    Debug.WriteLine("tempEndLoop : " + endLoopLineNumber);
                    //    //checkVar.checkForVariables(singleLine, varDictionary, errorDisplayBox, lineNumber);
                    //    checkStatement.checkForConditionalStatements(singleLine, mainDictionary, varDictionary, errorDisplayBox, lineNumber);
                    //}

                    //check for keywords
                    checkShape.checkForShape(singleLine, varDictionary, factory, shapes, errorDisplayBox, lineNumber, shape, polyArray);
                }

                //break foreach loop
                if (breakFlag == 1)
                {
                    break;
                }
            
            }
        }


        public void mainParser(string[] possibleCommands, Dictionary<int, string> mainDictionary, RichTextBox errorDisplayBox, PictureBox drawingArea, TextBox commandLine)
        {
            ifConditionStatus = 0;
            whileConditionStatus = 0;

            while (whileConditionStatus ==  0)
            {
                foreach (KeyValuePair<int, string> pair in mainDictionary)
                {
                    if (CheckConditionalStatements.checkLoops == 1 && (pair.Key < whileLineNumber || pair.Key > endLoopLineNumber))
                    {
                        continue;
                    }

                    if (ifConditionStatus == 1 && (pair.Key > ifLineNumber && pair.Key <= endIfLineNumber))
                    {
                        continue;
                    }

                    if (whileConditionStatus == 1 && (pair.Key > whileLineNumber && pair.Key <= endLoopLineNumber))
                    {
                        continue;
                    }



                    lineNumber = pair.Key;
                    singleLine = pair.Value.Trim().Split(new char[] { ',', ' ' }, StringSplitOptions.RemoveEmptyEntries);

                    if (pair.Value.Length != 0)
                    {
                        if (possibleCommands.Contains(pair.Value.Split(' ')[0].Trim().ToUpper()))
                        {
                            //all shapes
                            checkForKeywords(possibleCommands, mainDictionary, errorDisplayBox, drawingArea, commandLine, lineNumber, singleLine);
                        }
                        else
                        {
                            //check for variables
                            checkVar.checkForVariables(singleLine, varDictionary, errorDisplayBox, lineNumber);
                        }

                        //breaks the loop for mainDictionary
                        if (breakLoopFlag == 1)
                        {
                            break;
                        }
                    }

                }
            }

            

            Debug.WriteLine("\n=============");
            foreach (KeyValuePair<string, int> dict in varDictionary)
            {
                Debug.WriteLine("\nkey: " + dict.Key + " value: " + dict.Value);
            }

            //display success message if no errors
            if (breakLoopFlag == 0)
            {
                int length = errorDisplayBox.TextLength;
                string displayMessasge = "\n✔ Command executed successfully";// at end of text
                errorDisplayBox.AppendText(displayMessasge);
                errorDisplayBox.SelectionStart = length;
                errorDisplayBox.SelectionLength = displayMessasge.Length;
                errorDisplayBox.SelectionColor = Color.Green;
                commandLine.Clear();
            }

            //reset dictionary and pictureBox
            drawingArea.Refresh();
            mainDictionary.Clear();
        }
    }
}
