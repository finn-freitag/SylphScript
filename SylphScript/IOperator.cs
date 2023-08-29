using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SylphScript
{
    public interface IOperator
    {
        string OperatorName { get; } // Operatorname cannot contain brackets
        bool IsOneParameterOperator { get; } // for example bool inversion ! Just Type1 is required
        OperatorGrade Grade { get; } // To specify parser position
        ReferenceName Type1 { get; }
        ReferenceName Type2 { get; }
        ReferenceName Result { get; }
        ObjectHolder Process(IFunction obj1, IFunction obj2, VariableHolder vHolder); // takes functions and not an ObjectHolder because the & or the | operator can sometimes make its decision without both values. It's easier for the user to use this feature sometimes.
    }
}
