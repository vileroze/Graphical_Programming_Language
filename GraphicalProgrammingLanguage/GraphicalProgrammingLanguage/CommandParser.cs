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

        int keywordCharIndex = 0;
        public static int lineNumber;

        public static int penX; //X-coordinate of MOVETO
        public static int penY; //Y-coordinate of MOVETO

        public static int drawToX = 0;//X1-coordinate of DRAWTO
        public static int drawToY = 0;//Y1-coordinate of DRAWTO

        public static int drawFromX = 0;//X2-coordinate of DRAWTO
        public static int drawFromY = 0;//Y2-coordinate of DRAWTO

        public int keyIndex; //for 'keyword' index

        public static Graphics draw;


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
            foreach (var keyword in returnedArrayList)
            {
                //string tempss = (String)keyword;
                if (keyword.GetType() == typeof(string))
                {
                    //find index of keyword character in inputBox
                    keywordCharIndex = (inputBox.Text.ToUpper()).IndexOf(((String)keyword).ToUpper());

                    //find line number
                    lineNumber = (inputBox.GetLineFromCharIndex(keywordCharIndex)) + 1;

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
                                int flag = 0;
                                //next two elements must be numbers
                                if (returnedArrayList[keywordIndex + 1].GetType() == typeof(int) && returnedArrayList[keywordIndex + 2].GetType() == typeof(int))
                                {
                                    penX = (int)returnedArrayList[keywordIndex + 1];
                                    penY = (int)returnedArrayList[keywordIndex + 2];

                                }
                                else if (returnedArrayList[keywordIndex + 1].GetType() != typeof(int) && returnedArrayList[keywordIndex + 2].GetType() != typeof(int))
                                {
                                    errorDisplayBox.Text += "\nMissing parameters in MOVETO on line: " + lineNumber + " (expected: [x,y])";
                                }
                            }

                            catch (System.ArgumentOutOfRangeException)
                            {
                                //Point textBoxLocation = inputBox.GetPositionFromCharIndex(keywordIndex);
                                errorDisplayBox.Text += "\nMissing parameters in MOVETO on line : " + lineNumber;
                            }
                        }

                        if ((String)keyword == "CIRCLE")
                        {
                            keyIndex = keywordIndex + 1;
                            try
                            {
                                //next two elements must be numbers
                                if (returnedArrayList[keywordIndex + 1].GetType() == typeof(int))
                                {
                                    int radius = (int)returnedArrayList[keywordIndex + 1];
                                    getAndAddShape(s, factory, (String)keyword, shapes, penX, penY, radius);
                                }
                                else
                                {
                                    errorDisplayBox.Text += "\nMissing parameters in CIRCLE on line: " + lineNumber + " (expected: [radius])";
                                }
                            }
                            catch (System.ArgumentOutOfRangeException)
                            {
                                //Point textBoxLocation = inputBox.GetPositionFromCharIndex(keywordIndex);
                                errorDisplayBox.Text += "\nMissing parameters in CIRCLE on line : " + lineNumber;
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
                                    errorDisplayBox.Text += "\nMissing parameters in RECTANGLE on line: " + lineNumber + " (expected: [width, height])";
                                }
                            }
                            catch (System.ArgumentOutOfRangeException)
                            {
                                //Point textBoxLocation = inputBox.GetPositionFromCharIndex(keywordIndex);
                                errorDisplayBox.Text += "\nMissing parameters in RECTANGLE on line : " + lineNumber;
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
                                    errorDisplayBox.Text += "\nMissing parameters else in TRIANGLE on line: " + lineNumber + " (expected: [height, base])";
                                }
                            }
                            catch (System.ArgumentOutOfRangeException)
                            {
                                //Point textBoxLocation = inputBox.GetPositionFromCharIndex(keywordIndex);
                                errorDisplayBox.Text += "\nMissing parameters in TRIANGLE on line : " + lineNumber;
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

                                    drawFromX = drawToX;
                                    drawFromY = drawToY;
                                }
                                else
                                {
                                    errorDisplayBox.Text += "\nMissing parameters else in DRAWTO on line: " + lineNumber + " (expected: [height, base])";
                                }

                            }
                            catch (System.ArgumentOutOfRangeException)
                            {
                                //Point textBoxLocation = inputBox.GetPositionFromCharIndex(keywordIndex);
                                errorDisplayBox.Text += "\nMissing parameters in DRAWTO on line : " + lineNumber;
                            }
                        }
                    }
                    else
                    {
                        //errorDisplay.Text += "Unrecognized keyword on line: " + lineNumber;
                        displayErrorMsg(errorDisplayBox, "Unrecognized keyword on line: ", lineNumber);
                    }
                }
            }

            drawingArea.Refresh();
            returnedArrayList.Clear();
        }




















        public void displayErrorMsg(RichTextBox errorDisplayBox, String error, int lineNumber)
        {
            //this.errorDisplayBox = errorDisplayBox;
            errorDisplayBox.Text += "\n" + error + lineNumber;
        }

        public void drawCurrMoveToPos(PaintEventArgs e, int penX, int penY)
        {
            Graphics movePos = e.Graphics;
            Pen p = new Pen(Color.Red, 1);
            movePos.DrawRectangle(p, penX - (3 / 2), penY - (3 / 2), 3, 3);
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
