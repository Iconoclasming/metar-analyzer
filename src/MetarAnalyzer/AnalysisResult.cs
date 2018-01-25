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
            return $"{nameof(ObservationDateTime)}: {ObservationDateTime}, " +
                   $"{nameof(Index)}: {Index}, " +
                   $"{nameof(Wind)}: {{{Wind}}}, " +
                   $"{nameof(Visibility)}: {{{Visibility}}}, " +
                   $"{nameof(CurrentWeather)}: {CurrentWeather}";
        }
    }
}
