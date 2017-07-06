using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace LeakManager.Model
{
    public interface IDataService
    {
        void GetData(Action<DataItem, Exception> callback);
        void SetData(ObservableCollection<Leak> leaks);
    }
}
