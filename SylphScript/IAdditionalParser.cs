using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SylphScript
{
    public interface IAdditionalParser
    {
        bool isTypeParser { get; }
        (IFunction Function, bool Success) Parse(ref int index, string code, VariableHolder vHolder);
    }
}
