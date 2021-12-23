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

        private void runCode_Click(object sender, EventArgs e)
        {
            String commandLineInput = commandLine.Text; //reads the command in the 'commandLine'

            if (commandLineInput.Equals("run", StringComparison.InvariantCultureIgnoreCase))
            {
                String codeAreaInput = codeArea.Text;
                parser.shapes.Clear();
                errorDisplayBox.Text = "";
                parser.keyIndex = 0;
                string code = codeAreaInput;
                string[] codeSplit = code.Split(new Char[] { ',', ' ', '\n' }, StringSplitOptions.RemoveEmptyEntries);
                string[] possibleCommands = { "DRAWTO", "MOVETO", "CIRCLE", "RECTANGLE", "TRIANGLE" };

                //return array list and store in variable
                var returnedArrayList = parser.returnArrayList(codeSplit);

                //check keyword
                parser.checkForKeywords(codeArea, possibleCommands, returnedArrayList, errorDisplayBox, DrawingArea);
            }
            else if (commandLineInput.Equals("clear", StringComparison.InvariantCultureIgnoreCase))
            {
                parser.shapes.Clear();
                DrawingArea.Refresh();

            }
            else if (commandLineInput.Equals("reset", StringComparison.InvariantCultureIgnoreCase))
            {
                CommandParser.penX = 0;
                CommandParser.penY = 0;
                DrawingArea.Refresh();

            }
            else if (string.IsNullOrWhiteSpace(commandLineInput) && commandLine.Text.Length > 0)
            {
                commandLine.Text = "no command given";
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

        private void commandLine_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                runCode.PerformClick();

                // these last two lines will stop the beep sound
                e.SuppressKeyPress = true;
                e.Handled = true;
            }
        }
    }
}
