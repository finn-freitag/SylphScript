using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SylphScript
{
    public enum OperatorGrade : byte
    {
        /// <summary>
        /// For example a multiplication, division or a logic operation.
        /// </summary>
        First = 1,
        /// <summary>
        /// For example a addition, subtraction or string operations.
        /// </summary>
        Second = 2,
        /// <summary>
        /// For example a boolean comparison.
        /// </summary>
        Third = 3
    }
}
