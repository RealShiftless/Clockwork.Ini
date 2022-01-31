using System;
using System.Collections.Generic;
using System.Text;

namespace Clockwork.Ini
{
    public interface IValueType
    {
        public string Name { get; }

        public DynamicValue Parse(string str);
        public bool TryParse(string str, out DynamicValue self);

        public string ToString(DynamicValue self);
        public string SerializeToString(DynamicValue self);
    }
}
