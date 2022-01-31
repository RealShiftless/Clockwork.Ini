using System;
using System.Collections.Generic;
using System.Text;

namespace Clockwork.Ini.Types
{
    class IntegerType : IValueType
    {
        public string Name => "Integer";


        public DynamicValue Parse(string str)
        {
            return int.Parse(str);
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
            return self.Value.ToString();
        }
    }
}
