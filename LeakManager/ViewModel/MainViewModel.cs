using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using LeakManager.Model;
using System;
using System.Collections.ObjectModel;

namespace LeakManager.ViewModel
{
    public class MainViewModel : ViewModelBase
    {
        private readonly IDataService _dataService;       
        private ObservableCollection<Leak> _leaksCollection;
        private Leak _leakInfo;
        private Comment _commentInfo;

        public RelayCommand AddLeakCommand { get; set; }

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
            LeakInfo = new Leak();
            CommentInfo = new Comment();
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

        /*
        public override void Cleanup()
        {
            // Clean up if needed
            base.Cleanup();
        }
        */
    }
}