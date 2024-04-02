using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SylphScript.Functions
{
    public class _implConvertFunction : IFunction
    {
        public IFunction NextFunction { get; set; }
        public ReferenceName AssignedReturnType { get { return conversion.Target; } set { } }
        public IFunction[] AssignedParameters { get; set; }

        public ReferenceName FullName => "_implConvertFunction";

        public ArgResPermutation Parameters
        {
            get
            {
                return ArgResPermutation.Build().Add(conversion.Target, conversion.Origin);
            }
        }

        public ReferenceName ReferenceObject { get; set; }

        private IConversion conversion = null;

        public _implConvertFunction(IConversion conversion)
        {
            this.conversion = conversion;
        }

        public IFunction GetNewInstance()
        {
            return new _implConvertFunction(conversion);
        }

        public ObjectHolder GetResult(VariableHolder variableHolder)
        {
            if (AssignedParameters.Length != 1) throw new InvalidOperationException("Invalid type cast!");
            return conversion.Convert(AssignedParameters[0].GetResult(variableHolder));
        }
    }
}
