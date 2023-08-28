using SylphScript.Operators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SylphScript
{
    public static class OperatorRegistry
    {
        public static List<IOperator> Operators = new List<IOperator>()
        {
            new AddTwoIntegers(),
            new DivideTwoIntegers(),
            new ModuloTwoIntegers(),
            new MultiplicateTwoIntegers(),
            new SubtractTwoIntegers(),
        };

        public static IOperator GetOperator(ReferenceName type1, ReferenceName type2)
        {
            for(int i = 0; i < Operators.Count; i++)
            {
                if (Operators[i].Type1 == type1 && Operators[i].Type2 == type2) return Operators[i];
            }

            return null;
        }
    }
}
