using SylphScript.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SylphScript
{
    public static class TypeRegistry
    {
        public static List<IType> Types = new List<IType>()
        {
            new @bool(),
            new @byte(),
            new @char(),
            new @double(),
            new @int(),
            new @string(),
        };

        public static bool ContainsType(ReferenceName type)
        {
            for(int i = 0; i < Types.Count; i++)
            {
                if (Types[i].Name == type) return true;
            }
            return false;
        }
    }
}
