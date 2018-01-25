using System;

namespace MetarAnalyzer
{
    public class MetarAnalysisResult
    {
        public DateTime ObservationDateTime { get; }
        public string Index { get; }
        public WindObservation Wind { get; set; }
        public VisibilityObservation Visibility { get; set; }
        public string CurrentWeather { get; set; }

        public MetarAnalysisResult(DateTime observationDateTime, string index)
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
