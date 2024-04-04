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
        public List<string> ReadonlyVariables = new List<string>();
        public ReturnCallback ReturnCallbackFunc = null;

        public VariableHolder()
        {
            
        }

        public void AddVariable(string name, ObjectHolder value, bool Readonly = false)
        {
            string[] parts = name.Split('.');
            if(parts.Length == 1)
            {
                if(Variables.ContainsKey(name) || (Parent != null && Parent.VariableExist(name)))
                    throw new InvalidOperationException("This variable does already exist!");
            }
            else if (!Variables.ContainsKey(parts[0]))
            {
                if (Parent != null)
                {
                    Parent.AddVariable(name, value, Readonly);
                    return;
                }
                else
                    throw new InvalidOperationException("This variable doesn't exist!");
            }
            VariableHolder lastVHolder = this;
            for (int i = 0; i < parts.Length - 1; i++)
            {
                if (!lastVHolder.Variables.ContainsKey(parts[i]))
                    throw new InvalidOperationException("This variable doesn't exist!");
                lastVHolder = lastVHolder.Variables[parts[i]].SubHolder;
            }
            string lastPart = parts[parts.Length - 1];
            if (lastVHolder.Variables.ContainsKey(lastPart))
                throw new InvalidOperationException("This variable does already exists!");
            lastVHolder.Variables.Add(lastPart, value);
            if (Readonly)
                ReadonlyVariables.Add(lastPart);
        }

        public void SetVariable(string name, ObjectHolder value, bool keepRefs = false, bool Readonly = false)
        {
            string[] parts = name.Split('.');
            if (!Variables.ContainsKey(parts[0]))
            {
                if (Parent != null)
                {
                    Parent.SetVariable(name, value, keepRefs, Readonly);
                    return;
                }
                else
                    throw new InvalidOperationException("This variable doesn't exist!");
            }
            VariableHolder lastVHolder = this;
            for (int i = 0; i < parts.Length - 1; i++)
            {
                if (!lastVHolder.Variables.ContainsKey(parts[i]))
                    throw new InvalidOperationException("This variable doesn't exist!");
                lastVHolder = lastVHolder.Variables[parts[i]].SubHolder;
            }
            string lastPart = parts[parts.Length - 1];
            if (!lastVHolder.Variables.ContainsKey(lastPart))
                throw new InvalidOperationException("This variable doesn't exist!");
            if (lastVHolder.Variables[lastPart].TypeFullName != value.TypeFullName)
                throw new InvalidOperationException("Variable type doesn't match the type of assignment!");
            if (lastVHolder.ReadonlyVariables.Contains(lastPart))
                throw new InvalidOperationException("This variable is readonly!");
            if(keepRefs)
                lastVHolder.Variables[lastPart].Object = value.Object;
            else
                lastVHolder.Variables[lastPart] = value;
            if (Readonly)
                lastVHolder.ReadonlyVariables.Add(lastPart);
        }

        public ObjectHolder GetVariable(string name)
        {
            string[] parts = name.Split('.');
            if (!Variables.ContainsKey(parts[0]))
            {
                if (Parent != null)
                    return Parent.GetVariable(name);
                else
                    throw new InvalidOperationException("This variable doesn't exist!");
            }
            VariableHolder lastVHolder = this;
            for (int i = 0; i < parts.Length - 1; i++)
            {
                if (!lastVHolder.Variables.ContainsKey(parts[i]))
                    throw new InvalidOperationException("This variable doesn't exist!");
                lastVHolder = lastVHolder.Variables[parts[i]].SubHolder;
            }
            string lastPart = parts[parts.Length - 1];
            if (lastVHolder.Variables.ContainsKey(lastPart))
                return lastVHolder.Variables[lastPart];
            throw new InvalidOperationException("This variable doesn't exist!");
        }

        public bool VariableExist(string name)
        {
            string[] parts = name.Split('.');
            if (!Variables.ContainsKey(parts[0]))
            {
                if (Parent != null)
                    return Parent.VariableExist(name);
                else
                    return false;
            }
            VariableHolder lastVHolder = this;
            for (int i = 0; i < parts.Length - 1; i++)
            {
                if (!lastVHolder.Variables.ContainsKey(parts[i]))
                    return false;
                lastVHolder = lastVHolder.Variables[parts[i]].SubHolder;
            }
            string lastPart = parts[parts.Length - 1];
            return lastVHolder.Variables.ContainsKey(lastPart) || (Parent != null && Parent.VariableExist(name));
        }

        public bool IsVariableReadonly(string name)
        {
            string[] parts = name.Split('.');
            if (!Variables.ContainsKey(parts[0]))
            {
                if (Parent != null)
                    return Parent.VariableExist(name);
                else
                    throw new InvalidOperationException("This variable doesn't exist!");
            }
            VariableHolder lastVHolder = this;
            for (int i = 0; i < parts.Length - 1; i++)
            {
                if (!lastVHolder.Variables.ContainsKey(parts[i]))
                    throw new InvalidOperationException("This variable doesn't exist!");
                lastVHolder = lastVHolder.Variables[parts[i]].SubHolder;
            }
            string lastPart = parts[parts.Length - 1];
            if(!(lastVHolder.Variables.ContainsKey(lastPart) || (Parent != null && Parent.VariableExist(name))))
                throw new InvalidOperationException("This variable doesn't exist!");
            return lastVHolder.ReadonlyVariables.Contains(lastPart);
        }

        public VariableHolder GetSubHolder(string AdditionalPositionName)
        {
            VariableHolder holder = new VariableHolder();
            holder.Parent = this;
            holder.PositionFullName = PositionFullName + "." + AdditionalPositionName;
            return holder;
        }

        public VariableHolder Clone()
        {
            VariableHolder vh = new VariableHolder();
            vh.Parent = Parent;
            vh.PositionFullName = PositionFullName;
            vh.ReturnCallbackFunc = ReturnCallbackFunc;
            foreach(var item in Variables)
            {
                vh.Variables.Add(item.Key, item.Value.Clone());
            }
            return vh;
        }
    }
}
