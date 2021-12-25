using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GraphicalProgrammingLanguage
{
    public class CommandParser
    {
        ArrayList codeSplitArrayList = new ArrayList();

        //Form1 ko varaible haru
        ShapeFactory factory = new ShapeFactory(); //to return the shapes 
        public ArrayList shapes = new ArrayList(); //stores shapes
        public Shape s;

        int charindex = 0;
        public static int unrecognizedKeywordlineNumber;
        public static int lineNumber;

        public static int penX; //X-coordinate of MOVETO
        public static int penY; //Y-coordinate of MOVETO

        public static int drawToX = 0;//X1-coordinate of DRAWTO
        public static int drawToY = 0;//Y1-coordinate of DRAWTO

        public static int drawFromX = 0;//X2-coordinate of DRAWTO
        public static int drawFromY = 0;//Y2-coordinate of DRAWTO

        public int keyIndex; //for 'keyword' index

        public static Graphics draw;

        //-------------------
        public int charIndex;


        //takes an arry of string and converts it into arraylist by parsing required string to integer
        public ArrayList returnArrayList(string[] codeSplit)
        {
            //starting(inputBox, errorDisplayBox);
            for (int index = 0; index < codeSplit.Length; index++)
            {
                try
                {
                    if (int.Parse(codeSplit[index]) >= 0)
                    {
                        codeSplitArrayList.Add(int.Parse(codeSplit[index]));
                    }
                }
                catch (System.FormatException)
                {
                    codeSplitArrayList.Add(codeSplit[index].ToUpper());
                }
            }
            return codeSplitArrayList;
        }

        public void checkForKeywords(RichTextBox inputBox, string[] possibleCommands, ArrayList returnedArrayList, RichTextBox errorDisplayBox, PictureBox drawingArea)
        {
            //inputBox.SelectionStart = inputBox.SelectionStart;
            //inputBox.SelectionLength = 5;
            //inputBox.Focus();

            //int selectionStart = inputBox.SelectionStart;
            //int lineIndex = inputBox.GetLineFromCharIndex(selectionStart);
            //errorDisplayBox.Text += lineIndex + " ";

            //int chars = inputBox.Text.Length;
            //errorDisplayBox.Text += "     " + chars;
            foreach (var keyword in returnedArrayList)
            {
                
                //int index = 0;
                //checks if element is a string
                if (keyword.GetType() == typeof(string))
                {
                    //charindex = charIndex + ((String)keyword).Length;
                    //errorDisplayBox.Text += "current char : " + charindex;

                    //find line number
                    unrecognizedKeywordlineNumber = inputBox.GetLineFromCharIndex(inputBox.Find((String)keyword + " "));
                    
                    //charIndex = inputBox.Text.IndexOf((String)keyword) + 1;
                    //errorDisplayBox.Text += ((String)keyword) + " ko charindex : " + charindex;

                    //String[] gg = inputBox.Text.Split('\n');

                    //foreach (string cc in gg)
                    //{
                    //    string[] g = cc.Split(' ');
                    //    //MessageBox.Show("" + keyword, "");
                    //    if (g[0].ToUpper() == (string)keyword)
                    //    {

                    //        lineNumber = index + 1;
                    //        break;
                    //    }
                    //    //else
                    //    //{
                    //        index++;
                    //    //}
                    //}

                    if (possibleCommands.Contains(keyword))
                    {
                        //find index of keyword in returnedArrayList
                        int keywordIndex = returnedArrayList.IndexOf(keyword, keyIndex);

                        //finds the keyword
                        if ((String)keyword == "MOVETO")
                        {
                            keyIndex = keywordIndex + 1;
                            try
                            {
                                //try
                                //{
                                //    //check if number of parameters passed are more than required
                                //    if (returnedArrayList[keywordIndex + 3].GetType() == typeof(int))
                                //    {
                                //        int lineNum = inputBox.GetLineFromCharIndex(inputBox.Find((String)keyword + returnedArrayList[keywordIndex + 3]));
                                //        //displayErrorMsg(errorDisplayBox, "Wrong number of parameters in MOVETO on line", lineNumber, "( expected: [x,y] )");
                                //        break;
                                //    }
                                //}
                                //catch (System.ArgumentOutOfRangeException e)
                                //{

                                //}

                                //next two elements must be numbers
                                if (returnedArrayList[keywordIndex + 1].GetType() == typeof(int) && returnedArrayList[keywordIndex + 2].GetType() == typeof(int))
                                {
                                    penX = (int)returnedArrayList[keywordIndex + 1];
                                    penY = (int)returnedArrayList[keywordIndex + 2];
                                }
                                else
                                {
                                    int lineNum = inputBox.GetLineFromCharIndex(inputBox.Find((String)keyword +" "+ returnedArrayList[keywordIndex + 1]));
                                    displayErrorMsg(errorDisplayBox, "Missing parameters in MOVETO on line", lineNum + 1, "( expected: [x,y] )");
                                    break;
                                }
                            }
                            catch (System.ArgumentOutOfRangeException)
                            {
                                int lineNum = inputBox.GetLineFromCharIndex(inputBox.Find((String)keyword + " "+ returnedArrayList[keywordIndex + 1]));
                                displayErrorMsg(errorDisplayBox, "Missing parameters in MOVETO on line", lineNum + 1, "( expected: [x,y] )");
                                break;
                            }
                        }

                        if ((String)keyword == "CIRCLE")
                        {
                            keyIndex = keywordIndex + 1;
                            try
                            {
                                try
                                {
                                    if (returnedArrayList[keywordIndex + 2].GetType() == typeof(int))
                                    {
                                        int lineNum = inputBox.GetLineFromCharIndex(inputBox.Find((String)keyword + " " + returnedArrayList[keywordIndex + 1] + " " + returnedArrayList[keywordIndex + 2]));
                                        displayErrorMsg(errorDisplayBox, "secondMissing parameters in CIRCLE on line", lineNum, "( expected: [radius] )");
                                        break;
                                    }
                                }
                                catch (System.ArgumentOutOfRangeException e)
                                {

                                }

                                //next element must be number
                                if (returnedArrayList[keywordIndex + 1].GetType() == typeof(int))
                                {
                                    int radius = (int)returnedArrayList[keywordIndex + 1];
                                    getAndAddShape(s, factory, (String)keyword, shapes, penX, penY, radius);
                                }
                                else
                                {
                                    int lineNum = inputBox.GetLineFromCharIndex(inputBox.Find((String)keyword + " " + returnedArrayList[keywordIndex + 1]));
                                    displayErrorMsg(errorDisplayBox, "firstMissing parameters in CIRCLE on line", lineNum, "( expected: [radius] )");
                                    break;
                                }

                                
                            }
                            catch (System.ArgumentOutOfRangeException)
                            {
                                int lineNum = inputBox.GetLineFromCharIndex(inputBox.Find((String)keyword + " " + returnedArrayList[keywordIndex + 1]));
                                displayErrorMsg(errorDisplayBox, "thirdMissing parameters in CIRCLE on line", lineNum, "( expected: [radius] )");
                                break;
                            }
                        }

                        if ((String)keyword == "RECTANGLE")
                        {
                            keyIndex = keywordIndex + 1;
                            try
                            {
                                //next two elements must be numbers
                                if (returnedArrayList[keywordIndex + 1].GetType() == typeof(int) && returnedArrayList[keywordIndex + 2].GetType() == typeof(int))
                                {
                                    int width = (int)returnedArrayList[keywordIndex + 1];
                                    int height = (int)returnedArrayList[keywordIndex + 2];
                                    getAndAddShape(s, factory, (String)keyword, shapes, penX, penY, width, height);
                                }
                                else
                                {
                                    displayErrorMsg(errorDisplayBox, "Missing parameters in RECTANGLE on line", lineNumber, "(expected: [width, height])");
                                    break;
                                }
                            }
                            catch (System.ArgumentOutOfRangeException)
                            {
                                displayErrorMsg(errorDisplayBox, "Missing parameters in RECTANGLE on line", lineNumber, "(expected: [width, height])");
                                break;
                            }

                        }

                        if ((String)keyword == "TRIANGLE")
                        {
                            keyIndex = keywordIndex + 1;
                            try
                            {
                                //next two elements must be numbers
                                if (returnedArrayList[keywordIndex + 1].GetType() == typeof(int) && returnedArrayList[keywordIndex + 2].GetType() == typeof(int))
                                {
                                    int bases = (int)returnedArrayList[keywordIndex + 1];
                                    int height = (int)returnedArrayList[keywordIndex + 2];
                                    getAndAddShape(s, factory, (String)keyword, shapes, penX, penY, bases, height);
                                }
                                else
                                {
                                    displayErrorMsg(errorDisplayBox, "Missing parameters in TRIANGLE on line", lineNumber, "(expected: [height, base])");
                                    break;
                                }
                            }
                            catch (System.ArgumentOutOfRangeException)
                            {
                                displayErrorMsg(errorDisplayBox, "Missing parameters in TRIANGLE on line", lineNumber, "(expected: [height, base])");
                                break;
                            }
                        }

                        if ((String)keyword == "DRAWTO")
                        {
                            keyIndex = keywordIndex + 1;
                            try
                            {
                                //next two elements must be numbers
                                if (returnedArrayList[keywordIndex + 1].GetType() == typeof(int) && returnedArrayList[keywordIndex + 2].GetType() == typeof(int))
                                {
                                    //if 'moveTo' is written before 'drawTo'
                                    drawFromX = penX;
                                    drawFromY = penY;

                                    //store params
                                    drawToX = (int)returnedArrayList[keywordIndex + 1];
                                    drawToY = (int)returnedArrayList[keywordIndex + 2];

                                    //acts as moveto 
                                    penX = drawToX;
                                    penY = drawToY;

                                    getAndAddShape(s, factory, (String)keyword, shapes, drawFromX, drawFromY, drawToX, drawToY);

                                    //ending point become starting point
                                    drawFromX = drawToX;
                                    drawFromY = drawToY;
                                }
                                else
                                {
                                    displayErrorMsg(errorDisplayBox, "Missing parameters in DRAWTO on line", lineNumber, "(expected: [x, y])");
                                    break;
                                }

                            }
                            catch (System.ArgumentOutOfRangeException)
                            {
                                displayErrorMsg(errorDisplayBox, "Missing parameters in DRAWTO on line", lineNumber, "(expected: [x, y])");
                                break;
                            }
                        }
                    }
                    else
                    {
                        displayErrorMsg(errorDisplayBox, "Unrecognized keyword on line", unrecognizedKeywordlineNumber + 1, "try: CIRCLE, TRIANGLE, RECTANGLE, MOVETO or DRAWTO");
                        break;
                    }
                }
            }

            drawingArea.Refresh();
            returnedArrayList.Clear();
        }

        public void displayErrorMsg(RichTextBox errorDisplayBox, String error, int lineNumber, String correctFormat)
        {
            //this.errorDisplayBox = errorDisplayBox;
            errorDisplayBox.Text += "\n" + error + " : " + lineNumber + " | " + correctFormat;
        }

        public void drawCurrMoveToPos(PaintEventArgs e, int penX, int penY)
        {
            Graphics movePos = e.Graphics;
            Pen p = new Pen(Color.Red, 1);
            movePos.DrawRectangle(p, penX - (4 / 2), penY - (4 / 2), 4, 4);
            movePos.FillRectangle(new SolidBrush(Color.Red), penX - (4 / 2), penY - (4 / 2), 4, 4);
        }

        public void drawShapes(ArrayList shapes, Shape s, Graphics draw)
        {
            // draw all shapes stored in the 'shapes' arralist
            for (int i = 0; i < shapes.Count; i++)
            {
                s = (Shape)shapes[i];
                if (s != null)
                {
                    s.draw(draw); //draw the actual shape
                }
            }
        }

        public void getAndAddShape(Shape s, ShapeFactory factory, String keyword, ArrayList shapes, params int[] list)
        {
            s = factory.getShape((String)keyword);
            Color circleColour = Color.Transparent;
            s.set(circleColour, list);
            shapes.Add(s);
        }
    }
}
