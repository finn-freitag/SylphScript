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
            IFunction lastFunction = Parse(ref i, code);
            while (code.Length - i > 2)
            {
                IFunction newFunction = Parse(ref i, code);
                lastFunction.NextFunction = newFunction;
                lastFunction = newFunction;
            }
            return lastFunction;
        }

        private static IFunction Parse(ref int i, string code)
        {
            for (int p = 0; p < TypeParserRegistry.Parsers.Count; p++)
            {
                int backupI = i;
                var parserRes = TypeParserRegistry.Parsers[p].Parse(ref i, code);
                if (parserRes.Success)
                {
                    _constFunction constFunction = new _constFunction(parserRes.Object);
                    constFunction.AssignedReturnType = parserRes.Object.TypeFullName;
                    return constFunction;
                }
                else i = backupI;
            }

            for (; i < code.Length; i++) // Enumerate letters of code
            {
                SkipSpace(ref i, code);
                string currentFuncName = "";
                currentFuncName = ParserHelper.GetIdentifier(ref i, code); // get function name
                if (currentFuncName != "")
                {
                    SkipSpace(ref i, code);
                    List<IFunction> parameters = new List<IFunction>();
                    List<ReferenceName> paramTypes = new List<ReferenceName>();
                    bool first = true;
                    while (code[i] != ')') // Enumerate parameters
                    {
                        if ((first && code[i] != '(') || (!first && code[i] != ',')) throw new InvalidOperationException("Invalid syntax!");
                        first = false;
                        i++;
                        SkipSpace(ref i, code);
                        IFunction func = Parse(ref i, code);
                        parameters.Add(func);
                        paramTypes.Add(func.AssignedReturnType);
                        SkipSpace(ref i, code);
                    }
                    i++;
                    IFunction result = null;
                    for (int f = 0; f < FunctionsRegistry.Functions.Count; f++) // Enumerate functions, find matching function
                    {
                        if (FunctionsRegistry.Functions[f].FullName == currentFuncName)
                        {
                            ReferenceName returnType = FunctionsRegistry.Functions[f].Parameters.GetResultType(paramTypes.ToArray());
                            if (returnType != null)
                            {
                                result = FunctionsRegistry.Functions[f].GetNewInstance();
                                result.AssignedReturnType = returnType;
                                result.AssignedParameters = parameters.ToArray();
                                return result;
                            }
                        }
                    }
                    if (result == null)
                    {
                        for (int f = 0; f < FunctionsRegistry.Functions.Count; f++) // Enumerate functions, find function that matches using implicit conversions
                        {
                            if (FunctionsRegistry.Functions[f].FullName == currentFuncName)
                            {
                                for (int j = 0; j < FunctionsRegistry.Functions[f].Parameters.permutation.Count; j++)
                                {
                                    if (FunctionsRegistry.Functions[f].Parameters.permutation[j].Parameters.Count == paramTypes.Count)
                                    {
                                        bool foundConversion = true;
                                        List<IConversion> conversions = new List<IConversion>();
                                        for (int c = 0; c < paramTypes.Count; c++)
                                        {
                                            IConversion con = ConversionRegistry.GetImplicitConversion(paramTypes[c], FunctionsRegistry.Functions[f].Parameters.permutation[j].Parameters[c]);
                                            if (con == null)
                                            {
                                                foundConversion = false;
                                                break;
                                            }
                                            else conversions.Add(con);
                                        }
                                        if (foundConversion)
                                        {
                                            for(int c = 0; c < paramTypes.Count; c++)
                                            {
                                                _implConvertFunction implConFunc = new _implConvertFunction(conversions[c]);
                                                implConFunc.AssignedParameters = new IFunction[] { parameters[c] };
                                                parameters[c] = implConFunc;
                                            }
                                            result = FunctionsRegistry.Functions[f].GetNewInstance();
                                            result.AssignedReturnType = FunctionsRegistry.Functions[f].Parameters.permutation[j].Result;
                                            result.AssignedParameters = parameters.ToArray();
                                            return result;
                                        }
                                    }
                                }
                            }
                        }
                    }
                    throw new InvalidOperationException("Unknown function: \"" + currentFuncName + "\" (" + parameters.Count + " parameters)!");
                }
                else
                {
                    throw new InvalidOperationException("Invalid syntax!");
                }
            }
            throw new InvalidOperationException("Something went wrong!");
        }

        private static void SkipSpace(ref int i, string code) // Skip ' ', '\t', '\r', '\n', comments
        {
            while (i < code.Length)
            {
                int j = i;
                if (code[i] == ' ' || code[i] == '\t' || code[i] == '\r' || code[i] == '\n') i++;
                else if (!ParserHelper.SkipDoubleSlashComment(ref i, code)) ParserHelper.SkipSlashAsteriskComment(ref i, code);
                if (j == i) return;
            }
        }
    }
}
