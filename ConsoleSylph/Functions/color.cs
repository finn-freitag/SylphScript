using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SylphScript;

namespace ConsoleSylph.Functions
{
    public class color : IFunction
    {
        public IFunction NextFunction { get; set; }
        public ReferenceName AssignedReturnType { get; set; }
        public IFunction[] AssignedParameters { get; set; }

        public ReferenceName FullName => "color";

        public ArgResPermutation Parameters => ArgResPermutation.Build()
            .Add("null", "int", "int")
            .Add("null", "int", "null")
            .Add("null", "null", "int");

        public IFunction GetNewInstance()
        {
            return new color();
        }

        public ObjectHolder GetResult(VariableHolder variableHolder)
        {
            if (AssignedParameters[0] != null) Console.BackgroundColor = IntToColor((int)AssignedParameters[0].GetResult(variableHolder).Object, ConsoleColor.Black);
            if (AssignedParameters[1] != null) Console.ForegroundColor = IntToColor((int)AssignedParameters[1].GetResult(variableHolder).Object, ConsoleColor.White);
            return ObjectHolder.Null;
        }

        private ConsoleColor IntToColor(int i, ConsoleColor Default)
        {
            // Strange order but the same as the color command (color -r) shows.
            if (i == 0) return ConsoleColor.Black;
            if (i == 1) return ConsoleColor.DarkBlue;
            if (i == 2) return ConsoleColor.DarkGreen;
            if (i == 3) return ConsoleColor.DarkCyan;
            if (i == 4) return ConsoleColor.DarkRed;
            if (i == 5) return ConsoleColor.DarkMagenta;
            if (i == 6) return ConsoleColor.DarkYellow;
            if (i == 7) return ConsoleColor.Gray;
            if (i == 8) return ConsoleColor.DarkGray;
            if (i == 9) return ConsoleColor.Blue;
            if (i == 10) return ConsoleColor.Green;
            if (i == 11) return ConsoleColor.Cyan;
            if (i == 12) return ConsoleColor.Red;
            if (i == 13) return ConsoleColor.Magenta;
            if (i == 14) return ConsoleColor.Yellow;
            if (i == 15) return ConsoleColor.White;
            return Default;
        }
    }
}
