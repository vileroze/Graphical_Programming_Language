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
        public static int penX; //X-coordinate of MOVETO
        public static int penY; //Y-coordinate of MOVETO

        public static int drawToX = 0;//X1-coordinate of DRAWTO
        public static int drawToY = 0;//Y1-coordinate of DRAWTO

        public static int drawFromX = 0;//X2-coordinate of DRAWTO
        public static int drawFromY = 0;//Y2-coordinate of DRAWTO

        public static Graphics draw;
        public static Color color = Color.Black;
        public static bool fill;

        public static int endIfLineNumber = 0;
        public static int ifLineNumber = 0;
        public static int ifConditionStatus = 0;

        public static int endLoopLineNumber = 0;
        public static int whileLineNumber = 0;
        public static int whileConditionStatus = 0;

        public static int endMethodLineNumber = 0;
        public static int methodLineNumber = 0;
        public static int methodConditionStatus = 0;
        

        public static int breakLoopFlag = 0; //flag to break the outer foreach loop
        public static int breakFlag = 0; //flag to break the main for loop of mainDictionary
        public static int lineNumber = 0;

        CheckKeyword checkKeyword = new CheckKeyword();
        CheckMethod checkMethod = new CheckMethod();
        CheckMethodCall checkCall = new CheckMethodCall();

        public static string[] singleLine;

        // kei bgiryo bhane static hataune
        public static Dictionary<string, int> varDictionary = new Dictionary<string, int>();
        //xxxxxxx
        //public ConcreteCollection varCollection = new ConcreteCollection();

        CustomMethods custom = new CustomMethods();
        CheckVariable checkVar = new CheckVariable();
        
        static int breakWhileLoop = 0;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="possibleCommands">array of all possible commands</param>
        /// <param name="complexCommands">array that contains all complex commands like METHOD</param>
        /// <param name="mainDictionary">dictionary that holds each line to be executed</param>
        /// <param name="errorDisplayBox">textBox to display all errors</param>
        /// <param name="drawingArea">pictureBox where all the shapes are drawn</param>
        /// <param name="commandLine">textbox at the bottom of the codeArea</param>
        public void mainParser(string[] possibleCommands, string[] complexCommands, Dictionary<int, string> mainDictionary, RichTextBox errorDisplayBox, PictureBox drawingArea, TextBox commandLine)
        {
            ifConditionStatus = 0;
            whileConditionStatus = 0;
            methodConditionStatus = 0;
            bool containsLoop = custom.hasWhile(mainDictionary);

            //checks if input has a WHILE loop
            if (containsLoop)
            {
                while (whileConditionStatus == 0)
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
                                checkKeyword.checkForKeywords(possibleCommands, mainDictionary, errorDisplayBox, lineNumber, singleLine);
                            }
                            else if (complexCommands.Contains(pair.Value.Split(' ')[0].Trim().ToUpper()))
                            {
                                //check for methods
                                checkMethod.checkForMethods(singleLine, mainDictionary, CommandParser.varDictionary, errorDisplayBox, lineNumber);
                            }
                            else if (CheckMethod.methodNames.Contains(pair.Value.Split(' ')[0].Trim().ToUpper()))
                            {
                                //check for method call
                                checkCall.checkForMethodCall(possibleCommands, mainDictionary, singleLine, varDictionary, errorDisplayBox, lineNumber);
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

                    //breaks the loop for mainDictionary
                    if (breakLoopFlag == 1)
                    {
                        break;
                    }
                }

                //for input without loop 
                loopWithoutWhile(possibleCommands, complexCommands, mainDictionary, errorDisplayBox);
            }
            else
            {
                //for input without loop 
                loopWithoutWhile(possibleCommands, complexCommands, mainDictionary, errorDisplayBox);
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


        /// <summary>
        /// defines what should be done if no WHILE LOOP is detected
        /// </summary>
        /// <param name="possibleCommands"></param>
        /// <param name="complexCommands"></param>
        /// <param name="mainDictionary"></param>
        /// <param name="errorDisplayBox"></param>
        public void loopWithoutWhile(string[] possibleCommands, string[] complexCommands, Dictionary<int, string> mainDictionary, RichTextBox errorDisplayBox)
        {
            foreach (KeyValuePair<int, string> pair in mainDictionary)
            {
                if (CommandParser.ifConditionStatus == 1 && (pair.Key > CommandParser.ifLineNumber && pair.Key <= CommandParser.endIfLineNumber))
                {
                    continue;
                }

                if (CommandParser.methodConditionStatus == 1 && (pair.Key > CommandParser.methodLineNumber && pair.Key <= CommandParser.endMethodLineNumber))
                {
                    continue;
                }

                CommandParser.lineNumber = pair.Key;
                CommandParser.singleLine = pair.Value.Trim().Split(new char[] { ',', ' ' }, StringSplitOptions.RemoveEmptyEntries);

                if (pair.Value.Length != 0)
                {
                    if (possibleCommands.Contains(pair.Value.Split(' ')[0].Trim().ToUpper()))
                    {
                        //all shapes
                        checkKeyword.checkForKeywords(possibleCommands, mainDictionary, errorDisplayBox, CommandParser.lineNumber, CommandParser.singleLine);
                    }
                    else if (complexCommands.Contains(pair.Value.Split(' ')[0].Trim().ToUpper()))
                    {
                        checkMethod.checkForMethods(CommandParser.singleLine, mainDictionary, CommandParser.varDictionary, errorDisplayBox, CommandParser.lineNumber);
                    }
                    else if (CheckMethod.methodNames.Contains(pair.Value.Split(' ')[0].Trim().ToUpper()))
                    {
                        checkCall.checkForMethodCall(possibleCommands, mainDictionary, CommandParser.singleLine, CommandParser.varDictionary, errorDisplayBox, CommandParser.lineNumber);
                    }
                    else
                    {
                        //check for variables
                        checkVar.checkForVariables(CommandParser.singleLine, CommandParser.varDictionary, errorDisplayBox, CommandParser.lineNumber);
                    }

                    //breaks the loop for mainDictionary
                    if (CommandParser.breakLoopFlag == 1)
                    {
                        break;
                    }
                }
            }
        }
    }
}
