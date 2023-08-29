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
            string secondID = ParserHelper.GetIdentifier(ref index, code);
            ParserHelper.SkipSpace(ref index, code);
            if (code[index] != '=') return (null, false);
            index++;
            ParserHelper.SkipSpace(ref index, code);
            IFunction value = Parser.Parse(ref index, code, vHolder);
            if(secondID != "")
            {
                if (!TypeRegistry.ContainsType(firstID)) return (null, false);
                if (value.AssignedReturnType != firstID)
                {
                    IConversion conversion = ConversionRegistry.GetConversion(value.AssignedReturnType, firstID);
                    if (conversion == null) return (null, false);
                    _implConvertFunction convert = new _implConvertFunction(conversion);
                    convert.AssignedParameters = new IFunction[] { value };
                    vHolder.AddVariable(secondID, new ObjectHolder(null, firstID));
                    return (new _Assignment(secondID, convert), true);
                }
                vHolder.AddVariable(secondID, new ObjectHolder(null, firstID));
                return (new _Assignment(secondID, value), true);
            }
            else
            {
                if (!vHolder.VariableExist(firstID)) return (null, false);
                if (vHolder.GetVariable(firstID).TypeFullName != value.AssignedReturnType)
                {
                    IConversion conversion = ConversionRegistry.GetConversion(value.AssignedReturnType, vHolder.GetVariable(firstID).TypeFullName);
                    if (conversion == null) return (null, false);
                    _implConvertFunction convert = new _implConvertFunction(conversion);
                    convert.AssignedParameters = new IFunction[] { value };
                    return (new _Reassignment(firstID, convert), true);
                }
                return (new _Reassignment(firstID, value), true);
            }
            //string type = ParserHelper.GetIdentifier(ref index, code);
            //if (type == "") return (null, false);
            //for (int t = 0; t < TypeRegistry.Types.Count; t++)
            //{
            //    if (type == TypeRegistry.Types[t].Name)
            //    {
            //        ParserHelper.SkipSpace(ref index, code);
            //        string name = ParserHelper.GetIdentifier(ref index, code);
            //        if (name == "") return (null, false);
            //        if (vHolder.VariableExist(name)) return (null, false);
            //        ParserHelper.SkipSpace(ref index, code);
            //        if (code[index] != '=') return (null, false);
            //        index++;
            //        ParserHelper.SkipSpace(ref index, code);
            //        IFunction value = Parser.Parse(ref index, code, vHolder);
            //        vHolder.AddVariable(name, new ObjectHolder(null, type));
            //        _Assignment assignment = new _Assignment(name, value);
            //        assignment.AssignedReturnType = type;
            //        return (assignment, true);
            //    }
            //}
            return (null, false);
        }
    }
}
