using SylphScript;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleSylph.Types.ColorFunctions
{
    public class Color : IFunction
    {
        public IFunction NextFunction { get; set; }
        public ReferenceName AssignedReturnType { get; set; }
        public IFunction[] AssignedParameters { get; set; }
        public ReferenceName ReferenceObject { get; set; }

        public ReferenceName FullName => "Color";

        public ArgResPermutation Parameters => ArgResPermutation.Build()
            .Add("Color")
            .Add("Color", "int")         // Foreground
            .Add("Color", "int", "int"); // Foreground, Background

        public IFunction GetNewInstance()
        {
            return new Color();
        }

        public ObjectHolder GetResult(VariableHolder variableHolder)
        {
            if (AssignedParameters.Length == 0)
                return new ObjectHolder(new Tuple<int, int>(15, 0), "Color");
            if (AssignedParameters.Length == 1)
                return new ObjectHolder(new Tuple<int, int>((int)AssignedParameters[0].GetResult(variableHolder).Object, 0), "Color");
            if (AssignedParameters.Length == 2)
                return new ObjectHolder(new Tuple<int, int>((int)AssignedParameters[0].GetResult(variableHolder).Object, (int)AssignedParameters[1].GetResult(variableHolder).Object), "Color");
            throw new InvalidOperationException();
        }
    }
}
