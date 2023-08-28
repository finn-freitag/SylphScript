using SylphScript.Functions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SylphScript.AdditionalParsers
{
    public class StringParser : IAdditionalParser
    {
        public (IFunction Function, bool Success) Parse(ref int index, string code, VariableHolder vHolder)
        {
            try
            {
                bool escaped = false;
                StringBuilder sb = new StringBuilder();
                int originalIndex = index;
                if (code[index] != '"') return (null, false);
                index++;
                while (code[index] != '"' || escaped)
                {
                    if (escaped)
                    {
                        if (code[index] == '\\') sb.Append("\\");
                        if (code[index] == '0') sb.Append("\0");
                        if (code[index] == 't') sb.Append("\t");
                        if (code[index] == 'r') sb.Append("\r");
                        if (code[index] == 'n') sb.Append("\n");
                        if (code[index] == '"') sb.Append("\"");
                    }
                    else
                    {
                        if (code[index] == '\\') escaped = true;
                        else sb.Append(code[index]);
                    }
                    index++;
                }
                index++;
                return (new _constFunction(new ObjectHolder(sb.ToString(), "string")), true);
            }
            catch { }
            return (null, false);
        }
    }
}
