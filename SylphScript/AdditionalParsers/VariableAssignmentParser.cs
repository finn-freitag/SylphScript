using SylphScript.Functions;
using SylphScript.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SylphScript.AdditionalParsers
{
    public class VariableAssignmentParser : IAdditionalParser
    {
        public bool isTypeParser => false;

        public (IFunction Function, bool Success) Parse(ref int index, string code, VariableHolder vHolder)
        {
            string firstID = ParserHelper.GetIdentifier(ref index, code);
            if (firstID == "") return (null, false);
            ParserHelper.SkipSpace(ref index, code);
            List<(object, TokenType)> tokens = new List<(object, TokenType)>();
            IType lastType = null;
            VariableHolder lastVHolder = null;
            string currentIdentifierPath = "";
            if (code[index] == '.')
            {
                tokens.Add((firstID, TokenType.Variable));
                currentIdentifierPath = firstID;
                lastType = TypeRegistry.FindType(vHolder.GetVariable(firstID).TypeFullName);
                lastVHolder = vHolder;
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
                            return (null, false);
                        lastType = TypeRegistry.FindType(func.AssignedReturnType);
                        lastVHolder = TypeHelper.ConvertToVariableHolder(lastType);
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
            }
            string secondID = ParserHelper.GetIdentifier(ref index, code);
            ParserHelper.SkipSpace(ref index, code);
            if (code[index] != '=') return (null, false);
            index++;
            ParserHelper.SkipSpace(ref index, code);
            bool asReference = false;
            bool keepRefs = false;
            for(int i = 0; i < 2; i++)
            {
                if (code[index] == '*')
                {
                    index++;
                    ParserHelper.SkipSpace(ref index, code);
                    asReference = true;
                }
                if (code[index] == '+')
                {
                    index++;
                    ParserHelper.SkipSpace(ref index, code);
                    keepRefs = true;
                }
            }
            IFunction value = Parser.Parse(ref index, code, vHolder);
            if(secondID != "")
            {
                if (!TypeRegistry.ContainsType(firstID) && firstID != "var") return (null, false);
                if (value.AssignedReturnType != firstID && firstID != "var")
                {
                    IConversion conversion = ConversionRegistry.GetImplicitConversion(value.AssignedReturnType, firstID);
                    if (conversion == null) return (null, false);
                    _implConvertFunction convert = new _implConvertFunction(conversion);
                    convert.AssignedParameters = new IFunction[] { value };
                    vHolder.AddVariable(secondID, new ObjectHolder(null, firstID));
                    return (new _Assignment(secondID, convert, asReference), true);
                }
                vHolder.AddVariable(secondID, new ObjectHolder(null, value.AssignedReturnType));
                return (new _Assignment(secondID, value, asReference), true);
            }
            else
            {
                if(tokens.Count > 0)
                {
                    if (!lastVHolder.VariableExist(currentIdentifierPath)) return (null, false);
                    if (lastVHolder.GetVariable(currentIdentifierPath).TypeFullName != value.AssignedReturnType)
                    {
                        IConversion conversion = ConversionRegistry.GetImplicitConversion(value.AssignedReturnType, lastVHolder.GetVariable(currentIdentifierPath).TypeFullName);
                        if (conversion == null) return (null, false);
                        _implConvertFunction convert = new _implConvertFunction(conversion);
                        convert.AssignedParameters = new IFunction[] { value };
                        return (new _Reassignment(tokens, convert, asReference, keepRefs), true);
                    }
                    return (new _Reassignment(tokens, value, asReference, keepRefs), true);
                }
                else
                {
                    if (!vHolder.VariableExist(firstID)) return (null, false);
                    if (vHolder.GetVariable(firstID).TypeFullName != value.AssignedReturnType)
                    {
                        IConversion conversion = ConversionRegistry.GetImplicitConversion(value.AssignedReturnType, vHolder.GetVariable(firstID).TypeFullName);
                        if (conversion == null) return (null, false);
                        _implConvertFunction convert = new _implConvertFunction(conversion);
                        convert.AssignedParameters = new IFunction[] { value };
                        return (new _Reassignment(firstID, convert, asReference, keepRefs), true);
                    }
                    return (new _Reassignment(firstID, value, asReference, keepRefs), true);
                }
            }
        }
    }
}
