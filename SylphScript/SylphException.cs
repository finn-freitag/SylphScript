using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SylphScript
{
    public class SylphException : Exception // Own class for possible later use.
    {
        public SylphException()
        {

        }

        public SylphException(string message) : base(message)
        {

        }
    }
}
