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
            new NullParser(),
            new NumberParser(),
            new CharParser(),
            new StringParser(),
        };
    }
}
