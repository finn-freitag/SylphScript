using SylphScript.Functions;
using SylphScript.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SylphScript.AdditionalParsers
{
    public class VariableGetterParser : IAdditionalParser
    {
        public bool isTypeParser => true;

        public (IFunction Function, bool Success) Parse(ref int index, string code, VariableHolder vHolder)
        {
            string identifier = ParserHelper.GetIdentifier(ref index, code);
            ParserHelper.SkipSpace(ref index, code);
            if (identifier != "" && code[index] != '(')
            {
                if (code[index] == '.')
                {
                    List<(object, TokenType)> tokens = new List<(object, TokenType)>();
                    string currentIdentifierPath = "";
                    IType lastType = null;
                    VariableHolder lastVHolder = vHolder;
                    if (!vHolder.VariableExist(identifier))
                    {
                        index++;
                        ParserHelper.SkipSpace(ref index, code);
                        int backupIndex = index;
                        string subIdentifier = ParserHelper.GetIdentifier(ref index, code);
                        if (subIdentifier == "")
                            return (null, false);
                        IType type = TypeRegistry.FindType(identifier);
                        if (type == null)
                            return (null, false);
                        IFunctionRegistry staticFuncs = TypeHelper.GetStaticFunctions(type);
                        if (staticFuncs.FunctionList.Count == 0)
                            return (null, false);
                        ParserHelper.SkipSpace(ref index, code);
                        if (code[index] == '(')
                        {
                            index = backupIndex;
                            IFunction func = Parser.Parse(ref index, code, vHolder, false, false, staticFuncs);
                            tokens.Add((func, TokenType.Function));
                            if (func.AssignedReturnType != "null")
                            {
                                lastType = TypeRegistry.FindType(func.AssignedReturnType);
                                lastVHolder = TypeHelper.ConvertToVariableHolder(lastType);
                            }
                        }
                        else
                            return (null, false);
                    }
                    else
                    {
                        tokens.Add((identifier, TokenType.Variable));
                        currentIdentifierPath = identifier;
                        lastType = TypeRegistry.FindType(vHolder.GetVariable(identifier).TypeFullName);
                    }
                    while (index < code.Length && code[index] == '.')
                    {
                        index++;
                        ParserHelper.SkipSpace(ref index, code);
                        int backupIndex = index;
                        string subIdentifier = ParserHelper.GetIdentifier(ref index, code);
                        if (subIdentifier == "")
                            return (null, false);
                        ParserHelper.SkipSpace(ref index, code);
                        if (code[index] == '(')
                        {
                            index = backupIndex;
                            IFunction func = Parser.Parse(ref index, code, vHolder, false, false, lastType.SubFunctions);
                            tokens.Add((func, TokenType.Function));
                            if (func.AssignedReturnType == "null")
                                continue;
                            lastType = TypeRegistry.FindType(func.AssignedReturnType);
                            lastVHolder = TypeHelper.ConvertToVariableHolder(lastType);
                            currentIdentifierPath = "";
                        }
                        else
                        {
                            currentIdentifierPath += "." + subIdentifier;
                            currentIdentifierPath = currentIdentifierPath.Trim('.');
                            if (tokens[tokens.Count - 1].Item2 == TokenType.Variable)
                                tokens[tokens.Count - 1] = (currentIdentifierPath, tokens[tokens.Count - 1].Item2);
                            else
                                tokens.Add((subIdentifier, TokenType.Variable));
                            lastType = TypeRegistry.FindType(lastVHolder.GetVariable(currentIdentifierPath).TypeFullName);
                        }
                        ParserHelper.SkipSpace(ref index, code);
                    }
                    if (lastType == null)
                        return (new _getVariable(tokens, "null"), true);
                    else
                        return (new _getVariable(tokens, lastType.Name), true);
                }
                else
                {
                    if (!vHolder.VariableExist(identifier))
                        return (null, false);
                    return (new _getVariable(identifier, vHolder.GetVariable(identifier).TypeFullName), true);
                }
            }
            else
            {
                return (null, false);
            }
        }
    }
}
