using SylphScript.Functions;
using SylphScript.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SylphScript.AdditionalParsers
{
    public class OperatorParser : IAdditionalParser
    {
        public bool isTypeParser => false;

        public (IFunction Function, bool Success) Parse(ref int index, string code, VariableHolder vHolder)
        {
            List<object> tokens = new List<object>(); // Contains IFunction / IOperator[]
            bool containsOP = false;
            while (true)
            {
                ParserHelper.SkipSpace(ref index, code);
                int backupI = index;
                var res = ParseNextToken(ref index, code, vHolder);
                if (!res.Success
                    || (tokens.Count > 0
                    && (res.Token is IFunction && tokens[tokens.Count - 1] is IFunction))
                    || (tokens.Count > 0
                    && (res.Token is IOperator[] && tokens[tokens.Count - 1] is IOperator[] && !(((IOperator[])res.Token).Where(op => op.IsOneParameterOperator).ToArray().Length > 0))))
                {
                    index = backupI;
                    break;
                }
                tokens.Add(res.Token);
                if (res.Token is IOperator[]) containsOP = true;
            }
            if (!containsOP) return (null, false);

            if (tokens.Count < 2) return (null, false);

            // find matching operators and make type casts
            for(int i = 0; i < tokens.Count; i++)
            {
                if (tokens[i] is IOperator[])
                {
                    IOperator[] op = (IOperator[])tokens[i];
                    for(int r = 0; r < 2; r++)
                    {
                        for (int o = 0; o < op.Length; o++)
                        {
                            if (op[o].IsOneParameterOperator)
                            {
                                if (i + 1 < tokens.Count && tokens[i + 1] is IFunction)
                                {
                                    IFunction func = (IFunction)tokens[i + 1];
                                    if (op[o].Type1 == func.AssignedReturnType)
                                    {
                                        tokens[i] = op[o];
                                    }
                                    else if (r == 1)
                                    {
                                        IConversion con = ConversionRegistry.GetImplicitConversion(func.AssignedReturnType, op[o].Type1);
                                        if(con != null)
                                        {
                                            _implConvertFunction conFunc = new _implConvertFunction(con);
                                            conFunc.AssignedParameters = new IFunction[] { func };
                                            tokens[i + 1] = conFunc;
                                        }
                                    }
                                }
                                else return (null, false);
                            }
                            else
                            {
                                if (i + 1 < tokens.Count && tokens[i + 1] is IFunction && i - 1 >= 0 && tokens[i - 1] is IFunction)
                                {
                                    IFunction func1 = (IFunction)tokens[i - 1];
                                    IFunction func2 = (IFunction)tokens[i + 1];
                                    if (op[o].Type1 == func1.AssignedReturnType && op[o].Type2 == func2.AssignedReturnType)
                                    {
                                        tokens[i] = op[o];
                                    }
                                    else if (r == 1)
                                    {
                                        IConversion con1 = ConversionRegistry.GetImplicitConversion(func1.AssignedReturnType, op[o].Type1);
                                        IConversion con2 = ConversionRegistry.GetImplicitConversion(func2.AssignedReturnType, op[o].Type2);
                                        if(con1 != null && con2 != null)
                                        {
                                            _implConvertFunction conFunc1 = new _implConvertFunction(con1);
                                            conFunc1.AssignedParameters = new IFunction[] { func1 };
                                            tokens[i - 1] = conFunc1;
                                            _implConvertFunction conFunc2 = new _implConvertFunction(con2);
                                            conFunc2.AssignedParameters = new IFunction[] { func2 };
                                            tokens[i + 1] = conFunc2;
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }

            for(int i = 0; i < tokens.Count; i++)
            {
                if (tokens[i] is IOperator[]) return (null, false);
            }

            // 'tokens' only contains IFunction / IOperator

            // parse tokens

            for (int g = 1; g < 4; g++) // Enumerate operator grades
            {
                for (int i = 0; i < tokens.Count; i++)
                {
                    if (tokens[i] is IOperator)
                    {
                        IOperator op = (IOperator)tokens[i];
                        if (op.Grade == (OperatorGrade)(byte)g)
                        {
                            if (op.IsOneParameterOperator)
                            {
                                IFunction func = (IFunction)tokens[i + 1];
                                _operator o = new _operator(op);
                                o.AssignedParameters = new IFunction[] { func };
                                tokens.RemoveAt(i);
                                tokens.RemoveAt(i);
                                tokens.Insert(i, o);
                            }
                            else
                            {
                                IFunction func1 = (IFunction)tokens[i - 1];
                                IFunction func2 = (IFunction)tokens[i + 1];
                                _operator o = new _operator(op);
                                o.AssignedParameters = new IFunction[] { func1, func2 };
                                i--;
                                tokens.RemoveAt(i);
                                tokens.RemoveAt(i);
                                tokens.RemoveAt(i);
                                tokens.Insert(i, o);
                            }
                        }
                    }
                }
            }

            if (tokens.Count > 1) return (null, false);
            if (tokens[0] is IOperator) return (null, false);
            return ((IFunction)tokens[0], true);
        }

        private (object Token, bool Success) ParseNextToken(ref int index, string code, VariableHolder vHolder)
        {
            if (index >= code.Length) return (null, false);
            
            if (code[index] == '(')
            {
                index++;
                IFunction value = Parser.Parse(ref index, code, vHolder);
                if (code[index] != ')') return (null, false);
                index++;
                return (value, true);
            }

            int backupIndex = index;
            string id = ParserHelper.GetIdentifier(ref index, code);
            if(id != "")
            {
                if (code[index] == '(')
                {
                    index = backupIndex;
                    try
                    {
                        IFunction func = Parser.Parse(ref index, code, vHolder, false, false);
                        return (func, true);
                    }
                    catch
                    {
                        return (null, false);
                    }
                }
                else
                {
                    VariableGetterParser getter = new VariableGetterParser();
                    var res = getter.Parse(ref index, code, vHolder);
                    if (res.Success) return (res.Function, true);
                }
            }
            try
            {
                IFunction f = Parser.Parse(ref index, code, vHolder, true, false);
                return (f, true);
            }
            catch { }
            while (true)
            {
                if (ParserHelper.SkipDoubleSlashComment(ref index, code) || ParserHelper.SkipSlashAsteriskComment(ref index, code) || code[index] == ' ' || code[index] == ')')
                {
                    IOperator[] op = OperatorRegistry.GetOperator(id);
                    if (op.Length == 0) return (null, false);
                    return (op, true);
                }
                id += code[index];
                index++;
            }
        }
    }
}
