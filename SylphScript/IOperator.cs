using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SylphScript
{
    public interface IOperator
    {
        /// <summary>
        /// Operatorname cannot contain brackets must cannot start with a letter or digit!
        /// </summary>
        string OperatorName { get; }
        /// <summary>
        /// for example bool inversion ! Just Type1 is required
        /// </summary>
        bool IsOneParameterOperator { get; }
        /// <summary>
        /// To specify parser position
        /// </summary>
        OperatorGrade Grade { get; }
        ReferenceName Type1 { get; }
        ReferenceName Type2 { get; }
        ReferenceName Result { get; }
        /// <summary>
        /// takes functions and not an ObjectHolder because the & or the | operator can sometimes make its decision without both values. It's easier for the user to use this feature sometimes.
        /// </summary>
        ObjectHolder Process(IFunction obj1, IFunction obj2, VariableHolder vHolder);
    }
}
