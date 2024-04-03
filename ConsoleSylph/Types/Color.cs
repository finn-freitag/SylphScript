using ConsoleSylph.Types.ColorFunctions;
using SylphScript;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleSylph.Types
{
    public class Color : IType
    {
        public string Name => "Color";

        public List<IFunction> SubFunctions => new List<IFunction>(){
            new Apply(),
        };

        public List<(string name, ObjectHolder defaultValue)> Variables => new List<(string, ObjectHolder)>() {
            ("Foreground", new ObjectHolder(15, "int")),
            ("Background", new ObjectHolder(0 , "int")),
        };

        public VariableHolder ConvertToVHolder(object typeEquivalent, ValueHolder valueHolder)
        {
            Tuple<int,int> tuple = typeEquivalent as Tuple<int,int>;
            VariableHolder vh = new VariableHolder(valueHolder);
            vh.AddVariable("Foreground", new ObjectHolder(tuple.Item1, "int"));
            vh.AddVariable("Background", new ObjectHolder(tuple.Item2, "int"));
            return vh;
        }
    }
}
