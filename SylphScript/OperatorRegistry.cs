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
    }
}
