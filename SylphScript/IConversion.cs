using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SylphScript
{
    public interface IConversion
    {
        bool Implicit { get; }
        ReferenceName Origin { get; }
        ReferenceName Target { get; }
        ObjectHolder Convert(ObjectHolder value);
    }
}
