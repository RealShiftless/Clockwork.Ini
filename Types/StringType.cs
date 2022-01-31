﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace Clockwork.Ini.Types
{
    class StringType : IValueType
    {
        public string Name => "String";

        public DynamicValue Parse(string str)
        {
            if (!Regex.IsMatch(str, $"^\".*?\"$"))
                throw new Exception("Couldn't parse string");

            return str.Substring(1, str.Length - 2);
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
            return "\"" + self.Value + "\"";
        }
    }
}
