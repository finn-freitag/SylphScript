using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SylphScript
{
    public class TypeMismatchException : SylphException
    {
        public TypeMismatchException(string requiredType, string providedType)
            : base("Type mismatch: \"" + providedType + "\" provided, but \"" + requiredType + "\" required!")
        {
            Data.Add("Required type", requiredType);
            Data.Add("Provided type", providedType);
        }
    }
}
