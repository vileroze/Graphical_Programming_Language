using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GraphicalProgrammingLanguage
{
    public class Menu
    {
        OpenFileDialog fileExplorer = new OpenFileDialog();
        SaveFileDialog saveFile = new SaveFileDialog();

        /// <summary>
        /// load the contents of the selected text file to the RichTextBox that is passed
        /// </summary>
        /// <param name="codeArea">RichTextBox to display the contents of the file that is loaded</param>
        public void Load(RichTextBox codeArea)
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
                MessageBox.Show("NO FILE CHOSEN !!", "ALERT", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            catch (FileNotFoundException)
            {
                MessageBox.Show("FILE NOT FOUND !!", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            catch (IOException)
            {
                MessageBox.Show("Something went wrong, try again !!", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// save the contents of the RichTextBox to a text file in a location specified by the user
        /// </summary>
        /// <param name="codeArea">contains the contents that is to be read ans saved</param>
        public void Save(RichTextBox codeArea)
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
                    MessageBox.Show(message, "ALERT", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
                        MessageBox.Show(message, "ALERT", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                catch (IOException)
                {
                    MessageBox.Show("Error", "IO exception");
                }
            }
        }

        /// <summary>
        /// exit applicaiton
        /// </summary>
        public void Close()
        {
            Application.Exit();
        }
    }
}
