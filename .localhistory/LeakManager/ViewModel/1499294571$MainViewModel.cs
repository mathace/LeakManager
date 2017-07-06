using GalaSoft.MvvmLight;
using LeakManager.Model;
using System;
using System.Collections.ObjectModel;

namespace LeakManager.ViewModel
{
    /// <summary>
    /// This class contains properties that the main View can data bind to.
    /// <para>
    /// See http://www.mvvmlight.net
    /// </para>
    /// </summary>
    public class MainViewModel : ViewModelBase
    {
        private readonly IDataService _dataService;

        /// <summary>
        /// The <see cref="WelcomeTitle" /> property's name.
        /// </summary>
        public const string WelcomeTitlePropertyName = "WelcomeTitle";

        private string _welcomeTitle = string.Empty;
        private ObservableCollection<Leak> _leaks;
        /// <summary>
        /// Gets the WelcomeTitle property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public string WelcomeTitle
        {
            get
            {
                return _welcomeTitle;
            }
            set
            {
                Set(ref _welcomeTitle, value);
            }
        }
        public ObservableCollection<Leak> Leaks
        {
             get{ return _leaks; }
            set { Set(ref _leaks, value); }
        }

        /// <summary>
        /// Initializes a new instance of the MainViewModel class.
        /// </summary>
        public MainViewModel(IDataService dataService)
        {
            _dataService = dataService;
            _dataService.GetData(
                (item, error) =>
                {
                    if (error != null)
                    {
                        // Report error here
                        return;
                    }

                    WelcomeTitle = item.Title;
                });

            Leaks = new ObservableCollection<Leak>()
            {
                new Leak() {CreateDate = DateTime.Today, Title="LeakTitle1", Comments=new ObservableCollection<Comment>
                {new Comment() {CreateDate = DateTime.Today, Text = "comment1" },
                new Comment() {CreateDate = DateTime.Today, Text = "comment2" },
                new Comment() {CreateDate = DateTime.Today, Text = "comment3" }
                }},
                new Leak() {CreateDate = DateTime.Today, Title="LeakTitle2", Comments=new ObservableCollection<Comment>
                {new Comment() {CreateDate = DateTime.Today, Text = "comment1" },
                new Comment() {CreateDate = DateTime.Today, Text = "comment2" },
                new Comment() {CreateDate = DateTime.Today, Text = "comment3" }
                }}
            };
            _dataService.SetData(Leaks);

        }

        ////public override void Cleanup()
        ////{
        ////    // Clean up if needed

        ////    base.Cleanup();
        ////}
    }
}