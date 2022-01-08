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
    class CheckKeyword
    {
        CheckShape checkShape = new CheckShape();
        CheckMethod checkMethod = new CheckMethod();
        CheckConditionalStatements checkStatement = new CheckConditionalStatements();
        CustomMethods custom = new CustomMethods();

        ShapeFactory factory = new ShapeFactory(); //to return the shapes 
        public static ArrayList shapes = new ArrayList(); //stores shapes
        public Shape shape; //shape of type Shape
        public int[] polyArray; //store all points of polygon

        /// <summary>
        /// extract lineNUmber and all commands from data dictionary, catches and displays errors, displays shapes and reads commandLine
        /// </summary>
        /// <param name="possibleCommands">all possible commands</param>
        /// <param name="dictionary">dictionary that holds</param>
        /// <param name="errorDisplayBox">textBox to display all errors</param>
        /// <param name="drawingArea">pictureBox to display all shapes</param>
        /// <param name="commandLine">get instance of single line command</param>
        public void checkForKeywords(string[] possibleCommands, Dictionary<int, string> mainDictionary, RichTextBox errorDisplayBox, int lineNumber, string[] singleLine)
        {
            foreach (string element in singleLine)
            {
                //checks if singleLine[0] is one of the possible commands
                if (custom.isPossibleCommand(possibleCommands, singleLine[0]) == true)
                {
                    // check for conditional statements
                    checkStatement.checkForConditionalStatements(singleLine, mainDictionary, CommandParser.varDictionary, errorDisplayBox, lineNumber);

                    //check for keywords
                    checkShape.checkForShape(singleLine, CommandParser.varDictionary, factory, shapes, errorDisplayBox, lineNumber, shape, polyArray);
                }

                //break foreach loop
                if (CommandParser.breakFlag == 1)
                {
                    break;
                }

            }
        }
    }
}
