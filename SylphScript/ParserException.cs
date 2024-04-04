using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SylphScript
{
    public class ParserException : SylphException
    {
        public ParserException(string message, int CharIndex) : base(message)
        {
            this.Data.Add("Char index", CharIndex);
        }
    }
}
