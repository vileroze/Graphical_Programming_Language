using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GraphicalProgrammingLanguage
{
    public partial class Form1 : Form
    {
        Shape s;
        ShapeFactory factory = new ShapeFactory();
        ArrayList shapes = new ArrayList();
        ArrayList codeSplitArrayList = new ArrayList();
        

        public Form1()
        {
            InitializeComponent();
        }

        private void runCode_Click(object sender, EventArgs e)
        {
            ShapeFactory factory = new ShapeFactory();
            string code = codeArea.Text;
            string[] codeSplit = code.Split(new Char[] { ',', ' ', '\n' },
                                 StringSplitOptions.RemoveEmptyEntries);
            string[] possibleCommands = { "DRAWTO", "MOVETO", "CIRCLE", "RECTANGLE", "TRIANGLE" };

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
        }
    }
}
