using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Xml.Serialization;

namespace LeakManager.Model
{
    public class DataService : IDataService
    {
        private string _filename = "leaks.xml";

        public DataService()
        {
            if (!File.Exists(_filename))
            {
                File.Create(_filename).Close();
            }
        }

        public void LoadLeaks(Action<ObservableCollection<Leak>, Exception> callback)
        {
            ObservableCollection<Leak> leaks = null;

            try
            {
                using (var reader = new FileStream(_filename, FileMode.Open))
                {
                    if (reader.Length > 0)
                    {
                        var serializer = new XmlSerializer(typeof(ObservableCollection<Leak>));
                        leaks = (ObservableCollection<Leak>)serializer.Deserialize(reader);
                    }
                    else
                    {
                        leaks = new ObservableCollection<Leak> {
                        new Leak { CreateDate = DateTime.Today, Title = "Leak Title 1", Comments = new ObservableCollection<Comment> { new Comment {CreateDate = DateTime.Now, Text = "Comment 1"} } },
                        new Leak { CreateDate = DateTime.Today, Title = "Leak Title 2", Comments = new ObservableCollection<Comment> { new Comment {CreateDate = DateTime.Now, Text = "Comment 2"} } },
                        new Leak { CreateDate = DateTime.Today, Title = "Leak Title 3", Comments = new ObservableCollection<Comment> { new Comment {CreateDate = DateTime.Now, Text = "Comment 3"} } }
                        };
                    } 
                }
            }
            catch (Exception)
            {
                Console.WriteLine("LoadLeaks Error!");
                throw;
            }
            finally
            {
                if (leaks != null)
                    callback(leaks, null);
            }
        }

        public void SaveLeaks(ObservableCollection<Leak> leaks)
        {
            try
            {
                using (var writer = new FileStream(_filename, FileMode.Create))
                {
                    var serializer = new XmlSerializer(typeof(ObservableCollection<Leak>));
                    serializer.Serialize(writer, leaks);
                }
            }
            catch (Exception)
            {
                Console.WriteLine("SaveLeaks Error!");
                throw;
            }
        }
    }    
}