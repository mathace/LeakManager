using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeakManager.Model
{
    [Serializable()]
    public class Leak
    {
        public DateTime CreateDate { get; set; }
        public string Title { get; set; }
        public ObservableCollection<Comment> Comments { get; set; }
    }
    [Serializable()]
    public class Comment
    {
        public DateTime CreateDate { get; set; }
        public string Text { get; set; }
    }
}
