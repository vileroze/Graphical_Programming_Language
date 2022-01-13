using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphicalProgrammingLanguage
{
    class Iterator : AbstractIterator
    {
        private ConcreteCollection collection;
        private int current = 0;
        private int step = 1;
        // Constructor
        public Iterator(ConcreteCollection collection)
        {
            this.collection = collection;
        }
        // Gets first it 
        public Variable First()
        {
            current = 0;
            return collection.GetVariable(current);
        }
        // Gets next item
        public Variable Next()
        {
            current += step;
            if (!IsCompleted)
            {
                return collection.GetVariable(current);
            }
            else
            {
                return null;
            }
        }
        // Check whether iteration is complete
        public bool IsCompleted
        {
            get { return current >= collection.Count; }
        }
    }
}
