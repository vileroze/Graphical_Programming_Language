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
    class CheckVariable
    {
        CustomMethods custom = new CustomMethods();

        public void checkForVariables(string[] singleLine, Dictionary<string, int> varDictionary, RichTextBox errorDisplayBox, int lineNumber)
        {
            try
            {
                //checks for variabless | pair.Value.Length == 4 && 
                if (singleLine.Contains("=") && Array.IndexOf(singleLine, "=") == 1)
                {
                    if (singleLine[1] == "=")
                    {
                        int indexOfEqualsSign = Array.IndexOf(singleLine, "="); //var = 10+20+30
                        string varName = "";
                        string output = ""; //to store all the things that need to be calculated

                        //create string to calculate value to put in the dictionary
                        string[] varValueArray = new string[10];
                        Array.Copy(singleLine, indexOfEqualsSign + 1, varValueArray, 0, singleLine.Length - 2);
                        varValueArray = varValueArray.Where(c => c != null).ToArray();

                        foreach (string input in varValueArray)
                        {
                            //stores something like : 10+20+30 OR height+20
                            output += input;
                        }


                        //replace variable with its value
                        string[] splitOutput = output.Split(new char[] { '+', '-', '/', '*' });
                        Debug.WriteLine("splitOutput : " + splitOutput.Length);

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

                            //check if variable name is a string
                            bool isVarString = int.TryParse(singleLine[indexOfEqualsSign - 1], out int varrName);

                            if (isVarString == false)
                            {
                                varName = singleLine[indexOfEqualsSign - 1];
                            }
                            else
                            {
                                custom.displayErrorMsg(errorDisplayBox, lineNumber, "variable names cannot be a number", "<variable name> = <some integer>");
                                CommandParser.breakLoopFlag = 1;
                                //break;
                            }

                            try
                            {
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
                            catch (InvalidCastException)
                            {
                                custom.displayErrorMsg(errorDisplayBox, lineNumber, "Variable value cannot be empty", "<variable name> = <some integer>");
                                CommandParser.breakLoopFlag = 1;
                            }
                            
                        }
                        catch (FormatException)
                        {
                            custom.displayErrorMsg(errorDisplayBox, lineNumber, "variable names cannot be a number", "<variable name> = <some integer>");
                            CommandParser.breakLoopFlag = 1;
                        }
                        // asdf = asdf
                        catch (EvaluateException)
                        {
                            custom.displayErrorMsg(errorDisplayBox, lineNumber, "varaibles cannot store strings", "<variable name> = <some integer>");
                            CommandParser.breakLoopFlag = 1;
                            //break;
                        }
                        catch (SyntaxErrorException)
                        {
                            custom.displayErrorMsg(errorDisplayBox, lineNumber, "varaibles cannot store strings", "<variable name> = <some integer>");
                            CommandParser.breakLoopFlag = 1;
                            //break;
                        }
                    }
                }
                else
                {
                    custom.displayErrorMsg(errorDisplayBox, lineNumber, "Keyword does not exist", "circle OR triangle OR rectangle OR drawto OR moveto");
                    CommandParser.breakLoopFlag = 1;
                    //break;
                }
            }
            catch (IndexOutOfRangeException)
            {
                errorDisplayBox.Text += "\naaaaaaaaaaaaaaaaaaaaa";
            }

            
        }
    }
}
