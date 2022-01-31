using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace Clockwork.Ini
{
    internal static class TypeManager
    {
        private static Dictionary<Type, IValueType> _types = new Dictionary<Type, IValueType>();

        static TypeManager()
        {
            Type type = typeof(IValueType);
            IEnumerable<Type> types = AppDomain.CurrentDomain.GetAssemblies()
                                      .SelectMany(s => s.GetTypes())
                                      .Where(p => type.IsAssignableFrom(p));

            foreach(Type t in types)
            {
                if(t.IsClass)
                {
                    IValueType valueType = (IValueType)Activator.CreateInstance(t);

                    _types.Add(t, valueType);
                }
            }
        }

        internal static IValueType GetValueType<T>() where T : IValueType, new()
        {
            return _types[typeof(T)];
        }

        internal static DynamicValue ParseString(string str)
        {
            DynamicValue value = null;
            foreach(KeyValuePair<Type, IValueType> pair in _types)
            {
                if(pair.Value.TryParse(str, out value))
                {
                    break;
                }
            }

            if(value == null)
                throw new InvalidCastException("Value " + str + " was not able to be parsed into any value!");

            Debug.WriteLine("parsed " + value.ValueType.Name);

            return value;
        }
    }
}
