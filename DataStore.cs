using Clockwork.Ini.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace Clockwork.Ini
{
    public class DataStore : DynamicValue
    {
        internal IniData Data { get; private set; }
        internal int ID;

        private Dictionary<string, DynamicValue> Values
        {
            get
            {
                return (Dictionary<string, DynamicValue>) Value;
            }
        }
        
        internal static DataStore GenGlobal(IniData data)
        {
            DataStore val = new DataStore("Global", data, null);

            return val;
        }

        private DataStore(string name, IniData data, DataStore parent) : base(TypeManager.GetValueType<Types.DataStoreType>(), new Dictionary<string, DynamicValue>()) 
        { 
            Name = name;
            Data = data;
            Parent = parent;

            ID = Data.RegisterDataStore(this);
        }

        public override void CreateDataStore(string name)
        {
            Values.Add(name, new DataStore(name, Data, this));
        }


        /* GET */
        internal DynamicValue GetValue(string name)
        {
            try
            {
                return Values[name];
            }
            catch (KeyNotFoundException)
            {
                throw new Exception("Key: " + name + " was not present in " + Name);
            }

        }


        /* OVERRIDES */
        public override void SetValue(string name, DynamicValue value)
        {
            Values[name] = value;

            value.Name = name;
            value.Parent = this;
        }
        public override bool TryGetValue(string name, out DynamicValue value)
        {
            try
            {
                value = GetValue(name);
                return true;
            }
            catch(KeyNotFoundException)
            {
                value = null;
                return false;
            }
        }
        public override bool ValueExists(string name)
        {
            return Values.ContainsKey(name);
        }
    }
}
