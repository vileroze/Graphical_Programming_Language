using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphicalProgrammingLanguage
{
    class ConcreteCollection : AbstractCollection
    {
        private List<Variable> listVariables = new List<Variable>();
        //Create Iterator
        public Iterator CreateIterator()
        {
            return new Iterator(this);
        }

        // Gets item count
        public int Count
        {
            get { return listVariables.Count; }
        }

        //Add items to the collection
        public void AddVariable(Variable variable)
        {
            listVariables.Add(variable);
        }

        //Get item from collection
        public Variable GetVariable(int IndexPosition)
        {
            return listVariables[IndexPosition];
        }

        //clear list
        public void clearVariable()
        {
            listVariables.Clear();
        }
    }
}
