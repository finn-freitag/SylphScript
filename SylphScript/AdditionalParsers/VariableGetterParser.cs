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

        /*public (IFunction Function, bool Success) Parse(ref int index, string code, VariableHolder vHolder)
        {
            string identifier = ParserHelper.GetIdentifier(ref index, code);
            ParserHelper.SkipSpace(ref index, code);
            if (identifier != "" && code[index] != '(')
            {
                if (!vHolder.VariableExist(identifier)) return (null, false);
                if (code[index] == '.')
                {
                    index++;
                    int indexBackup = index;
                    string subIdentifier = ParserHelper.GetIdentifier(ref index, code);
                    if (subIdentifier == "")
                        return (null, false);
                    ParserHelper.SkipSpace(ref index, code);
                    if (code[index] != '(')
                    {
                        // Sub variable
                        var subVars = TypeRegistry.FindType(vHolder.GetVariable(identifier).TypeFullName).Variables;
                        ReferenceName type = null;
                        for(int i = 0; i < subVars.Count; i++)
                            if (subVars[i].name == subIdentifier)
                                type = subVars[i].defaultValue.TypeFullName;
                        if(type != null)
                        {
                            return (new _getVariable(identifier + "." + subIdentifier, type), true);
                        }
                        else
                        {
                            return (null, false);
                        }
                    }
                    else
                    {
                        // Sub Function
                        index = indexBackup;
                        IFunction func = Parser.Parse(ref index, code, vHolder, false, false, TypeRegistry.FindType(vHolder.GetVariable(identifier).TypeFullName).SubFunctions);
                        return (func, true);
                    }
                }
                else
                {
                    return (new _getVariable(identifier, vHolder.GetVariable(identifier).TypeFullName), true);
                }
            }
            else
            {
                return (null, false);
            }
        }*/

        public (IFunction Function, bool Success) Parse(ref int index, string code, VariableHolder vHolder)
        {
            string identifier = ParserHelper.GetIdentifier(ref index, code);
            ParserHelper.SkipSpace(ref index, code);
            if (identifier != "" && code[index] != '(')
            {
                if (!vHolder.VariableExist(identifier)) return (null, false);
                if (code[index] == '.')
                {
                    List<(object, TokenType)> tokens = new List<(object, TokenType)>();
                    tokens.Add((identifier, TokenType.Variable));
                    string currentIdentifierPath = identifier;
                    IType lastType = TypeRegistry.FindType(vHolder.GetVariable(identifier).TypeFullName);
                    VariableHolder lastVHolder = vHolder;
                    while (code[index] == '.')
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
                            lastVHolder = TypeHelper.ConvertToVariableHolder(lastType, vHolder.ValueHolder);
                            currentIdentifierPath = "";
                        }
                        else
                        {
                            currentIdentifierPath += "." + subIdentifier;
                            currentIdentifierPath.Trim('.');
                            if (tokens[tokens.Count - 1].Item2 == TokenType.Variable)
                                tokens[tokens.Count - 1] = (currentIdentifierPath, tokens[tokens.Count - 1].Item2);
                            else
                                tokens.Add((subIdentifier, TokenType.Variable));
                            lastType = TypeRegistry.FindType(lastVHolder.GetVariable(currentIdentifierPath).TypeFullName);
                        }
                        ParserHelper.SkipSpace(ref index, code);
                    }
                    return (new _getVariable(tokens, lastType.Name), true);
                }
                else
                {
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
