using System;
using System.Collections.Generic;
using System.Text;

namespace Clockwork.Ini.Types
{
    class BoolType : IValueType
    {
        public string Name => "Boolean";

        public DynamicValue Parse(string str)
        {
            string val = str.ToLower();
            if(!(val == "false" || val == "true"))
                throw new Exception("Unable to parse string");

            return val == "true";
        }
        public bool TryParse(string str, out DynamicValue value)
        {
            try
            {
                value = Parse(str);
                return true;
            }
            catch
            {
                value = null;
                return false;
            }
        }

        public string ToString(DynamicValue self)
        {
            return self.Value.ToString();
        }
        public string SerializeToString(DynamicValue self)
        {
            return (bool)self.Value ? "true" : "false";
        }
    }
}
