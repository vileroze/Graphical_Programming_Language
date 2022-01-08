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
    class CheckConditionalStatements
    {
        CustomMethods custom = new CustomMethods();
        //public static List<Tuple< int, string>> whileTuple = new List<Tuple< int, string>>(); //  lineNumber, everything inside while
        //CheckKeyword checkKeyword = new CheckKeyword();
        public static int checkLoops = 0;

        public void checkForConditionalStatements( string[] singleLine, Dictionary<int, string> mainDictionary, Dictionary<string, int> varDictionary, RichTextBox errorDisplayBox, int lineNumber)
        {
            //--------------------IF STATEMENT--------------------------------
            if ((string)singleLine[0].ToUpper() == "IF")
            {
                int countArrayNum = singleLine.Length;
                int varToCompare = 0;
                string compOperator = "";
                int lastElement = 0;
                CommandParser.ifLineNumber = lineNumber;
                int tempLineNumber = lineNumber;

                //check if line has 3 components beside the actual keyword
                if (countArrayNum - 1 == 3)
                {

                    //get the line number of ENDIF
                    int tempEndif = 0;
                    foreach (var row in mainDictionary)
                    {
                        if (row.Key > lineNumber)
                        {
                            if (row.Value.ToUpper() == "ENDIF")
                            {
                                CommandParser.endIfLineNumber = row.Key;
                                tempEndif = row.Key;
                                break;
                            }

                            try
                            {
                                if (mainDictionary[tempLineNumber + 1].Split(' ')[0].Trim().ToUpper() == "IF")
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
                    CommandParser.ifConditionStatus = 0;

                    //runs when only one IF statement
                    if (tempEndif == 0)
                    {
                        custom.displayErrorMsg(errorDisplayBox, lineNumber, "IF statement was never ended", "IF <some integer value> == <some integer value> ....... ENDIF");
                        CommandParser.breakFlag = 1;
                        CommandParser.breakLoopFlag = 1;
                    }



                    //get the value of the variable
                    string[] parameter = CustomMethods.getValueFromDictionary(varDictionary, singleLine[1]);
                    try
                    {
                        //check if the value of variable is positive
                        if (int.Parse(parameter[0]) >= 0)
                        {
                            varToCompare = int.Parse(parameter[0]);

                            //check if statement has operator
                            if (singleLine[2].ToUpper() == "==" || singleLine[2].ToUpper() == ">" || singleLine[2].ToUpper() == "<" || singleLine[2].ToUpper() == ">=" || singleLine[2].ToUpper() == "<=" || singleLine[2].ToUpper() == "!=")
                            {
                                compOperator = singleLine[2];

                                //check if RHS element is an integer
                                try
                                {
                                    string[] rhsParam = CustomMethods.getValueFromDictionary(varDictionary, singleLine[3]);
                                    lastElement = int.Parse(rhsParam[0]);
                                    bool checkCondition = false;

                                    //check if condition not true accordint to the operators used
                                    if (singleLine[2].ToUpper() == "==")
                                    {
                                        if (varToCompare == lastElement)
                                        {
                                            checkCondition = true;
                                        }
                                    }
                                    else if (singleLine[2].ToUpper() == ">")
                                    {
                                        if (varToCompare > lastElement)
                                        {
                                            checkCondition = true;
                                        }
                                    }
                                    else if (singleLine[2].ToUpper() == "<")
                                    {
                                        if (varToCompare < lastElement)
                                        {
                                            checkCondition = true;
                                        }
                                    }
                                    else if (singleLine[2].ToUpper() == ">=")
                                    {
                                        if (varToCompare >= lastElement)
                                        {
                                            checkCondition = true;
                                        }
                                    }
                                    else if (singleLine[2].ToUpper() == "<=")
                                    {
                                        if (varToCompare <= lastElement)
                                        {
                                            checkCondition = true;
                                        }
                                    }
                                    else if (singleLine[2].ToUpper() == "!=")
                                    {
                                        if (varToCompare != lastElement)
                                        {
                                            checkCondition = true;
                                        }
                                    }

                                    if (checkCondition == false)
                                    {
                                        CommandParser.ifConditionStatus = 1;
                                    }
                                }
                                catch (FormatException)
                                {
                                    custom.displayErrorMsg(errorDisplayBox, lineNumber, "Right hand side of operator should be number", "IF <variable> == <some integer value>");
                                    CommandParser.breakLoopFlag = 1;
                                }
                            }
                            else
                            {
                                custom.displayErrorMsg(errorDisplayBox, lineNumber, "Comparison operator not recognized", "== OR <= OR >= OR != OR > OR <");
                                CommandParser.breakLoopFlag = 1;
                            }
                        }
                    }
                    catch (FormatException)
                    {
                        custom.displayErrorMsg(errorDisplayBox, lineNumber, "Left hand side of operator should be number", "IF <variable> == <some integer value>");
                        CommandParser.breakLoopFlag = 1;
                    }
                }
                else
                {
                    custom.displayErrorMsg(errorDisplayBox, lineNumber, "Wrong syntax for IF statement", "IF <variable> == <some integer value>");
                    CommandParser.breakLoopFlag = 1;
                }
            }


            //---------------------WHILE STATEMENT-------------------------------
            if ((string)singleLine[0].ToUpper() == "WHILE")
            { 
                int countArrayNum = singleLine.Length;
                int varToCompare = 0;
                string compOperator = "";
                int lastElement = 0;
                CommandParser.whileLineNumber = lineNumber;
                int tempLineNumber = lineNumber;

                //check if line has 3 components beside the actual keyword
                if (countArrayNum - 1 == 3)
                {
                    //get the line number of ENDLOOP
                    int tempEndLoop = 0;
                    foreach (var row in mainDictionary)
                    {
                        //ensures   ENDLOOP comes after WHILE and breaks
                        if (row.Key > CommandParser.whileLineNumber)
                        {
                            if (row.Value.ToUpper() == "ENDLOOP")
                            {
                                CommandParser.endLoopLineNumber = row.Key;
                                tempEndLoop = row.Key;
                                break;
                            }

                            try
                            {
                                if (mainDictionary[tempLineNumber + 1].Split(' ')[0].Trim().ToUpper() == "WHILE")
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
                    CommandParser.whileConditionStatus = 0;

                    //displays error if ENDLOOP is not found
                    if (tempEndLoop == 0)
                    {
                        custom.displayErrorMsg(errorDisplayBox, lineNumber, "WHILE statement was never ended", "WHILE <some integer value> > <some integer value> ....... ENDIF");
                        
                        CommandParser.breakLoopFlag = 1;
                        CommandParser.breakFlag = 1;
                    }

                    //check while condition
                    string[] parameter = CustomMethods.getValueFromDictionary(varDictionary, singleLine[1]);
                    try
                    {
                        //check if the value of variable is positive
                        if (int.Parse(parameter[0]) >= 0)
                        {
                            varToCompare = int.Parse(parameter[0]);

                            //check if statement has operator
                            if (singleLine[2].ToUpper() == "==" || singleLine[2].ToUpper() == ">" || singleLine[2].ToUpper() == "<" || singleLine[2].ToUpper() == ">=" || singleLine[2].ToUpper() == "<=" || singleLine[2].ToUpper() == "!=")
                            {
                                compOperator = singleLine[2];

                                //check if RHS element is an integer
                                try
                                {
                                    //get the value of the variable
                                    string[] rhsParam = CustomMethods.getValueFromDictionary(varDictionary, singleLine[3]);
                                    lastElement = int.Parse(rhsParam[0]);

                                    bool whilecheckCondition = false;

                                    //check if condition true according to the operators used
                                    if (singleLine[2].ToUpper() == "==")
                                    {
                                        if (varToCompare == lastElement)
                                        {
                                            whilecheckCondition = true;
                                        }
                                    }
                                    else if (singleLine[2].ToUpper() == ">")
                                    {
                                        if (varToCompare > lastElement)
                                        {
                                            whilecheckCondition = true;
                                        }
                                    }
                                    else if (singleLine[2].ToUpper() == "<")
                                    {
                                        if (varToCompare < lastElement)
                                        {
                                            whilecheckCondition = true;
                                        }
                                    }
                                    else if (singleLine[2].ToUpper() == ">=")
                                    {
                                        if (varToCompare >= lastElement)
                                        {
                                            whilecheckCondition = true;
                                        }
                                    }
                                    else if (singleLine[2].ToUpper() == "<=")
                                    {
                                        if (varToCompare <= lastElement)
                                        {
                                            whilecheckCondition = true;
                                        }
                                    }
                                    else if (singleLine[2].ToUpper() == "!=")
                                    {
                                        if (varToCompare != lastElement)
                                        {
                                            whilecheckCondition = true;
                                        }
                                    }

                                    if (whilecheckCondition == false)
                                    {
                                        CommandParser.whileConditionStatus = 1;
                                    }
                                    else
                                    {
                                        checkLoops = 1;
                                    }
                                    //else
                                    //{
                                    //    checkLoops = 1;
                                        

                                    //    //while (checkLoops == 1)
                                    //    //{
                                    //        foreach (var row in mainDictionary)
                                    //        {
                                    //            if (row.Key > CommandParser.whileLineNumber && row.Key < CommandParser.endLoopLineNumber)
                                    //            {
                                    //                whileTuple.Add(new Tuple<int, string>(row.Key, row.Value));
                                    //            }
                                    //        }

                                    //    foreach (var tuple in whileTuple)
                                    //    {
                                    //        Console.WriteLine("while bhitra kokura {0} - {1} - {2}", tuple.Item1, tuple.Item2);
                                    //    }

                                    //        //foreach (var tuple in whileTuple)
                                    //        //{
                                    //        //    //grab only those tuples have the name of the praticualr method
                                    //        //    //if (tuple.Item1.Trim().ToUpper() == methodName)
                                    //        //    //{
                                    //        //        //split line once more beofre parsing
                                    //        //        string[] whileCallLine = tuple.Item2.Trim().Split(new char[] { ',', ' ' }, StringSplitOptions.RemoveEmptyEntries);
                                    //        //        checkKeyword.checkForKeywords( mainDictionary, errorDisplayBox, tuple.Item1, whileCallLine);
                                    //        //    //}
                                    //        //}
                                    //    //}
                                    //}
                                }
                                catch (FormatException)
                                {
                                    custom.displayErrorMsg(errorDisplayBox, lineNumber, "Right hand side of operator should be number", "WHILE <variable> > <some integer value>");
                                    CommandParser.breakLoopFlag = 1;
                                }
                            }
                            else
                            {
                                custom.displayErrorMsg(errorDisplayBox, lineNumber, "Comparison operator not recognized", "== OR <= OR >= OR != OR > OR <");
                                CommandParser.breakLoopFlag = 1;
                            }
                        }
                    }
                    catch (FormatException)
                    {
                        custom.displayErrorMsg(errorDisplayBox, lineNumber, "Left hand side of operator should be number", "WHILE <variable> > <some integer value>");
                        CommandParser.breakLoopFlag = 1;
                    }
                }
                else
                {
                    custom.displayErrorMsg(errorDisplayBox, lineNumber, "Wrong syntax for WHILE statement", "WHILE <variable> > <some integer value>");
                    CommandParser.breakLoopFlag = 1;
                }
            }
        }
    }
}
