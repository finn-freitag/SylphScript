﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SylphScript.Types
{
    public class @byte : IType
    {
        public string Name => "byte";

        public List<IFunction> SubFunctions => new List<IFunction>();

        public List<(string name, ObjectHolder defaultValue)> Variables => new List<(string name, ObjectHolder defaultValue)>();

        public object AppendPropsFromVHolder(object originalTypeEquivalent, VariableHolder vHolder)
        {
            return originalTypeEquivalent;
        }

        public VariableHolder ConvertToVHolder(object typeEquivalent)
        {
            return null;
        }

        public object Clone(object typeEquivalent)
        {
            return typeEquivalent;
        }
    }
}
