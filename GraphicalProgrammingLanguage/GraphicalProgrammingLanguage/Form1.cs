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

        CommandParser parser = new CommandParser();


        public Form1()
        {
            InitializeComponent();
        }

        public void startProgram( String input)
        {
            parser.shapes.Clear();
            parser.charIndex = 0;
            errorDisplayBox.Text = "";
            parser.keyIndex = 0;
            string code = input;
            string[] codeSplit = code.Split(new Char[] {' ', ',', '\n' }, StringSplitOptions.RemoveEmptyEntries);


            
            //string[] codeSplit = code.Split('\n');
            string[] possibleCommands = { "DRAWTO", "MOVETO", "CIRCLE", "RECTANGLE", "TRIANGLE" };

            //return array list and store in variable
            var returnedArrayList = parser.returnArrayList(codeSplit);

            //check keyword
            parser.checkForKeywords(codeArea, possibleCommands, returnedArrayList, errorDisplayBox, drawingArea);
        }

        private void runCode_Click(object sender, EventArgs e)
        {

            String commandLineInput = commandLine.Text; //reads the command in the 'commandLine'

            if (commandLineInput.Equals("run", StringComparison.InvariantCultureIgnoreCase))
            {
                String codeAreaInput = codeArea.Text;
                startProgram(codeAreaInput);
            }
            else if (commandLineInput.Equals("clear", StringComparison.InvariantCultureIgnoreCase))
            {
                parser.shapes.Clear();
                drawingArea.Refresh();

            }
            else if (commandLineInput.Equals("reset", StringComparison.InvariantCultureIgnoreCase))
            {
                CommandParser.penX = 0;
                CommandParser.penY = 0;
                drawingArea.Refresh();

            }
            else if ((string.IsNullOrWhiteSpace(commandLineInput) && commandLine.Text.Length > 0) || commandLine.Text == "")
            {
                errorDisplayBox.Text += "\nNo command given on the command parser (try: run, clear, reset)";
            }
            else
            {
                startProgram(commandLineInput);
            }
        }

        private void commandLine_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                //peforms click operation of runCode button
                runCode.PerformClick();

                // these last two lines will stop the beep sound
                e.SuppressKeyPress = true;
                e.Handled = true;
            }
        }

        private void DrawingArea_Paint(object sender, PaintEventArgs e)
        {
            //draws a 3 by 3 rectangle to help visualize the current position of the 'Moveto' object
            parser.drawCurrMoveToPos(e, CommandParser.penX, CommandParser.penY);

            //draw all shapes stored in the 'shapes' arralist
            CommandParser.draw = e.Graphics;
            parser.drawShapes(parser.shapes, parser.s, CommandParser.draw);
        }


        //-----------------------MENU ITEMS------------------------------------------
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
                    String message = "Work saved to : " + filename;
                    message += "\nLocation: " + fileExplorer.FileName;
                    MessageBox.Show(message, "ALERT");
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
                        String message = "Work saved to : " + filename;
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

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
