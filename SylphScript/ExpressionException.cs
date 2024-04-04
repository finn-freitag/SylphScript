using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SylphScript
{
    public class ExpressionException : ParserException
    {
        public ExpressionException(string message, int CharIndex)
            : base(message, CharIndex)
        {
        }
    }
}
