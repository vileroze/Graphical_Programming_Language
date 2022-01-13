using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphicalProgrammingLanguage
{
    class Variable
    {
        public string name { get; set; }
        public int value { get; set; }
        
        public Variable (string name, int value)
        {
            name = name;
            value = value;
        }
    }
}
