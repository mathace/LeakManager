using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace LeakManager.Model
{
    public interface IDataService
    {
        void LoadLeaks(Action<ObservableCollection<Leak>, Exception> callback);
        void SaveLeaks(ObservableCollection<Leak> leaks);
    }
}
