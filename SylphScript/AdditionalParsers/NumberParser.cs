using SylphScript.Functions;
using SylphScript.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SylphScript.AdditionalParsers
{
    public class NumberParser : IAdditionalParser
    {
        public bool isTypeParser => true;

        public (IFunction Function, bool Success) Parse(ref int index, string code, VariableHolder vHolder)
        {
            int multiplier = 1;
            if (code[index] == '-')
            {
                multiplier = -1;
                index++;
            }
            int num1;
            if (!ParserHelper.GetNumber(ref index, code, out num1))
                return (null, false);
            num1 *= multiplier;
            int num2 = 0;
            if (index < code.Length && code[index] == '.')
            {
                index++;
                ParserHelper.GetNumber(ref index, code, out num2);
            }
            if (num2 == 0)
            {
                return (new _constFunction(new ObjectHolder(num1, "int")), true);
            }
            else
            {
                return (new _constFunction(new ObjectHolder(num1 + num2 * multiplier * (Math.Pow(0.1, Convert.ToString(num2).Length)), "double")), true);
            }
            //try
            //{
            //    int multiplier = 1;
            //    if (code[index] == '-') multiplier = -1;
            //    double number = 0;
            //    double number2 = 0;
            //    bool isSecond = false;
            //    bool first = true;
            //    int maxI = index;
            //    for (int i = index + (multiplier < 0 ? 1 : 0); i < code.Length; i++)
            //    {
            //        maxI = i;
            //        if (code[i] == '.')
            //        {
            //            isSecond = true;
            //        }
            //        else
            //        {
            //            if (char.IsDigit(code[i]))
            //            {
            //                if (!isSecond)
            //                {
            //                    number *= 10;
            //                    number += Convert.ToInt32(code[i] + "");
            //                }
            //                else
            //                {
            //                    if (first)
            //                    {
            //                        number2 += 1;
            //                        first = false;
            //                    }
            //                    number2 *= 10;
            //                    number2 += Convert.ToInt32(code[i] + "");
            //                }
            //            }
            //            else break;
            //        }
            //    }
            //    if (number2 != 0) number += (number2 / Math.Round(Math.Pow(10, Convert.ToString(number2).Length))) * 10 - 1;
            //    number *= multiplier;
            //    string numAsStr = Convert.ToString(number);
            //    index = maxI + 1;
            //    if (numAsStr.Contains("."))
            //        return (new _constFunction(new ObjectHolder(number, "double")), true);
            //    else
            //        return (new _constFunction(new ObjectHolder((int)number, "int")), true);
            //}
            //catch { }
            //return (null, false);
        }
    }
}
