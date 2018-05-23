using System.Diagnostics;

namespace MetarAnalyzerApp.ViewModel
{
    internal static class ViewModelManager
    {
        private static MainViewModel _mainViewModel;

        public static MainViewModel MainViewModel
        {
            get { return _mainViewModel; }
            set
            {
                Debug.Assert(_mainViewModel == null);
                _mainViewModel = value;
                _mainViewModel.OnShow();
            }
        }

        public static void Show(ViewModel viewModel)
        {
            Debug.Assert(MainViewModel != null);
            Debug.Assert(viewModel != null);
            MainViewModel.ContentViewModel = viewModel;
            viewModel.OnShow();
        }
    }
}
