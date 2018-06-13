using System;

namespace MetarAnalyzer
{
    public class AnalysisResult
    {
        public DateTime ObservationDateTime { get; }
        public string Index { get; }
        public WindObservation Wind { get; set; }
        public VisibilityObservation Visibility { get; set; }
        public string CurrentWeather { get; set; }

        public AnalysisResult(DateTime observationDateTime, string index)
        {
            ObservationDateTime = observationDateTime;
            Index = index;
        }

        public override string ToString()
        {
            return $"{nameof(ObservationDateTime)}: " + ObservationDateTime.Day + " of current month, "
                    + ObservationDateTime.ToString("hh:mm") + " GMT" + Environment.NewLine +
                   $"{nameof(Index)}: {Index}, " + Environment.NewLine +
                   $"{nameof(Wind)}: {{{Wind}}}, " + Environment.NewLine +
                   $"{nameof(Visibility)}: {{{Visibility}}}, " + Environment.NewLine +
                   $"{nameof(CurrentWeather)}: {CurrentWeather}";
        }
    }
}
