using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphicalProgrammingLanguage
{
    public class CloseCommand : MCommand
    {
        private Menu menu;

        public CloseCommand(Menu menuItems)
        {
            menu = menuItems;
        }

        public void Execute()
        {
            menu.Close();
        }
    }
}
