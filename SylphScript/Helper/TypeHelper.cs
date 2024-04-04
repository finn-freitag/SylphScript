using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SylphScript.Helper
{
    public static class TypeHelper
    {
        public static VariableHolder ConvertToVariableHolder(IType type)
        {
            VariableHolder holder = new VariableHolder();
            for(int i = 0; i < type.Variables.Count; i++)
            {
                holder.AddVariable(type.Variables[i].name, type.Variables[i].defaultValue);
            }
            return holder;
        }

        public static List<IFunction> GetStaticFunctions(IType type)
        {
            List<IFunction> staticFunctions = new List<IFunction>();
            for(int i = 0; i < type.SubFunctions.Count; i++)
            {
                if ((type.SubFunctions[i].Modifiers & Modifiers.Static) == Modifiers.Static)
                    staticFunctions.Add(type.SubFunctions[i]);
            }
            return staticFunctions;
        }
    }
}
