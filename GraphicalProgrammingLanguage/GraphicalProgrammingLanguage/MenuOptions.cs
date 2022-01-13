using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphicalProgrammingLanguage
{
    public class MenuOptions
    {
        private MCommand loadCommand;
        private MCommand saveCommand;
        private MCommand closeCommand;

        public MenuOptions(MCommand load, MCommand save, MCommand close)
        {
            this.loadCommand = load;
            this.saveCommand = save;
            this.closeCommand = close;
        }

        public void clickLoad()
        {
            loadCommand.Execute();
        }

        public void clickSave()
        {
            saveCommand.Execute();
        }
        public void clickClose()
        {
            closeCommand.Execute();
        }
    }
}
