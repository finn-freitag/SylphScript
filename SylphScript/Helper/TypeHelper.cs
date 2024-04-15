using SylphScript.Types;
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

        public static IFunctionRegistry GetStaticFunctions(IType type)
        {
            IFunctionRegistry staticFunctions = new SubFunctionsRegistry();
            for(int i = 0; i < type.SubFunctions.FunctionList.Count; i++)
            {
                if ((type.SubFunctions.FunctionList[i].Modifiers & Modifiers.Static) == Modifiers.Static)
                    staticFunctions.FunctionList.Add(type.SubFunctions.FunctionList[i]);
            }
            return staticFunctions;
        }

        public static ObjectHolder CreateInstance(IType type)
        {
            return new ObjectHolder(null, type.Name);
            /*VariableHolder vh = new VariableHolder();
            for(int i = 0; i < type.Variables.Count; i++)
            {
                if(type is CustomType ct)
                    vh.AddVariable(type.Variables[i].name, type.Variables[i].defaultValue.Clone(), ct.ReadonlyVars.Contains(type.Variables[i].name));
                else
                    vh.AddVariable(type.Variables[i].name, type.Variables[i].defaultValue.Clone());
            }
            return new ObjectHolder(null, type.Name, vh);*/
        }
    }
}
