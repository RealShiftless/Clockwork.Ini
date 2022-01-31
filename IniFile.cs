using Clockwork.Ini.Core;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Clockwork.Ini
{
    public class IniFile
    {
        internal IniData IniData;

        public DataStore Global
        {
            get
            {
                return IniData.Global;
            }
        }

        public DynamicValue this[string name]
        {
            get
            {
                return IniData.Global.GetValue(name);
            }
        }

        public IniFile()
        {
            IniData = new IniData();

        }
        private IniFile(IniData data)
        {
            IniData = data;
        }
        
        public static IniFile Load(string path)
        {
            StreamReader reader = new StreamReader(path);

            return new IniFile(IniParser.Parse(reader));
        }

        public void Save(string path)
        {
            StreamWriter writer = new StreamWriter(path);

            foreach(DataStore store in IniData.DataStores)
            {
                string str = TypeManager.GetValueType<Types.DataStoreType>().SerializeToString(store);
                writer.Write(str);
            }

            writer.Flush();
            writer.Close();
        }

        public void CreateDataStore(string name)
        {
            IniData.Global.CreateDataStore(name);
        }
        public void SetValue(string name, DynamicValue value)
        {
            IniData.Global.SetValue(name, value);
        }
    }
}
