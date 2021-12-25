﻿using System;
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
        Dictionary<int, string> dictionary = new Dictionary<int, string>();

        public Form1()
        {
            InitializeComponent();
        }

        public void startExecution( String input)
        {
            parser.shapes.Clear(); //clears array
            errorDisplayBox.Text = ""; //clears error messages
            string code = input;
            dictionary.Clear();

            string[] codeSpitArray = code.Split(new char[] { '\n' });

            // line number is one
            int lineNumber = 1;
            foreach (string line in codeSpitArray)
            {
                // add the entire line as value and the lineNumber as index
                dictionary.Add(lineNumber, line);
                lineNumber++;
            }

            // stores all possible commands
            string[] possibleCommands = { "DRAWTO", "MOVETO", "CIRCLE", "RECTANGLE", "TRIANGLE" };

            // main execution part of the program (see xml file for full specifications)
            parser.checkForKeywords(possibleCommands, dictionary, errorDisplayBox, drawingArea);
        }

        private void runCode_Click(object sender, EventArgs e)
        {
            String commandLineInput = commandLine.Text; //reads the command in the 'commandLine'

            if (commandLineInput.Equals("run", StringComparison.InvariantCultureIgnoreCase))
            {
                String codeAreaInput = codeArea.Text;
                startExecution(codeAreaInput);
            }
            else if (commandLineInput.Equals("clear", StringComparison.InvariantCultureIgnoreCase))
            {
                //clears all the sahpes in the array then refreshes the pictureBox so everything dissapears
                parser.shapes.Clear();
                drawingArea.Refresh();
            }
            else if (commandLineInput.Equals("reset", StringComparison.InvariantCultureIgnoreCase))
            {
                //resets moveto position to (0,0)
                CommandParser.penX = 0;
                CommandParser.penY = 0;
                drawingArea.Refresh();
            }
            else if ((string.IsNullOrWhiteSpace(commandLineInput) && commandLine.Text.Length > 0) || commandLine.Text == "")
            {
                errorDisplayBox.Text = "\nNo command given on the command parser (try: run, clear, reset)";
            }
            else
            {
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

                // these last two lines will stop the beep sound
                e.SuppressKeyPress = true;
                e.Handled = true;
            }
        }

        private void DrawingArea_Paint(object sender, PaintEventArgs e)
        {
            //draws a 3 by 3 rectangle to help visualize the current position of the 'Moveto' object
            parser.drawCurrMoveToPos(e, CommandParser.penX, CommandParser.penY);

            CommandParser.draw = e.Graphics;

            //draw all shapes stored in the 'shapes' arralist
            parser.drawShapes(parser.shapes, parser.shape, CommandParser.draw);
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
                    StreamReader s = File.OpenText(fileExplorer.FileName);
                    do
                    {
                        String line = s.ReadLine();
                        if (line == null) break; //breaks if end of file
                        codeArea.Text += line;
                        codeArea.AppendText(Environment.NewLine); // new line starting points are preserved
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
            //closes application
            Application.Exit();
        }
    }
}
