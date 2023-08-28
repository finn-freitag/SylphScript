using SylphScript.Functions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SylphScript
{
    public class VariableHolder
    {
        public VariableHolder Parent = null;
        public ReferenceName PositionFullName = "";
        public Dictionary<ReferenceName, ObjectHolder> Variables = new Dictionary<ReferenceName, ObjectHolder>();

        public VariableHolder()
        {
            
        }

        public void InitAsBase()
        {
            print printC = new print();
            Variables.Add(printC.FullName, new ObjectHolder(printC, printC.FullName));
            _Assignment _Assignment = new _Assignment();
            Variables.Add(_Assignment.FullName, new ObjectHolder(_Assignment, _Assignment.FullName));
        }

        public void AddVariable(ReferenceName name, ObjectHolder value)
        {
            if (Variables.ContainsKey(name)) throw new InvalidOperationException("This variable still exists!");
            Variables.Add(name, value);
        }

        public ObjectHolder GetVariable(ReferenceName name)
        {
            if(Variables.ContainsKey(name))return Variables[name];
            if (Parent == null) throw new InvalidOperationException("Variable does not exist!");
            return Parent.GetVariable(name);
        }

        public VariableHolder GetSubHolder(string AdditionalPositionName)
        {
            VariableHolder holder = new VariableHolder();
            holder.Parent = this;
            holder.PositionFullName += "." + AdditionalPositionName;
            return holder;
        }
    }
}
