using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using LeakManager.Model;
using System;
using System.Collections.ObjectModel;
using System.Windows.Data;

namespace LeakManager.ViewModel
{
    public class MainViewModel : ViewModelBase
    {
        private readonly IDataService _dataService;       
        private ObservableCollection<Leak> _leaksCollection;
        private Leak _leakInfo;
        private Comment _commentInfo;
        private String _commentTextInfo;

        public RelayCommand AddLeakCommand { get; set; }
        public RelayCommand<DateTime> DeleteLeakCommand { get; set; }
        public RelayCommand<DateTime> AddCommentCommand { get; set; }

        public ObservableCollection<Leak> Leaks
        {
             get{ return _leaksCollection; }
            set { Set(ref _leaksCollection, value); }
        }

        public Leak LeakInfo
        {
            get { return _leakInfo; }
            set { Set(ref _leakInfo, value); }
        }

        public Comment CommentInfo
        {
            get { return _commentInfo; }
            set { Set(ref _commentInfo, value); }
        }

        public String CommentTextInfo
        {
            get { return _commentTextInfo; }
            set { Set(ref _commentTextInfo, value); }
        }

        public MainViewModel(IDataService dataService)
        {
            _dataService = dataService;
            _dataService.LoadLeaks(
                (leaks, error) =>
                {
                    if (error != null)
                    {
                        // Report error here
                        return;
                    }
                    Leaks = leaks;
                });

            AddLeakCommand = new RelayCommand(AddLeak);
            DeleteLeakCommand = new RelayCommand<DateTime>((d)=>DeleteLeak(d));
            AddCommentCommand = new RelayCommand<DateTime>((d) => AddComment(d));
            LeakInfo = new Leak();
            CommentInfo = new Comment();
            CommentTextInfo = "";
        }

        public void AddLeak()
        {
            var tempLeak = new Leak()
            {
                CreateDate = LeakInfo.CreateDate,
                Title = LeakInfo.Title,
                Comments = new ObservableCollection<Comment>()
            };
            var tempComment = new Comment()
            {
                CreateDate = LeakInfo.CreateDate,
                Text = CommentInfo.Text
            };
            tempLeak.Comments.Add(tempComment);
            Leaks.Add(tempLeak);
            _dataService.SaveLeaks(Leaks);
            RaisePropertyChanged("Leaks");
            LeakInfo = new Leak();
            CommentInfo = new Comment();
        }

        public void DeleteLeak(DateTime ldate)
        {
            Leak found = null;
            foreach (var item in Leaks)
            {
                if (item.CreateDate == ldate)
                    found = item;
            }
            if (found != null)
            {
                Leaks.Remove(found);
                _dataService.SaveLeaks(Leaks);
            }
            else
            {
                Console.WriteLine("Leak date not found!");
            }
        }

        public void AddComment(DateTime ldate)
        {
            Leak found = null;
            
            foreach (var item in Leaks)
            {
                if (item.CreateDate == ldate)
                    found = item;
            }
            if (found != null)
            {
                found.Comments.Add(new Comment { CreateDate = DateTime.Now, Text=CommentTextInfo });
                _dataService.SaveLeaks(Leaks);
                CommentTextInfo = "";
            }
            else
            {
                Console.WriteLine("Leak date not found!");
            }
        }

        /*
        public override void Cleanup()
        {
            // Clean up if needed
            base.Cleanup();
        }
        */
    }
}