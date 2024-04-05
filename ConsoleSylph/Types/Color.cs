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
            new ApplyStatic(),
        };

        public List<(string name, ObjectHolder defaultValue)> Variables => new List<(string, ObjectHolder)>() {
            ("Foreground", new ObjectHolder(15, "int")),
            ("Background", new ObjectHolder(0, "int")),
        };

        public object AppendPropsFromVHolder(object originalTypeEquivalent, VariableHolder vHolder)
        {
            int foreground = (int)vHolder.GetVariable("Foreground").Object;
            int background = (int)vHolder.GetVariable("Background").Object;
            return new Tuple<int, int>(foreground, background);
        }

        public VariableHolder ConvertToVHolder(object typeEquivalent)
        {
            if (typeEquivalent == null)
                return null;
            Tuple<int,int> tuple = typeEquivalent as Tuple<int,int>;
            VariableHolder vh = new VariableHolder();
            vh.AddVariable("Foreground", new ObjectHolder(tuple.Item1, "int"));
            vh.AddVariable("Background", new ObjectHolder(tuple.Item2, "int"));
            return vh;
        }

        public object Clone(object typeEquivalent)
        {
            Tuple<int, int> tuple = typeEquivalent as Tuple<int, int>;
            return new Tuple<int, int>(tuple.Item1, tuple.Item2);
        }
    }
}
