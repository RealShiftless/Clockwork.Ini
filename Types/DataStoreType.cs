using System;
using System.Collections.Generic;
using System.Text;

namespace Clockwork.Ini.Types
{
    class DataStoreType : IValueType
    {
        /* NAME */
        public string Name => "DataStore";


        /* PARSING */
        public DynamicValue Parse(string str)
        {
            throw new Exception("DataStores arent parsed through text!");
        }
        public bool TryParse(string str, out DynamicValue self)
        {
            self = null;
            return false;
        }


        /* TO STRING */
        public string ToString(DynamicValue self)
        {
            Dictionary<string, DynamicValue> value = (Dictionary<string, DynamicValue>) self.Value;

            string str = "";

            foreach(KeyValuePair<string, DynamicValue> pair in value)
            {
                DataStore store = pair.Value as DataStore;
                if(store != null)
                {
                    str += store.ToString();
                }
                else
                {
                    str += pair.Value.FullName + " = " + pair.Value.ToString() + "\n";
                }
            }

            return str;
        }
        public string SerializeToString(DynamicValue self)
        {
            Dictionary<string, DynamicValue> value = (Dictionary<string, DynamicValue>)self.Value;

            string str = "";

            if(self.FullName != null)
                str += "[" + self.FullName + "]\n";

            foreach (KeyValuePair<string, DynamicValue> pair in value)
            {
                if(!(pair.Value is DataStore))
                {
                    str += pair.Value.Name + " = " + pair.Value.ValueType.SerializeToString(pair.Value) + "\n";
                }
            }

            return str + "\n";
        }
    }
}
