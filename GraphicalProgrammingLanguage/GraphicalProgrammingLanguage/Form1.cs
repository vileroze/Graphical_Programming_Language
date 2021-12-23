using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GraphicalProgrammingLanguage
{
    public partial class Form1 : Form
    {

        OpenFileDialog fileExplorer = new OpenFileDialog();
        SaveFileDialog saveFile = new SaveFileDialog();

        Shape s;
        ShapeFactory factory = new ShapeFactory(); //to return the shapes 
        ArrayList shapes = new ArrayList(); //stores shapes
        ArrayList codeSplitArrayList = new ArrayList();

        public static int penX; //X-coordinate of MOVETO
        public static int penY; //Y-coordinate of MOVETO

        public static int drawToX = 0;//X1-coordinate of DRAWTO
        public static int drawToY = 0;//Y1-coordinate of DRAWTO

        public static int drawFromX = 0;//X2-coordinate of DRAWTO
        public static int drawFromY = 0;//Y2-coordinate of DRAWTO

        int keyIndex; //for 'keyword' index
        int keywordCharIndex = 0; //HELPS FIND LINE NUMBER
        int lineNumber; //STORES line number


        public Form1()
        {
            InitializeComponent();
        }

        private void runCode_Click(object sender, EventArgs e)
        {
            shapes.Clear(); //clear arraylist before execution
            keyIndex = 0; //helps find 'keyword' index

            ShapeFactory factory = new ShapeFactory();
            string code = codeArea.Text; //stores text from textBox
            string[] codeSplit = code.Split(new Char[] { ',', ' ', '\n' }, //s[lit the stored 'code'
                                 StringSplitOptions.RemoveEmptyEntries);
            string[] possibleCommands = { "DRAWTO", "MOVETO", "CIRCLE", "RECTANGLE", "TRIANGLE" }; //stores all possible commands

            //iterate through 'codeSplit', parse each item and store to arraylist 'codeSplitArrayList'
            for (int index = 0; index < codeSplit.Length; index++)
            {
                try
                {
                    if (int.Parse(codeSplit[index]) >= 0)
                    {
                        codeSplitArrayList.Add(int.Parse(codeSplit[index])); //parse add store if integer
                    }
                }
                catch (System.FormatException)
                {
                    codeSplitArrayList.Add(codeSplit[index].ToUpper()); //catch and store if not integer
                }
            }

            foreach (var keyword in codeSplitArrayList)
            {
                if (keyword.GetType() == typeof(string))
                {
                    //find index of keyword character in codeArea
                    keywordCharIndex = (codeArea.Text.ToUpper()).IndexOf(((String)keyword).ToUpper());

                    //find line number
                    lineNumber = codeArea.GetLineFromCharIndex(keywordCharIndex);
                    //check if keyword is one of the possible 'commands'
                    if (possibleCommands.Contains(keyword))
                    {
                        //find index of keyword in codeSplitArrayList
                        int keywordIndex = codeSplitArrayList.IndexOf(keyword, keyIndex);

                        //finds the specific keyword
                        if ((String)keyword == "MOVETO")
                        {
                            //next two elements must be numbers
                            if ((codeSplitArrayList[keywordIndex + 1].GetType() == typeof(int)) && (codeSplitArrayList[keywordIndex + 2].GetType() == typeof(int)))
                            {
                                //increase if 'keyword' is 'possibleCommand'
                                keyIndex = keywordIndex + 1;

                                penX = (int)codeSplitArrayList[keywordIndex + 1];
                                penY = (int)codeSplitArrayList[keywordIndex + 2];
                            }
                            else
                            //display error message
                            {
                                codeArea.Text += "\nWrong number of params in MOVETO on line: " + (lineNumber + 1) + " (required: 2 [x,y])";
                            }
                        }

                        if ((String)keyword == "CIRCLE")
                        {
                            //next two elements must be numbers
                            if (codeSplitArrayList[keywordIndex + 1].GetType() == typeof(int))
                            {
                                //increase if 'keyword' is 'possibleCommand'
                                keyIndex = keywordIndex + 1;

                                s = factory.getShape((String)keyword); //return a shape
                                Color circleColour = Color.Transparent;
                                s.set(circleColour, penX, penY, (int)codeSplitArrayList[keywordIndex + 1]);
                                shapes.Add(s); //add the shape to arraylist
                            }
                            else
                            //display error message
                            {
                                codeArea.Text += "\nWrong number of parameters in CIRCLE on line: " + (lineNumber + 1) + " (required: 1 [radius])";
                            }
                        }

                        if ((String)keyword == "RECTANGLE")
                        {
                            //next two elements must be numbers
                            if (codeSplitArrayList[keywordIndex + 1].GetType() == typeof(int) && codeSplitArrayList[keywordIndex + 2].GetType() == typeof(int))
                            {
                                //increase if 'keyword' is 'possibleCommand'
                                keyIndex = keywordIndex + 1;

                                s = factory.getShape((String)keyword); //return a shape
                                Color circleColour = Color.Transparent;
                                s.set(circleColour, penX, penY, (int)codeSplitArrayList[keywordIndex + 1], (int)codeSplitArrayList[keywordIndex + 2]);
                                shapes.Add(s); //add the shape to arraylist
                            }
                            else
                            //display error message
                            {
                                codeArea.Text += "\nWrong number of parameters in RECTANGLE on line: " + (lineNumber + 1) + " (required: 2 [width, height])";
                            }
                        }

                        if ((String)keyword == "TRIANGLE")
                        {
                            //increase if 'keyword' is 'possibleCommand'
                            keyIndex = keywordIndex + 1;

                            //next two elements must be numbers
                            if (codeSplitArrayList[keywordIndex + 1].GetType() == typeof(int) && codeSplitArrayList[keywordIndex + 2].GetType() == typeof(int))
                            {
                                s = factory.getShape((String)keyword); //return a shape
                                Color circleColour = Color.Transparent;
                                s.set(circleColour, penX, penY, (int)codeSplitArrayList[keywordIndex + 1], (int)codeSplitArrayList[keywordIndex + 2]);
                                shapes.Add(s); //add the shape to arraylist
                            }
                            else
                            //display error message
                            {
                                codeArea.Text += "\nWrong number of parameters else in TRIANGLE on line: " + (lineNumber + 1) + " (required: 2 [height, base])";
                            }
                        }

                        if ((String)keyword == "DRAWTO")
                        {
                            keyIndex = keywordIndex + 1;
                            try
                            {
                                //next two elements must be numbers
                                if (codeSplitArrayList[keywordIndex + 1].GetType() == typeof(int) && codeSplitArrayList[keywordIndex + 2].GetType() == typeof(int))
                                {
                                    //starting point for the line if a 'moveTo' exists before 
                                    drawFromX = penX;
                                    drawFromY = penY;

                                    //starting point for the line from parameter
                                    drawToX = (int)codeSplitArrayList[keywordIndex + 1];
                                    drawToY = (int)codeSplitArrayList[keywordIndex + 2];

                                    //acts as moveto 
                                    penX = drawToX;
                                    penY = drawToY;

                                    s = factory.getShape((String)keyword);
                                    Color circleColour = Color.Transparent;
                                    s.set(circleColour, drawFromX, drawFromY, drawToX, drawToY);
                                    shapes.Add(s);

                                    //stores previous end point to be used for next line's starting point
                                    drawFromX = drawToX;
                                    drawFromY = drawToY;
                                }
                                else
                                {
                                    //errorDisplay.Text += "\nMissing parameters else in DRAWTO on line: " + (lineNumber + 1) + " (expected: [height, base])";
                                }

                            }
                            catch (System.ArgumentOutOfRangeException)
                            {
                                //Point textBoxLocation = codeArea.GetPositionFromCharIndex(keywordIndex);
                                //errorDisplay.Text += "\nMissing parameters in DRAWTO on line : " + (lineNumber + 1);
                            }
                        }
                    }
                    else
                    {
                        codeArea.Text += "\nUnrecognized keyword on line: " + (lineNumber + 1);
                    }
                }
            }
            DrawingArea.Refresh(); //refresh drawing area to show newly drawn shape
            codeSplitArrayList.Clear(); //clear arraylist after done processsing
        }

        private void DrawingArea_Paint(object sender, PaintEventArgs e)
        {
            //drwas a 3 by 3 rectangle to help visualize the current position of the 'Moveto' object
            Graphics gr = e.Graphics;
            Pen p = new Pen(Color.Red, 1);
            gr.DrawRectangle(p, penX - (3 / 2), penY - (3 / 2), 3, 3);


            Graphics g = e.Graphics;
            //iterates through all shapes stored in the 'shapes' arralist
            for (int i = 0; i < shapes.Count; i++)
            {
                s = (Shape)shapes[i];
                if (s != null)
                {
                    s.draw(g); //draws the actual shape
                }
                else
                {
                    codeArea.Text += "\ninvalid shape in array"; //will never run as shape factory does not return invalid shapes
                }
            }
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                using (StreamWriter outputFile = File.CreateText(fileExplorer.FileName))
                {
                    // Write the info to the file. 
                    outputFile.WriteLine(codeArea.Text);
                    outputFile.Close();

                    String filename = Path.GetFileName(fileExplorer.FileName);
                    String mssg = "Work saved to : " + filename;
                    mssg += "\nLocation: " + fileExplorer.FileName;
                    MessageBox.Show(mssg, "ALERT");
                }
            }
            catch (System.ArgumentException)
            {
                saveFile.RestoreDirectory = true;
                saveFile.Title = "Where do you want to save your work?";
                saveFile.InitialDirectory = @"C:\Users\DELL\OneDrive\Desktop\fourth year\semester 1\Advanced Software Engineering\";
                saveFile.Filter = "Text|*.txt|All|*.*";
                try
                {
                    if (saveFile.ShowDialog() == DialogResult.OK)
                    {
                        StreamWriter fWriter = File.CreateText(saveFile.FileName);
                        fWriter.WriteLine(codeArea.Text);
                        fWriter.Close();

                        String filename = Path.GetFileName(saveFile.FileName);
                        String mssg = "Work saved to : " + filename;
                        mssg += "\nLocation: " + saveFile.FileName;

                        MessageBox.Show(mssg, "ALERT");
                    }
                }
                catch (IOException)
                {
                    MessageBox.Show("Error", "IO exception");
                }
            }
        }

        private void loadToolStripMenuItem_Click(object sender, EventArgs e)
        {
            fileExplorer.Filter = "Text|*.txt|All|*.*";
            fileExplorer.Title = "Choose your file";
            fileExplorer.FilterIndex = 1;
            fileExplorer.InitialDirectory = @"C:\Users\DELL\OneDrive\Desktop\fourth year\semester 1\Advanced Software Engineering\";
            fileExplorer.RestoreDirectory = true;
            try
            {
                if (fileExplorer.ShowDialog() == DialogResult.OK)
                {
                    codeArea.Text = "";
                    //String filename = Path.GetFileName(fileExplorer.FileName);
                    StreamReader s = File.OpenText(fileExplorer.FileName);
                    do
                    {
                        String line = s.ReadLine();
                        if (line == null) break;
                        codeArea.Text += line;
                        codeArea.AppendText(Environment.NewLine);
                    } while (true);
                    s.Close();
                }
            }

            catch (System.ArgumentException)
            {
                MessageBox.Show("NO FILE CHOSEN", "ERROR");
            }
            catch (FileNotFoundException)
            {
                MessageBox.Show("FILE NOT FOUND", "ALERT ");
            }
            catch (IOException)
            {
                MessageBox.Show("IO exception", "ERROR");
            }
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
