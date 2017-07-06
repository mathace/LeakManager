using System;
using System.Collections.Generic;
using LeakManager.Model;

namespace LeakManager.Design
{
    public class DesignDataService : IDataService
    {
        public void GetData(Action<DataItem, Exception> callback)
        {
            // Use this to create design time data

            var item = new DataItem("Welcome to MVVM Light [design]");
            callback(item, null);
        }

        public void SetData(IEnumerable<Leak> leaks)
        {
            throw new NotImplementedException();
        }
    }
}