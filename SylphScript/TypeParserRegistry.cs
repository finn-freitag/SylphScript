using SylphScript.TypeParsers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SylphScript
{
    public static class TypeParserRegistry
    {
        public static List<ITypeParser> Parsers = new List<ITypeParser>()
        {
            new NullParser(),
            new NumberParser(),
            new CharParser(),
            new StringParser(),
        };
    }
}
