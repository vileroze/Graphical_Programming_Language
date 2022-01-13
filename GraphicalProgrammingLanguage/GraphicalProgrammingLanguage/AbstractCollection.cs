using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphicalProgrammingLanguage
{
    interface AbstractCollection
    {
        Iterator CreateIterator();
    }
}
