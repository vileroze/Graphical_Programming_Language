using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphicalProgrammingLanguage
{
    class NegativeNumberException: Exception
    {
        public NegativeNumberException(String message) : base(message)
        {

        }
    }
}
