using System;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;

namespace LeakManager.Model
{
    public class DataService : IDataService
    {
        public void GetData(Action<DataItem, Exception> callback)
        {
            // Use this to connect to the actual data service

            var item = new DataItem("Welcome to MVVM Light");
            callback(item, null);            
        }
        public void SetData(IEnumerable<Leak> leaks)
        {
            var serializer = new XmlSerializer(typeof(IEnumerable<Leak>));
            var writer = File.AppendText("leaks.xml");
            serializer.Serialize(writer, leaks);
        }
    }    
}