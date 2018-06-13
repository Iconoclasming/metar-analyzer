using System;

namespace MetarAnalyzerApp.Commands
{
    public class CancelCommandEventArgs : EventArgs
    {
        public object Parameter { get; internal set; }
        public bool Cancel { get; internal set; }
    }
}
