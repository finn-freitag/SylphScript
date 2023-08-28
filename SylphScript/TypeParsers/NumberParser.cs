﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SylphScript.TypeParsers
{
    public class NumberParser : ITypeParser
    {
        public (ObjectHolder Object, bool Success) Parse(ref int index, string code)
        {
            try
            {
                int multiplier = 1;
                if (code[index] == '-') multiplier = -1;
                double number = 0;
                double number2 = 0;
                bool isSecond = false;
                bool first = true;
                int maxI = index;
                for (int i = index + (multiplier < 0 ? 1 : 0); i < code.Length; i++)
                {
                    maxI = i;
                    if (code[i] == '.')
                    {
                        isSecond = true;
                    }
                    else
                    {
                        if (char.IsDigit(code[i]))
                        {
                            if (!isSecond)
                            {
                                number *= 10;
                                number += Convert.ToInt32(code[i] + "");
                            }
                            else
                            {
                                if (first)
                                {
                                    number2 += 1;
                                    first = false;
                                }
                                number2 *= 10;
                                number2 += Convert.ToInt32(code[i] + "");
                            }
                        }
                        else
                        {
                            return (null, false);
                        }
                    }
                }
                if (number2 != 0) number += (number2 / Math.Round(Math.Pow(10, Convert.ToString(number2).Length))) * 10 - 1;
                number *= multiplier;
                string numAsStr = Convert.ToString(number);
                index = maxI + 1;
                if (numAsStr.Contains("."))
                    return (new ObjectHolder(number, "double"), true);
                else
                    return (new ObjectHolder((int)number, "int"), true);
            }
            catch { }
            return (null, false);
        }
    }
}