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
    class CheckMethod
    {
        CustomMethods custom = new CustomMethods();
        public static List<string> parameters = new List<string>(); // strores all params
        public static Dictionary<string, int> methodAndNumberOfParams = new Dictionary<string, int>(); //
        public static List<string> methodNames = new List<string>(); // stores all method name
        public static List<Tuple<string, int, string>> methodTuple = new List<Tuple<string, int, string>>(); // store methodname, lineNumber, everything inside method


        /// <summary>
        /// Check for the lines that start with the Keyword METHOD and check and display errors. If no error found then store every line in method inside a Tuple
        /// </summary>
        /// <param name="singleLine">single line from the mainDictionary</param>
        /// <param name="mainDictionary">holds info about the input</param>
        /// <param name="varDictionary">dictionary that sotres all variables</param>
        /// <param name="errorDisplayBox">to display errors</param>
        /// <param name="lineNumber">current line number</param>
        public void checkForMethods(string[] singleLine, Dictionary<int, string> mainDictionary, Dictionary<string, int> varDictionary, RichTextBox errorDisplayBox, int lineNumber)
        {

            if ((string)singleLine[0].ToUpper() == "METHOD")
            {
                //List<string> parameters = new List<string>();
                parameters.Clear();

                int countArrayNum = singleLine.Length;
                string methodName = "";
                int tempEndMethod = 0;
                CommandParser.methodLineNumber = lineNumber;
                int tempLineNumber = lineNumber;


                //check if line has minimu required syntax
                if (singleLine.Length >= 4)
                {
                    if (singleLine[2] == "(" && singleLine[singleLine.Length-1] == ")")
                    {
                        //check if method is ended
                        foreach (var row in mainDictionary)
                        {
                            if (row.Key > CommandParser.methodLineNumber)
                            {
                                if (row.Value.Trim().ToUpper() == "ENDMETHOD")
                                {
                                    CommandParser.endMethodLineNumber = row.Key;
                                    tempEndMethod = row.Key;
                                    break;
                                }

                                try
                                {
                                    if (mainDictionary[tempLineNumber + 1].Split(' ')[0].Trim().ToUpper() == "METHOD")
                                    {
                                        break;
                                    }
                                }
                                catch (KeyNotFoundException)
                                {

                                }
                                tempLineNumber++;
                            }
                        }


                        //return error if not ended
                        if (tempEndMethod == 0)
                        {
                            custom.displayErrorMsg(errorDisplayBox, lineNumber, "METHOD was never ended", "Method <method name> ( parameter list ) ....... ENDMETHOD");
                            CommandParser.breakFlag = 1;
                            CommandParser.breakLoopFlag = 1;
                        }

                        //check if method name is string
                        bool allowedName = int.TryParse(singleLine[1], out int mName);

                        if (allowedName == false)
                        {
                            methodName = singleLine[1].Trim().ToUpper();
                            methodNames.Add(methodName.ToUpper());


                            //check for parameters
                            for (int i = 3; i < singleLine.Length - 1; i++)
                            {
                                bool paramIsInt = int.TryParse(singleLine[i], out int ssd);

                                if (paramIsInt == false)
                                {
                                    //add it to the list of parameters and also the varDictionary
                                    parameters.Add(singleLine[i].Trim().ToUpper());
                                    varDictionary[singleLine[i].Trim().ToUpper()] = 0;
                                }
                                else
                                {
                                    custom.displayErrorMsg(errorDisplayBox, lineNumber, "Parameter list cannot be integers", "Method <method name> ( parameter list ) ....... ENDMETHOD");
                                    CommandParser.breakLoopFlag = 1;
                                    CommandParser.breakFlag = 1;
                                    break;
                                }
                            }

                            //set the name of the method and the number of parameters it contains
                            methodAndNumberOfParams[methodName] = parameters.Count();

                            //get methodname, lineNumber and every line inside method besides endmethod
                            foreach (var row in mainDictionary)
                            {
                                if (row.Key >= CommandParser.methodLineNumber && row.Key < CommandParser.endMethodLineNumber)
                                {
                                    methodTuple.Add(new Tuple<string, int, string>(methodName, row.Key, row.Value));
                                }
                            }
                        }
                        else
                        {
                            custom.displayErrorMsg(errorDisplayBox, lineNumber, "Method names cannot be a number", "Method <method name> ( parameter list ) ....... ENDMETHOD");
                            CommandParser.breakLoopFlag = 1;
                            CommandParser.breakFlag = 1;
                        }
                    }
                    else
                    {
                        custom.displayErrorMsg(errorDisplayBox, lineNumber, "Bracket is missing around the parameters", "Method <method name> ( parameter list ) ....... ENDMETHOD");
                        CommandParser.breakLoopFlag = 1;
                        CommandParser.breakFlag = 1;
                    }
                }
                else
                {
                    custom.displayErrorMsg(errorDisplayBox, lineNumber, "lll Wrong syntax for METHOD", "Method <method name> ( parameter list ) ....... ENDMETHOD");
                    CommandParser.breakLoopFlag = 1;
                    CommandParser.breakFlag = 1;
                }

                //make it 1 becasude we want to skip the method unless called
                CommandParser.methodConditionStatus = 1;
            }
        }
    }
}
