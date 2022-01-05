using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
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

        CommandParser parser = new CommandParser();
        CustomMethods custom = new CustomMethods();

        Dictionary<int, string> dictionary = new Dictionary<int, string>();
        ArrayList singleLineCommand = new ArrayList();

        //flag set to determine if command is from commandLine or codeArea (i.e 1 for codeArea and 2 for commandLine)
        static int flag = 0;

        // stores all possible commands
        public string[] possibleCommands = { "DRAWTO", "MOVETO", "CIRCLE", "RECTANGLE", "TRIANGLE", "PEN", "FILL", "POLYGON", "IF", "ENDIF", "WHILE", "ENDLOOP"};

        public Form1()
        {
            InitializeComponent();
        }


        /// <summary>
        /// takes input from either the commandLine or the actual code area depending on the commands passed in the commandLine
        /// </summary>
        /// <param name="input">the command to be parsed</param>
        public void startExecution( String input)
        {
            //for multi line codes i.e codeArea
            CommandParser.breakLoopFlag = 0;
            CheckConditionalStatements.checkLoops = 0;

            if (flag == 1)
            {

                errorDisplayBox.Text = "";
                string code = input;
                dictionary.Clear();
                
                

                // split lines 
                string[] splitLine = code.Split(new char[] { '\n' });
                int lineNumber = 1;
                foreach (string line in splitLine)
                {
                    // add the entire line as value and the lineNumber as key
                    dictionary.Add(lineNumber, line);
                    Debug.WriteLine(line);
                    lineNumber++;
                }
            }

            //for single line codes i.e commandLine
            if (flag == 2)
            {
                if (CommandParser.shapes.Count > 0) { singleLineCommand.Clear(); }
                errorDisplayBox.Text = "";

                //take input
                string singleCommand = input;

                //assign lineNumber
                int lineNumber = 1;

                //add the command to arraylist
                singleLineCommand.Add(singleCommand);

                // for every line add the lineNumber as key and entire line as value
                foreach (string line in singleLineCommand)
                {
                    dictionary.Add(lineNumber, line);
                    lineNumber++;
                }
            }

            // main execution part of the program (see xml file for full specifications)
            parser.mainParser(possibleCommands, dictionary, errorDisplayBox, drawingArea, commandLine);
        }

        private void runCode_Click(object sender, EventArgs e)
        {
            String commandLineInput = commandLine.Text; //reads the command in the 'commandLine'

            if (commandLineInput.Equals("run", StringComparison.InvariantCultureIgnoreCase))
            {
                //CommandParser.color = Color.Black;
                String codeAreaInput = codeArea.Text;
                flag = 1;
                //CommandParser.checkCondition = false;
                startExecution(codeAreaInput);
            }
            else if (commandLineInput.Equals("clear", StringComparison.InvariantCultureIgnoreCase))
            {
                //resets PEN color to default and FILL to off
                //parser.color = Color.Black;
                //parser.fill = false;
                parser.varDictionary.Clear();
                errorDisplayBox.Clear();

                //clears all the sahpes in the array then refreshes the pictureBox so everything dissapears
                CommandParser.shapes.Clear();
                drawingArea.Refresh();
            }
            else if (commandLineInput.Equals("reset", StringComparison.InvariantCultureIgnoreCase))
            {
                //resets moveto position to (0,0)
                CommandParser.penX = 0;
                CommandParser.penY = 0;

                //refresh to implement above changes
                drawingArea.Refresh();
            }
            else if ((string.IsNullOrWhiteSpace(commandLineInput) && commandLine.Text.Length > 0) || commandLine.Text == "")
            {
                errorDisplayBox.Text = "\n⚠️ No command given on the command parser (try: run, clear, reset)";
            }
            else
            {
                flag = 2;
                //runs the command like a multiline command
                startExecution(commandLineInput);
            }
        }

        private void commandLine_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                //peforms click operation of runCode button
                runCode.PerformClick();

                //refresh line number
                displayLineNumber.Refresh();

                // these last two lines will stop the beep sound
                e.SuppressKeyPress = true;
                e.Handled = true;
            }
        }

        private void DrawingArea_Paint(object sender, PaintEventArgs e)
        {
            //draws a 3 by 3 rectangle to help visualize the current position of the 'Moveto' object
            custom.drawCurrMoveToPos(e, CommandParser.penX, CommandParser.penY);

            CommandParser.draw = e.Graphics;

            //draw all shapes stored in the 'shapes' arralist
            custom.drawShapes(CommandParser.shapes, parser.shape, CommandParser.draw, CommandParser.fill);
        }


        //-----------------------MENU ITEMS------------------------------------------
        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                //checks if a file is open and if its is, saves it to the same file 
                using (StreamWriter outputFile = File.CreateText(fileExplorer.FileName))
                {
                    // Write the info to the file. 
                    outputFile.WriteLine(codeArea.Text);
                    outputFile.Close();

                    String filename = Path.GetFileName(fileExplorer.FileName);
                    String message = "Work saved to : " + filename;
                    message += "\nLocation: " + fileExplorer.FileName;
                    MessageBox.Show(message, "ALERT");
                }
            }
            catch (System.ArgumentException)
            {
                //prompts user to save file, if no file is open
                saveFile.RestoreDirectory = true;
                saveFile.Title = "Where do you want to save your work?";
                saveFile.InitialDirectory = @"C:\Users\DELL\OneDrive\Desktop\fourth year\semester 1\Advanced Software Engineering\";
                //saves file filter type
                saveFile.Filter = "Text|*.txt|All|*.*";
                try
                {
                    //displays dialog box and checks if user has selected a file
                    if (saveFile.ShowDialog() == DialogResult.OK)
                    {
                        StreamWriter fWriter = File.CreateText(saveFile.FileName);
                        fWriter.WriteLine(codeArea.Text);
                        fWriter.Close();

                        String filename = Path.GetFileName(saveFile.FileName);
                        String message = "Work saved to : " + filename;
                        //displays the location where the file was saved
                        message += "\nLocation: " + saveFile.FileName;
                        MessageBox.Show(message, "ALERT");
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
            //filters for only text files
            fileExplorer.Filter = "Text|*.txt|All|*.*";
            //title for fie explorer
            fileExplorer.Title = "Choose your file";
            fileExplorer.FilterIndex = 1;
            fileExplorer.InitialDirectory = @"C:\Users\DELL\OneDrive\Desktop\fourth year\semester 1\Advanced Software Engineering\";
            //remembers last visited directory
            fileExplorer.RestoreDirectory = true;
            try
            {
                if (fileExplorer.ShowDialog() == DialogResult.OK)
                {
                    codeArea.Text = ""; //clears text box before loading file contents
                    StreamReader reader = File.OpenText(fileExplorer.FileName);
                    do
                    {
                        String line = reader.ReadLine();
                        if (line == null) break; //breaks if end of file
                        codeArea.Text += line;
                        codeArea.AppendText(Environment.NewLine); // new line starting points are preserved
                    } while (true);
                    reader.Close();
                }
            }

            catch (System.ArgumentException)
            {
                MessageBox.Show("NO FILE CHOSEN !!", "ALERT");
            }
            catch (FileNotFoundException)
            {
                MessageBox.Show("FILE NOT FOUND !!", "ERROR");
            }
            catch (IOException)
            {
                MessageBox.Show("Something went wrong, try again !!", "ERROR");
            }
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            //if (MessageBox.Show("Are you sure you want to exit?", "My First Application", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.No)
            //{
            //    e.Cancel = true;
            //}
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //closes application
            Application.Exit();
        }


        int maxLineCount = 1; //maxLineCount - should be public
        private void codeArea_KeyUp(object sender, KeyEventArgs e)
        {
            //gets the current number of lines using charIndex
            int linecount = codeArea.GetLineFromCharIndex(codeArea.TextLength) + 1;
            
            if (linecount != maxLineCount)
            {
                displayLineNumber.Clear();
                //loops for every line in codeArea
                for (int i = 1; i < linecount + 1; i++)
                {
                    displayLineNumber.AppendText(Convert.ToString(i) + ".\r\n"); //displays the formatted line number to the textBox
                }
                maxLineCount = linecount;
            }
        }

        private void codeArea_VScroll(object sender, EventArgs e)
        {
            displayLineNumber.Text = "";

            //gets the current number of lines using charIndex
            int linecount = codeArea.GetLineFromCharIndex(codeArea.TextLength) + 1;

            if (linecount != maxLineCount)
            {
                displayLineNumber.Clear();

                //loops for every line in codeArea
                for (int i = 1; i < linecount + 1; i++)
                {
                    displayLineNumber.AppendText(Convert.ToString(i) + "\r\n");
                }
                maxLineCount = linecount;
            }
            //refreshes line numbers after scroll
            displayLineNumber.Refresh();
        }
    }
}
