﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SylphScript
{
    public interface ITypeParser
    {
        (ObjectHolder Object, bool Success) Parse(ref int index, string code);
    }
}