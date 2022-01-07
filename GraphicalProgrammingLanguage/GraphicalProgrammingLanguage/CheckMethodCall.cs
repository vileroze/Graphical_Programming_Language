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
    class CheckMethodCall
    {
        CustomMethods custom = new CustomMethods();
        CheckKeyword checkKeyword = new CheckKeyword();
        static int everythingOK = 0;
        //possibleCommands, mainDictionary, drawingArea, commandLine,
        public void checkForMethodCall(string[] possibleCommands, Dictionary<int, string> mainDictionary, PictureBox drawingArea, TextBox commandLine, string[] singleLine, Dictionary<string, int> varDictionary, RichTextBox errorDisplayBox, int lineNumber)
        {
            everythingOK = 0;
            //check for method call
            if (CheckMethod.methodNames.Contains(singleLine[0].ToUpper()))
            {
                if (singleLine.Length >= 3)
                {
                    if (singleLine[1] == "(" && singleLine[singleLine.Length - 1] == ")")
                    {
                        //if emthods with no parameter in called
                        //if (singleLine.Length == 3)
                        //{
                        //    foreach (var row in mainDictionary)
                        //    {
                        //        foreach (var tuple in CheckMethod.methodTuple)
                        //        {
                        //            Debug.WriteLine("metho call: " + "{0} - {1} - {2}", tuple.Item1, tuple.Item2.ToString(), tuple.Item3);
                        //            string[] methodCallLine = tuple.Item3.Trim().Split(new char[] { ',', ' ' }, StringSplitOptions.RemoveEmptyEntries);
                        //            checkKeyword.checkForKeywords(possibleCommands, mainDictionary, errorDisplayBox, drawingArea, commandLine, tuple.Item2, methodCallLine);
                        //        }
                        //    }
                        //}
                        //else if(singleLine.Length > 3)
                        //{
                            int numOfParamValue = 0;
                            for (int i = 2; i < singleLine.Length - 1; i++)
                            {
                                numOfParamValue++;
                            }

                            List<int> allParams = new List<int>();

                            //check if number of given parameters are equal to the parameters of method
                            if (numOfParamValue == CheckMethod.parameters.Count())
                            {
                                for (int i = 2; i < singleLine.Length - 1; i++)
                                {
                                    //if (!string.IsNullOrEmpty(singleLine[i]))
                                    //{
                                        bool paramIsInteger = int.TryParse(singleLine[i].Trim(), out int ssd);

                                        if (paramIsInteger)
                                        {
                                            allParams.Add(int.Parse(singleLine[i]));
                                            //hardcode lai pachi milaune 

                                            everythingOK = 1;
                                        }
                                        else
                                        {
                                            custom.displayErrorMsg(errorDisplayBox, lineNumber, "Parameter list must be only integers", "<method name> ( parameters )");
                                            CommandParser.breakLoopFlag = 1;
                                            CommandParser.breakFlag = 1;
                                            break;
                                        }
                                    //}
                                }

                                foreach (int pp in allParams)
                                {
                                    Debug.WriteLine("allParams: " + pp);
                                }

                                if (everythingOK == 1)
                                {
                                    for (int ii = 0; ii < CheckMethod.parameters.Count(); ii++)
                                    {
                                        varDictionary[CheckMethod.parameters[ii].ToUpper()] = allParams[ii];
                                    }

                                    foreach (var tuple in CheckMethod.methodTuple)
                                    {
                                        Debug.WriteLine("metho call: " + "{0} - {1} - {2}", tuple.Item1, tuple.Item2.ToString(), tuple.Item3);
                                        string[] methodCallLine = tuple.Item3.Trim().Split(new char[] { ',', ' ' }, StringSplitOptions.RemoveEmptyEntries);
                                        checkKeyword.checkForKeywords(possibleCommands, mainDictionary, errorDisplayBox, drawingArea, commandLine, tuple.Item2, methodCallLine);
                                    }
                                }
                            }
                            else
                            {
                                custom.displayErrorMsg(errorDisplayBox, lineNumber, "Number of prameters are not equal", "<method name> ( parameters )");
                                CommandParser.breakLoopFlag = 1;
                            }
                        //}
                        
                    }
                    else
                    {
                        custom.displayErrorMsg(errorDisplayBox, lineNumber, "Wrong syntax for METHOD calling", "<method name> ( parameters )");
                        CommandParser.breakLoopFlag = 1;
                        //break;
                    }
                }
                else
                {
                    custom.displayErrorMsg(errorDisplayBox, lineNumber, "Wrong syntax for METHOD calling", "<method name> ( parameters )");
                    CommandParser.breakLoopFlag = 1;
                }
            }
            else
            {
                custom.displayErrorMsg(errorDisplayBox, lineNumber, "Keyword does not exist", "circle OR triangle OR rectangle OR drawto OR moveto");
                CommandParser.breakLoopFlag = 1;
                //break;
            }
        }
    }
}
