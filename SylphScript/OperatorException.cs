using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SylphScript
{
    public class OperatorException : SylphException
    {
        public OperatorException(string operatorname)
            : base("Operator \"" + operatorname + "\" doesn't fulfill the requirements!")
        {
            
        }
    }
}
