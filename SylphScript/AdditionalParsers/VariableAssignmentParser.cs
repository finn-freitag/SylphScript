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
        public (IFunction Function, bool Success) Parse(ref int index, string code, VariableHolder vHolder)
        {
            string type = ParserHelper.GetIdentifier(ref index, code);
            if (type != "")
            {
                for(int t = 0; t < TypeRegistry.Types.Count; t++)
                {
                    if(type == TypeRegistry.Types[t].Name)
                    {
                        ParserHelper.SkipSpace(ref index, code);
                        string name = ParserHelper.GetIdentifier(ref index, code);
                        if (name == "") return (null, false);
                        ParserHelper.SkipSpace(ref index, code);
                        if (code[index] != '=') return (null, false);
                        index++;
                        ParserHelper.SkipSpace(ref index, code);
                        IFunction value = Parser.Parse(ref index, code, vHolder);
                        vHolder.AddVariable(name, new ObjectHolder(null, type));
                        _Assignment assignment = new _Assignment(name, value);
                        assignment.AssignedReturnType = type;
                        return (assignment, true);
                    }
                }
                return (null, false);
            }
            else return (null, false);
        }
    }
}
