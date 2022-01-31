using System;
using System.Collections.Generic;
using System.Text;

namespace Clockwork.Ini.Core
{
    internal class IniData
    {
        public DataStore Global { get; private set; }
        
        internal List<DataStore> DataStores = new List<DataStore>();

        internal IniData()
        {
            Global = DataStore.GenGlobal(this);
        }

        /// <summary>
        /// Registers data store and returns its handle
        /// </summary>
        /// <param name="store">The data store</param>
        /// <returns>its handle</returns>
        internal int RegisterDataStore(DataStore store)
        {
            DataStores.Add(store);

            return DataStores.Count - 1;
        }
    }
}
