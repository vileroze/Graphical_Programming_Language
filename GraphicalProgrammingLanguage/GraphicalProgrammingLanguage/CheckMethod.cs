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
        public static List<string> parameters = new List<string>();
        public static List<string> methodNames = new List<string>();

        //mthodname, lineNumber, everything inside method
        public static List<Tuple<string, int, string>> methodTuple = new List<Tuple<string, int, string>>();

        public void checkForMethods(string[] singleLine, Dictionary<int, string> mainDictionary, Dictionary<string, int> varDictionary, RichTextBox errorDisplayBox, int lineNumber)
        {

            if ((string)singleLine[0].ToUpper() == "METHOD")
            {
                
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
                            methodName = singleLine[1];
                            methodNames.Add(methodName.ToUpper());

                            //if emthods with no parameter in called
                            //if (singleLine.Length == 4)
                            //{
                            //    Debug.WriteLine("parameter chaina: ");
                            //    foreach (var row in mainDictionary)
                            //    {
                            //        if (row.Key > CommandParser.methodLineNumber && row.Key < CommandParser.endMethodLineNumber)
                            //        {
                            //            methodTuple.Add(new Tuple<string, int, string>(methodName, row.Key, row.Value));
                            //        }
                            //    }
                            //}
                            //else if(singleLine.Length > 4)
                           // {

                                for (int i = 3; i < singleLine.Length - 1; i++)
                                {
                                    //if (!string.IsNullOrEmpty(singleLine[i]))
                                    //{
                                        bool paramIsInt = int.TryParse(singleLine[i], out int ssd);

                                        if (paramIsInt == false)
                                        {
                                            //Debug.WriteLine("singleLine[i]: " + singleLine[i]);
                                            parameters.Add(singleLine[i].ToUpper());
                                            varDictionary[singleLine[i].ToUpper()] = 0;
                                        }
                                        else
                                        {
                                            custom.displayErrorMsg(errorDisplayBox, lineNumber, "Parameter list cannot be integers", "Method <method name> ( parameter list ) ....... ENDMETHOD");
                                            CommandParser.breakLoopFlag = 1;
                                            CommandParser.breakFlag = 1;
                                            break;
                                        }
                                    //}
                                }

                            Debug.WriteLine("parameters: " + parameters.Count);

                                //only distinct parameters
                                parameters = parameters.Distinct().ToList();

                                //get methodname, lineNumber, everything inside method
                                foreach (var row in mainDictionary)
                                {
                                    if (row.Key > CommandParser.methodLineNumber && row.Key < CommandParser.endMethodLineNumber)
                                    {
                                        methodTuple.Add(new Tuple<string, int, string>(methodName, row.Key, row.Value));
                                    }
                                }
                            //}
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
                CommandParser.methodConditionStatus = 1;
            }
        }
    }
}
