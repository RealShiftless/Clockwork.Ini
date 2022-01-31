using System;
using System.Collections.Generic;
using System.Text;

namespace Clockwork.Ini
{
    public class DynamicValue
    {
        /* INTERNAL STRUCTURE */
        internal DataStore Parent;
        internal string Name;

        internal string FullName
        {
            get
            {
                return GetFullName();
            }
        }


        /* VALUE VARIABLES */
        public IValueType ValueType { get; internal set; }
        public object Value { get; internal set; }


        /* CONSTRUCTOR */
        internal DynamicValue(IValueType valueType, object value)
        {
            ValueType = valueType;
            Value = value;
        }


        /* INDEXER */
        public DynamicValue this[string valueName]
        {
            get
            {
                DataStore dataStore = this as DataStore;

                if(dataStore == null)
                    throw new NotImplementedException("DynamicType " + Name + " was not a DataStore");

                return dataStore.GetValue(valueName);
            }
        }


        /* FUNC */
        public virtual void SetValue(string name, DynamicValue value)
        {
            throw new NotImplementedException("DynamicType " + Name + " was not a DataStore");
        }
        public virtual bool TryGetValue(string name, out DynamicValue value)
        {
            throw new NotImplementedException("DynamicType " + Name + " was not a DataStore");
        }
        public virtual bool ValueExists(string name)
        {
            throw new NotImplementedException("DynamicType " + Name + " was not a DataStore");
        }
        public virtual void CreateDataStore(string name)
        {
            throw new NotImplementedException("DynamicType " + Name + " was not a DataStore");
        }


        /* HELPER FUNC */
        private string GetFullName()
        {
            if(Parent != null)
            {
                string parentName = Parent.GetFullName();

                return (parentName != null ? parentName + "." : "") + Name;
            }
            else
            {
                return null;
            }
        }


        /* OVERRIDES */
        public override string ToString()
        {
            return ValueType.ToString(this);
        }


        /* CAST OVERRIDES */
        public static implicit operator DynamicValue(int other)
        {
            return new DynamicValue(TypeManager.GetValueType<Types.IntegerType>(), other);
        }
        public static implicit operator DynamicValue(float other)
        {
            return new DynamicValue(TypeManager.GetValueType<Types.FloatType>(), other);
        }
        public static implicit operator DynamicValue(string other)
        {
            return new DynamicValue(TypeManager.GetValueType<Types.StringType>(), other);
        }
        public static implicit operator DynamicValue(char other)
        {
            return new DynamicValue(TypeManager.GetValueType<Types.CharType>(), other);
        }
        public static implicit operator DynamicValue(bool other)
        {
            return new DynamicValue(TypeManager.GetValueType<Types.BoolType>(), other);
        }

        public static explicit operator int(DynamicValue self)
        {
            return (int) self.Value;
        }
        public static explicit operator float(DynamicValue self)
        {
            return (float) self.Value;
        }
        public static explicit operator string(DynamicValue self)
        {
            return (string) self.Value;
        }
        public static explicit operator char(DynamicValue self)
        {
            return (char) self.Value;
        }
        public static explicit operator bool(DynamicValue self)
        {
            return (bool) self.Value;
        }
    }
}
