using SylphScript.Conversions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SylphScript
{
    public static class ConversionRegistry
    {
        public static List<IConversion> Conversions = new List<IConversion>()
        {
            new ByteToChar(),
            new ByteToInteger(),
            new CharToByte(),
            new IntegerToByte(),
        };

        public static bool ConversionExists(ReferenceName origin, ReferenceName target)
        {
            return GetConversion(origin, target) != null;
        }

        public static IConversion GetConversion(ReferenceName origin, ReferenceName target)
        {
            for(int i = 0; i < Conversions.Count; i++)
            {
                if (Conversions[i].Origin == origin && Conversions[i].Target == target) return Conversions[i];
            }
            return null;
        }

        public static IConversion GetImplicitConversion(ReferenceName origin, ReferenceName target)
        {
            for(int i = 0; i < Conversions.Count; i++)
            {
                if (Conversions[i].Origin == origin && Conversions[i].Target == target && Conversions[i].Implicit) return Conversions[i];
            }
            return null;
        }
    }
}
