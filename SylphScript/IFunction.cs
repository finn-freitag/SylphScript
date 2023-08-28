using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SylphScript
{
    public interface IFunction
    {
        IFunction NextFunction { get; set; }
        ReferenceName AssignedReturnType { get; set; }
        IFunction[] AssignedParameters { get; set; }
        ReferenceName FullName { get; }
        ArgResPermutation Parameters { get; }
        ObjectHolder GetResult(VariableHolder variableHolder);
        IFunction GetNewInstance();
    }
}
