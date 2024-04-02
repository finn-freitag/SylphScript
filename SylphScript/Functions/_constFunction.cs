using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SylphScript.Functions
{
    public class _constFunction : IFunction
    {
        public IFunction NextFunction { get; set; }
        public ReferenceName AssignedReturnType { get { return holder.TypeFullName; } set { } }
        public IFunction[] AssignedParameters { get; set; }

        public ReferenceName FullName => "_constFunction";

        public ArgResPermutation Parameters { get; private set; }

        public ReferenceName ReferenceObject { get; set; }

        private ObjectHolder holder = null;

        public _constFunction(ObjectHolder holder)
        {
            this.holder = holder;
            Parameters = ArgResPermutation.Build().Add(holder.TypeFullName);
        }

        public ObjectHolder GetResult(VariableHolder variableHolder)
        {
            return holder;
        }

        public IFunction GetNewInstance()
        {
            return new _constFunction(holder);
        }
    }
}
