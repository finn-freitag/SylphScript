using SylphScript.AdditionalParsers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SylphScript
{
    public static class AdditionalParserRegistry
    {
        public static List<IAdditionalParser> Parsers = new List<IAdditionalParser>()
        {
            new OperatorParser(), // needs to be first
            new BoolParser(),
            new CharParser(),
            new CRLFParser(),
            new ForLoop3ParamsParser(),
            new IfParser(),
            new NullParser(),
            new NumberParser(),
            new StringParser(),
            new VariableAssignmentParser(),
            new VariableGetterParser(),
        };
    }
}
