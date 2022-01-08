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
        static int everythingOK = 0; //flag set if thera are no errors

        /// <summary>
        /// Checks if the previously declared methods are called again on a later line. If called, checks for syntax and displays error, if none found  sends every line inside 
        /// the tuple to be parsed again including the line that declares the method
        /// </summary>
        /// <param name="possibleCommands"></param>
        /// <param name="mainDictionary"></param>
        /// <param name="singleLine"></param>
        /// <param name="varDictionary"></param>
        /// <param name="errorDisplayBox"></param>
        /// <param name="lineNumber"></param>
        public void checkForMethodCall(string[] possibleCommands, Dictionary<int, string> mainDictionary, string[] singleLine, Dictionary<string, int> varDictionary, RichTextBox errorDisplayBox, int lineNumber)
        {
            everythingOK = 0;

            //check for method call
            if (CheckMethod.methodNames.Contains(singleLine[0].ToUpper()))
            {
                string methodName = singleLine[0].Trim().ToUpper();

                //check for syntax errors
                if (singleLine.Length >= 3)
                {
                    //check for brackets
                    if (singleLine[1] == "(" && singleLine[singleLine.Length - 1] == ")")
                    {
                        int numOfParamValue = 0;

                        //count the number of parameters passed
                        for (int i = 2; i < singleLine.Length - 1; i++)
                        {
                            numOfParamValue++;
                        }

                        //stores all params
                        List<int> allParams = new List<int>();

                        //check if number of given parameters are equal to the parameters of method
                        if (numOfParamValue == CheckMethod.methodAndNumberOfParams[methodName])
                        {
                            if (CheckMethod.methodAndNumberOfParams[methodName] == 0)
                            {
                                //////pass all lines between method to command parser be parsed again
                                foreach (var tuple in CheckMethod.methodTuple)
                                {
                                    /////////////////////////
                                    if (tuple.Item1.Trim().ToUpper() == methodName)
                                    {
                                        string[] methodCallLine = tuple.Item3.Trim().Split(new char[] { ',', ' ' }, StringSplitOptions.RemoveEmptyEntries);
                                        checkKeyword.checkForKeywords(possibleCommands, mainDictionary, errorDisplayBox, tuple.Item2, methodCallLine);
                                    }
                                }
                            }

                            //checks if all the prams passed is an integer
                            for (int i = 2; i < singleLine.Length - 1; i++)
                            {
                                bool paramIsInteger = int.TryParse(singleLine[i].Trim(), out int ssd);

                                if (paramIsInteger)
                                {
                                    allParams.Add(int.Parse(singleLine[i]));
                                    everythingOK = 1;
                                }
                                else
                                {
                                    custom.displayErrorMsg(errorDisplayBox, lineNumber, "Parameter list must be only integers", "<method name> ( parameters )");
                                    CommandParser.breakLoopFlag = 1;
                                    CommandParser.breakFlag = 1;
                                    break;
                                }
                            }

                            //runs if no errors
                            if (everythingOK == 1)
                            {
                                string firstLine = "";
                                int flag = 0;

                                //get only the first line from the tuple (i.e something like (MYMETHOD - 5 - Method myMethod ( radius, size )))
                                foreach (Tuple<string, int, string> tuple in CheckMethod.methodTuple)
                                {
                                    if (flag == 0 && tuple.Item1.Trim().ToUpper() == methodName)
                                    {
                                        firstLine = tuple.Item3;
                                        flag++;
                                    }
                                }

                                //split the line
                                string[] splitTuple = firstLine.Split(new char[] { ',',' ' }, StringSplitOptions.RemoveEmptyEntries);
                                List<string> storeParams = new List<string>();

                                //grab only the parameters and store it (radius AND size)
                                for (int i = 3; i < splitTuple.Length - 1; i++)
                                {
                                    storeParams.Add(splitTuple[i]);
                                }

                                //add those parameters to the vardictionary
                                for (int ii = 0; ii < storeParams.Count(); ii++)
                                {
                                    varDictionary[storeParams[ii].ToUpper()] = allParams[ii];
                                }

                                //pass all lines between method to command parser be parsed again
                                foreach (var tuple in CheckMethod.methodTuple)
                                {
                                    //grab only those tuples have the name of the praticualr method
                                    if (tuple.Item1.Trim().ToUpper() == methodName)
                                    {
                                        //split line once more beofre parsing
                                        string[] methodCallLine = tuple.Item3.Trim().Split(new char[] { ',', ' ' }, StringSplitOptions.RemoveEmptyEntries);
                                        checkKeyword.checkForKeywords(possibleCommands, mainDictionary, errorDisplayBox, tuple.Item2, methodCallLine);
                                    }
                                }
                            }
                        }
                        else
                        {
                            custom.displayErrorMsg(errorDisplayBox, lineNumber, "Number of prameters are not equal", "<method name> ( parameters )");
                            CommandParser.breakLoopFlag = 1;
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
                    custom.displayErrorMsg(errorDisplayBox, lineNumber, "Wrong syntax for METHOD calling", "<method name> ( parameters )");
                    CommandParser.breakLoopFlag = 1;
                }
            }
            else
            {
                custom.displayErrorMsg(errorDisplayBox, lineNumber, "Keyword does not exist", "circle OR triangle OR rectangle OR drawto OR moveto");
                CommandParser.breakLoopFlag = 1;
            }
        }
    }
}
