using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SylphScript
{
    public class VariableException : SylphException
    {
        public VariableException(string message, string VariableName, int CharIndex = -1) : base(message)
        {
            this.Data.Add("Variable name", VariableName);
            if (CharIndex > -1)
                this.Data.Add("Char Index", CharIndex);
        }
    }
}
