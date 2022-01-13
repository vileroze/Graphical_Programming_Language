using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GraphicalProgrammingLanguage
{
    public class SaveCommand : MCommand
    {
        private Menu menu;
        RichTextBox codeArea;

        public SaveCommand(Menu menuItems, RichTextBox ogCodeArea)
        {
            menu = menuItems;
            codeArea = ogCodeArea;
        }

        public void Execute()
        {
            menu.Save(codeArea);
        }
    }
}
