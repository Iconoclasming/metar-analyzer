namespace MetarAnalyzerApp.ViewModel
{
    public class MainViewModel : ViewModel
    {
        private ViewModel _contentViewModel;

        public ViewModel ContentViewModel
        {
            get => _contentViewModel;
            set
            {
                _contentViewModel = value;
                OnPropertyChanged();
            }
        }

        internal override void OnShow()
        {
            ViewModelManager.Show(new MetarAnalyzerViewModel());
        }
    }
}
