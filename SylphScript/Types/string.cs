﻿using SylphScript.Types.stringFunctions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SylphScript.Types
{
    public class @string : IType
    {
        public string Name => "string";

        public List<IFunction> SubFunctions => new List<IFunction>()
        {
            new CharAt(),
            new Length(),
        };

        public List<(string name, ObjectHolder defaultValue)> Variables => new List<(string name, ObjectHolder defaultValue)>();

        public VariableHolder ConvertToVHolder(object typeEquivalent, ValueHolder valueHolder)
        {
            return null;
        }
    }
}
