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
            new ConcatIntegerAndString(),
            new ConcatStringAndInteger(),
            new ConcatDoubleAndString(),
            new ConcatStringAndDouble(),
            new LogicAndBoolAndBool(),
            new LogicAndByteAndByte(),
            new LogicAndCharAndChar(),
            new LogicAndIntegerAndInteger(),
            new LogicEqualsBoolAndBool(),
            new LogicEqualsByteAndByte(),
            new LogicEqualsCharAndChar(),
            new LogicEqualsDoubleAndDouble(),
            new LogicEqualsIntegerAndInteger(),
            new LogicEqualsObjectAndObject(),
            new LogicEqualsStringAndString(),
            new LogicGreaterDoubleAndDouble(),
            new LogicGreaterIntegerAndInteger(),
            new LogicNot(),
            new LogicNotEqualsBoolAndBool(),
            new LogicNotEqualsByteAndByte(),
            new LogicNotEqualsCharAndChar(),
            new LogicNotEqualsDoubleAndDouble(),
            new LogicNotEqualsIntegerAndInteger(),
            new LogicNotEqualsObjectAndObject(),
            new LogicNotEqualsStringAndString(),
            new LogicOrBoolAndBool(),
            new LogicOrByteAndByte(),
            new LogicOrCharAndChar(),
            new LogicOrIntegerAndInteger(),
            new LogicSmallerDoubleAndDouble(),
            new LogicSmallerIntegerAndInteger(),
            new LogicXorBoolAndBool(),
            new LogicXorByteAndByte(),
            new LogicXorCharAndChar(),
            new LogicXorIntegerAndInteger(),

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
                if (char.IsLetterOrDigit(Operators[i].OperatorName[0]))
                    throw new OperatorException(Operators[i].OperatorName);
                else if (Operators[i].OperatorName == Operator)
                    ops.Add(Operators[i]);
            }
            return ops.ToArray();
        }
    }
}
