using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Threading;

namespace MetarAnalyzerApp.ViewModel
{
    public class ViewModel : INotifyPropertyChanged
    {
        private static readonly Dispatcher Dispatcher;
        private static readonly DispatcherSynchronizationContext DispatcherSynchronizationContext;

        static ViewModel()
        {
            Dispatcher = Application.Current?.Dispatcher;
            DispatcherSynchronizationContext = Dispatcher != null
                ? new DispatcherSynchronizationContext(Dispatcher)
                : null;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        internal virtual void OnShow()
        {
        }

        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            var eventHandler = PropertyChanged;
            if (eventHandler == null) return;
            if (Dispatcher == null || Dispatcher.CheckAccess())
            {
                eventHandler(this, new PropertyChangedEventArgs(propertyName));
            }
            else
            {
                DispatcherSynchronizationContext.Send(arg => 
                    eventHandler(this, new PropertyChangedEventArgs(propertyName)), null);
            }
        }
    }
}
