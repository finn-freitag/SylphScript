using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SylphScript
{
    public class ValueHolder
    {
        private Dictionary<int, ObjectHolder> Data = new Dictionary<int, ObjectHolder>();
        private int autoID = 0;

        public ValueHolder() { }

        public int AddVariable(ObjectHolder value)
        {
            int id = autoID++;
            if (Data.ContainsKey(id)) throw new InvalidOperationException("This value does already exists!");
            Data.Add(id, value);
            return id;
        }

        public void SetVariable(int id, ObjectHolder value)
        {
            if (!Data.ContainsKey(id))
                throw new InvalidOperationException("Value does not exist!");
            Data[id] = value;
        }

        public ObjectHolder GetValue(int id)
        {
            return Data[id];
        }

        public bool ValueExist(int id)
        {
            return Data.ContainsKey(id);
        }
    }
}
