using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SylphScript
{
    public class ArgResPermutation
    {
        public List<(ReferenceName Result, List<ReferenceName> Parameters)> permutation = new List<(ReferenceName, List<ReferenceName>)>();

        public ArgResPermutation() { }

        public ArgResPermutation Add(ReferenceName Result, params ReferenceName[] Parameters)
        {
            permutation.Add((Result, Parameters.ToList()));
            return this;
        }

        public bool ContainsParameterList(params ReferenceName[] Parameters)
        {
            return GetResultType(Parameters) != null;
        }

        public ReferenceName GetResultType(params ReferenceName[] Parameters)
        {
            for(int i = 0; i < permutation.Count; i++)
            {
                if (permutation[i].Parameters.Count == Parameters.Length)
                {
                    bool match = true;
                    for(int p = 0; p < Parameters.Length; p++)
                    {
                        if (Parameters[p] != permutation[i].Parameters[p]) match = false;
                    }
                    if (match) return permutation[i].Result;
                }
            }
            return null;
        }

        public static ArgResPermutation Build()
        {
            return new ArgResPermutation();
        }
    }
}
