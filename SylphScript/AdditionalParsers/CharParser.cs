using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SylphScript.AdditionalParsers
{
    public class CharParser : IAdditionalParser
    {
        public (ObjectHolder Object, bool Success) Parse(ref int index, string code)
        {
            if (code[index] != '\'') return (null, false);
            int originalIndex = index;
            try
            {
                index++;
                bool special = code[index] == '\\';
                if (code[index + 1 + (special ? 1 : 0)] != '\'')
                {
                    index = originalIndex;
                    return (null, false);
                }
                if (!special)
                {
                    index += 2;
                    return (new ObjectHolder(code[index - 2], "char"), true);
                }
                else
                {
                    index++;
                    char c = '\0';
                    if (code[index] == '\\') c = '\\';
                    if (code[index] == 't') c = '\t';
                    if (code[index] == 'r') c = '\r';
                    if (code[index] == 'n') c = '\n';
                    if (code[index] == 'b') c = '\b';
                    if (code[index] == '\'') c = '\'';
                    index += 2;
                    return (new ObjectHolder(c, "char"), true);
                }
            }
            catch
            {
                index = originalIndex;
                return (null, false);
            }
        }
    }
}
