using SylphScript.Helper;
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
        public VariableHolder SubHolder = new VariableHolder();

        public static readonly ObjectHolder Null = new ObjectHolder(null, "null");

        public ObjectHolder(object @object, ReferenceName typeFullName)
        {
            Object = @object;
            TypeFullName = typeFullName;
            FixProperties();
        }

        public ObjectHolder(object @object, ReferenceName typeFullName, VariableHolder subHolder)
        {
            Object = @object;
            TypeFullName = typeFullName;
            SubHolder = subHolder;
            FixProperties();
        }

        public void FixProperties()
        {
            if (TypeFullName == "null")
                return;
            IType type = TypeRegistry.FindType(TypeFullName);
            if (Object == null)
            {
                SubHolder = new VariableHolder();
                for (int i = 0; i < type.Variables.Count; i++)
                {
                    var v = type.Variables[i];
                    SubHolder.AddVariable(v.name, v.defaultValue);
                }
            }
            else
            {
                SubHolder = type.ConvertToVHolder(Object);
            }
        }

        public ObjectHolder Clone()
        {
            return new ObjectHolder(Object, TypeFullName, SubHolder);
        }
    }
}
