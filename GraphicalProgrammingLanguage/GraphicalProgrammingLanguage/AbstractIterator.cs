using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphicalProgrammingLanguage
{
    interface AbstractIterator
    {
        Variable First();
        Variable Next();
        bool IsCompleted { get; }
    }
}
