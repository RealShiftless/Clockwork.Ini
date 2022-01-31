using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace Clockwork.Ini.Types
{
    class FloatType : IValueType
    {
        public string Name => "Float";

        public DynamicValue Parse(string str)
        {
            string val = str.Replace('.', ',');

            if(!Regex.IsMatch(str, @"[0-9]+\,[0-0]+"))
                throw new Exception("String wasn't parsed");

            return float.Parse(str);
        }
        public bool TryParse(string str, out DynamicValue value)
        {
            try
            {
                string val = str.Replace('.', ',');
                value = Parse(val);
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
            string val = self.Value.ToString();

            if(!val.Contains(','))
                val += ",0";

            return val;
        }
    }
}
