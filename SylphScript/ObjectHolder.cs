using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SylphScript
{
    public class ObjectHolder
    {
        public object Object = null;
        public ReferenceName TypeFullName = "";

        public static readonly ObjectHolder Null = new ObjectHolder(null, "null");

        public ObjectHolder(object @object, ReferenceName typeFullName)
        {
            Object = @object;
            TypeFullName = typeFullName;
        }

        public ObjectHolder Clone()
        {
            return new ObjectHolder(Object, TypeFullName);
        }
    }
}
