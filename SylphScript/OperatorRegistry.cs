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
            new AddTwoDoubles(),
            new AddTwoIntegers(),
            new DivideTwoDoubles(),
            new DivideTwoIntegers(),
            new ModuloTwoDoubles(),
            new ModuloTwoIntegers(),
            new MultiplicateTwoDoubles(),
            new MultiplicateTwoIntegers(),
            new SubtractTwoDoubles(),
            new SubtractTwoIntegers(),
            new ConcatCharAndChar(),
            new ConcatCharAndString(),
            new ConcatStringAndChar(),
            new ConcatStringAndString(),
        };

        public static bool OperatorExists(string Operator)
        {
            return GetOperator(Operator) != null;
        }

        public static IOperator[] GetOperator(string Operator)
        {
            List<IOperator> ops = new List<IOperator>();
            for(int i = 0; i < Operators.Count; i++)
            {
                if (Operators[i].OperatorName == Operator) ops.Add(Operators[i]);
            }
            return ops.ToArray();
        }

        public static string[] GetOperatorStrings()
        {
            List<string> ops = new List<string>();
            for (int i = 0; i < Operators.Count; i++)
            {
                ops.Add(Operators[i].OperatorName);
            }
            return ops.ToArray();
        }
    }
}
