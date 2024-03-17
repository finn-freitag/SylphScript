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
        public delegate void ReturnCallback(ObjectHolder value);

        public VariableHolder Parent = null;
        public ReferenceName PositionFullName = "";
        public Dictionary<string, ObjectHolder> Variables = new Dictionary<string, ObjectHolder>();
        public ReturnCallback ReturnCallbackFunc = null;

        public VariableHolder()
        {
            
        }

        public void AddVariable(string name, ObjectHolder value)
        {
            if (Variables.ContainsKey(name)) throw new InvalidOperationException("This variable does already exists!");
            Variables.Add(name, value);
        }

        public void SetVariable(string name, ObjectHolder value)
        {
            if (!Variables.ContainsKey(name))
            {
                if (Parent == null) throw new InvalidOperationException("This variable doesn't exist!");
                Parent.SetVariable(name, value);
                return;
            }
            if (Variables[name].TypeFullName != value.TypeFullName) throw new InvalidOperationException("Variable type doesn't match the type of assignment!");
            Variables[name].Object = value.Object;
        }

        public ObjectHolder GetVariable(string name)
        {
            if(Variables.ContainsKey(name))return Variables[name];
            if (Parent == null) throw new InvalidOperationException("This variable doesn't exist!");
            return Parent.GetVariable(name);
        }

        public bool VariableExist(string name)
        {
            return Variables.ContainsKey(name) || (Parent != null && Parent.VariableExist(name));
        }

        public VariableHolder GetSubHolder(string AdditionalPositionName)
        {
            VariableHolder holder = new VariableHolder();
            holder.Parent = this;
            holder.PositionFullName = this.PositionFullName + "." + AdditionalPositionName;
            return holder;
        }
    }
}
