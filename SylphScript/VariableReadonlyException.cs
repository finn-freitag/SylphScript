using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SylphScript
{
    public class VariableReadonlyException : VariableException
    {
        public VariableReadonlyException(string message, string VariableName, int CharIndex = -1)
            : base(message, VariableName, CharIndex)
        {

        }
    }
}
