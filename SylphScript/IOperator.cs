using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SylphScript
{
    public interface IOperator
    {
        string OperatorName { get; }
        ReferenceName Type1 { get; }
        ReferenceName Type2 { get; }
        ReferenceName Result { get; }
        ObjectHolder Process(ObjectHolder obj1, ObjectHolder obj2);
    }
}
