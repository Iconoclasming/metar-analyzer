using MetarAnalyzer;
using MetarAnalyzerApp.Commands;
using System.Windows.Input;

namespace MetarAnalyzerApp.ViewModel
{
    public class MetarAnalyzerViewModel : ViewModel
    {
        private string _input;
        private string _output;
        private ICommand _parseCommand;

        public string Input
        {
            get => _input;
            set
            {
                _input = value;
                OnPropertyChanged();
                Parse();
            }
        }

        public string Output
        {
            get => _output;
            set
            {
                _output = value;
                OnPropertyChanged();
            }
        }

        public ICommand ParseCommand => _parseCommand ?? (_parseCommand = new ActionCommand(Parse));

        private void Parse()
        {
            var analysisResult = MetarAnalyzerImpl.Analyze(Input, out var validation);
            Output = analysisResult?.ToString() ?? validation.ToString();
        }
    }
}
