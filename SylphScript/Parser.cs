using SylphScript.Functions;
using SylphScript.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SylphScript
{
    public static class Parser
    {
        public static IFunction Parse(string code)
        {
            int i = 0;
            return ParseMultiple(ref i, code, new VariableHolder());
        }
        
        public static IFunction Parse(string code, VariableHolder vHolder)
        {
            int i = 0;
            return ParseMultiple(ref i, code, vHolder);
        }

        public static IFunction ParseMultiple(ref int i, string code, VariableHolder vHolder)
        {
            IFunction lastFunction = Parse(ref i, code, vHolder);
            IFunction first = lastFunction;
            ParserHelper.SkipSpace(ref i, code);
            while (code.Length - i > 2 && code[i] != '}')
            {
                IFunction newFunction = Parse(ref i, code, vHolder);
                //if (newFunction is _getVariable) throw new InvalidOperationException("Variable is not a command!");
                lastFunction.NextFunction = newFunction;
                lastFunction = newFunction;
                ParserHelper.SkipSpace(ref i, code);
            }
            return first;
        }

        public static IFunction Parse(ref int i, string code, VariableHolder vHolder, bool useTypeParsers = true, bool useAdditionalParsers = true, List<IFunction> useFunctionSet = null)
        {
            if (useAdditionalParsers || useTypeParsers)
            {
                ParserHelper.SkipSpace(ref i, code);

                for (int p = 0; p < AdditionalParserRegistry.Parsers.Count; p++)
                {
                    if(useAdditionalParsers || (useTypeParsers && AdditionalParserRegistry.Parsers[p].isTypeParser))
                    {
                        int backupI = i;
                        var parserRes = AdditionalParserRegistry.Parsers[p].Parse(ref i, code, vHolder);
                        if (parserRes.Success) return parserRes.Function;
                        else i = backupI;
                    }
                }
            }

            ParserHelper.SkipSpace(ref i, code);
            int functionStart = i;
            string currentFuncName = ParserHelper.GetIdentifier(ref i, code); // get function name
            if (currentFuncName != "")
            {
                ParserHelper.SkipSpace(ref i, code);
                List<IFunction> parameters = new List<IFunction>();
                List<ReferenceName> paramTypes = new List<ReferenceName>();
                bool first = true;
                while (code[i] != ')') // Enumerate parameters
                {
                    if ((first && code[i] != '(') || (!first && code[i] != ',')) throw new ParserException("Invalid function parameter syntax!", i);
                    first = false;
                    i++;
                    if (code[i] == ')') break;
                    ParserHelper.SkipSpace(ref i, code);
                    IFunction func = Parse(ref i, code, vHolder);
                    parameters.Add(func);
                    paramTypes.Add(func.AssignedReturnType);
                    ParserHelper.SkipSpace(ref i, code);
                }
                i++;
                IFunction result = null;
                if (useFunctionSet == null) useFunctionSet = FunctionsRegistry.Functions;
                for (int f = 0; f < useFunctionSet.Count; f++) // Enumerate functions, find matching function
                {
                    if (useFunctionSet[f].FullName == currentFuncName)
                    {
                        ReferenceName returnType = useFunctionSet[f].Parameters.GetResultType(paramTypes.ToArray());
                        if (returnType != null)
                        {
                            result = useFunctionSet[f].GetNewInstance();
                            result.AssignedReturnType = returnType;
                            result.AssignedParameters = parameters.ToArray();
                            return result;
                        }
                    }
                }
                if (result == null)
                {
                    for (int f = 0; f < useFunctionSet.Count; f++) // Enumerate functions, find function that matches using implicit conversions
                    {
                        if (useFunctionSet[f].FullName == currentFuncName)
                        {
                            for (int j = 0; j < useFunctionSet[f].Parameters.permutation.Count; j++)
                            {
                                if (useFunctionSet[f].Parameters.permutation[j].Parameters.Count == paramTypes.Count)
                                {
                                    bool foundConversion = true;
                                    List<IConversion> conversions = new List<IConversion>();
                                    for (int c = 0; c < paramTypes.Count; c++)
                                    {
                                        IConversion con = ConversionRegistry.GetImplicitConversion(paramTypes[c], useFunctionSet[f].Parameters.permutation[j].Parameters[c]);
                                        if (con == null)
                                        {
                                            foundConversion = false;
                                            break;
                                        }
                                        else conversions.Add(con);
                                    }
                                    if (foundConversion)
                                    {
                                        for (int c = 0; c < paramTypes.Count; c++)
                                        {
                                            _implConvertFunction implConFunc = new _implConvertFunction(conversions[c]);
                                            implConFunc.AssignedParameters = new IFunction[] { parameters[c] };
                                            parameters[c] = implConFunc;
                                        }
                                        result = useFunctionSet[f].GetNewInstance();
                                        result.AssignedReturnType = useFunctionSet[f].Parameters.permutation[j].Result;
                                        result.AssignedParameters = parameters.ToArray();
                                        return result;
                                    }
                                }
                            }
                        }
                    }
                }
                throw new UnknownFunctionException("Unknown function: \"" + currentFuncName + "\" (" + parameters.Count + " parameters)!", currentFuncName, functionStart);
            }
            else
            {
                throw new ExpressionException("Unknown expression!", functionStart);
            }
        }
    }
}
