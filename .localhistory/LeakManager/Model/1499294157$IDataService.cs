using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LeakManager.Model
{
    public interface IDataService
    {
        void GetData(Action<DataItem, Exception> callback);
        void SetData(IEnumerable<Leak> leaks);
    }
}
