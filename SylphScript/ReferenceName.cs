using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SylphScript
{
    public class ReferenceName // Just an own object for a possible later further processing.
    {
        public string FullName = "";

        public ReferenceName(string FullName)
        {
            this.FullName = FullName;
        }

        public override string ToString()
        {
            return FullName;
        }

        public override bool Equals(object obj)
        {
            if(obj == null) return false;
            if(!(obj is ReferenceName)) return false;
            return ((ReferenceName)obj).FullName == FullName;
        }

        public static bool operator ==(ReferenceName lhs, ReferenceName rhs)
        {
            if ((object)lhs == null && (object)rhs == null) return true;
            return lhs.Equals(rhs);
        }

        public static bool operator !=(ReferenceName lhs, ReferenceName rhs)
        {
            if ((object)lhs == null && (object)rhs == null) return false;
            if (((object)lhs == null && (object)rhs != null) || ((object)lhs != null && (object)rhs == null)) return true;
            return !lhs.Equals(rhs);
        }

        public static implicit operator ReferenceName(string FullName) {  return new ReferenceName(FullName); }
        public static implicit operator string(ReferenceName FullName) {  return FullName.FullName; }
    }
}
