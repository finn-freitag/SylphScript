using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SylphScript.Helper
{
    public static class TypeHelper
    {
        public static VariableHolder ConvertToVariableHolder(IType type, ValueHolder valueHolder)
        {
            VariableHolder holder = new VariableHolder(valueHolder);
            for(int i = 0; i < type.Variables.Count; i++)
            {
                holder.AddVariable(type.Variables[i].name, type.Variables[i].defaultValue);
            }
            return holder;
        }
    }
}
