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
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GraphicalProgrammingLanguage
{
    public partial class Form1 : Form
    {
        //public static Thread colorFlash;
        bool flashFlag = false, running = false;
        FlashShape flash;

        //flag to stop flashing
        public static int abortFlag = 0;

        CheckShape chShape = new CheckShape();

        CommandParser parser = new CommandParser();
        CustomMethods custom = new CustomMethods();
        CheckKeyword chkKeyword = new CheckKeyword();

        public static Dictionary<int, string> mainDictionary = new Dictionary<int, string>();
        ArrayList singleLineCommand = new ArrayList();

        //flag set to determine if command is from commandLine or codeArea (i.e 1 for codeArea and 2 for commandLine)
        static int flag = 0;
        static int debugFlag = 0;

        // stores all possible commands
        public string[] possibleCommands = { "DRAWTO", "MOVETO", "CIRCLE", "RECTANGLE", "TRIANGLE", "PEN", "FILL", "POLYGON", "IF", "ENDIF", "WHILE", "ENDLOOP"};
        public string[] possibleComplexCommands = {"METHOD", "ENDMETHOD"};


        //xxxxxxxxxxxxxxxxxxxxxxxxxxxxxx
        public static Menu menu = new Menu();
        

        public static MCommand loadCommand;
        public static MCommand saveCommand;
        public static MCommand closeCommand;
        MenuOptions menuOpt;



        public Form1()
        {
            //supresses cross thread call errros
            PictureBox.CheckForIllegalCrossThreadCalls = false;
            InitializeComponent();
            flash = new FlashShape(this, CheckKeyword.shapes);


            loadCommand = new LoadCommand(menu, codeArea);
            saveCommand = new SaveCommand(menu, codeArea);
            closeCommand = new CloseCommand(menu);

            menuOpt = new MenuOptions(loadCommand, saveCommand, closeCommand);
        }

        /// <summary>
        /// made this so the PictureBox (drawingArea) 'refresh' method can be called on another class 
        /// </summary>
        public void refreshPictureBox()
        {
            drawingArea.Refresh();
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
                mainDictionary.Clear();

                // split lines 
                string[] splitLine = code.Split(new char[] { '\n' });
                int lineNumber = 1;
                foreach (string line in splitLine)
                {
                    // add the entire line as value and the lineNumber as key
                    mainDictionary.Add(lineNumber, line);
                    Debug.WriteLine(line);
                    lineNumber++;
                }
            }

            //for single line codes i.e commandLine
            if (flag == 2)
            {
                if (CheckKeyword.shapes.Count > 0) { singleLineCommand.Clear(); }
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
                    mainDictionary.Add(lineNumber, line);
                    lineNumber++;
                }
            }

            // main execution part of the program (see xml file for full specifications)
            parser.mainParser(possibleCommands, possibleComplexCommands, mainDictionary, errorDisplayBox, drawingArea, commandLine);
        }

        private void runCode_Click(object sender, EventArgs e)
        {
            if (CommandParser.breakLoopFlag == 0)
            {
                debugFlag = 1;
                forExecution();
            }
            else
            {
                runCode.Enabled = false;
            }
        }

        public void forExecution()
        {
            String commandLineInput = commandLine.Text; //reads the command in the 'commandLine'

            if (commandLineInput.Equals("run", StringComparison.InvariantCultureIgnoreCase))
            {
                CheckMethod.methodTuple.Clear();
                CheckMethod.parameters.Clear();
                mainDictionary.Clear();
                String codeAreaInput = codeArea.Text;
                flag = 1;
                abortFlag = 0;
                startExecution(codeAreaInput);
            }
            else if (commandLineInput.Equals("clear", StringComparison.InvariantCultureIgnoreCase))
            {
                //parser.varDictionary.Clear();
                CommandParser.varDictionary.Clear();
                errorDisplayBox.Clear();

                //clears all the sahpes in the array then refreshes the pictureBox so everything dissapears
                CheckKeyword.shapes.Clear();

                //stop the flashing if on
                abortFlag = 1;

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
                errorDisplayBox.Text = "\n⚠️ No command given on the command parser (try: run, clear, reset or any of the other possible commands)";
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
            if(debugFlag == 1)
            {
                //draws a 3 by 3 rectangle to help visualize the current position of the 'Moveto' object
                custom.drawCurrMoveToPos(e, CommandParser.penX, CommandParser.penY);

                CommandParser.draw = e.Graphics;

                //draw all shapes stored in the 'shapes' arralist
                custom.drawShapes(CheckKeyword.shapes, chkKeyword.shape, CommandParser.draw, CommandParser.fill);
            }
            
        }


        //-----------------------MENU ITEMS------------------------------------------
        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            menuOpt.clickSave();
        }

        private void loadToolStripMenuItem_Click(object sender, EventArgs e)
        {
            menuOpt.clickLoad();
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (MessageBox.Show("Are you sure you want to exit?", "My First Application", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.No)
            {
                e.Cancel = true;
            }
            else
            {
                //terminate thread
                FlashShape.thread.Abort();
            }
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //closes application
            menuOpt.clickClose();
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
        //realign line numbers when codeArea is scrolled
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

        private void debugButton_Click(object sender, EventArgs e)
        {
            debugFlag = 0;
            forExecution();
            if (CommandParser.breakLoopFlag == 0)
            {
                runCode.Enabled = true;
            }
            else
            {
                runCode.Enabled = false;
            }
        }
    }
}
