using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SylphScript
{
    public class UnknownFunctionException : ParserException
    {
        public UnknownFunctionException(string message, string FunctionName, int CharIndex)
            : base(message, CharIndex)
        {
            this.Data.Add("Function name", FunctionName);
        }
    }
}
