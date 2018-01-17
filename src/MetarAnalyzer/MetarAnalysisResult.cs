using System;

namespace MetarAnalyzer
{
    public class MetarAnalysisResult
    {
        public DateTime ObservationDateTime { get; }
        public WindObservation Wind { get; set; }

        public MetarAnalysisResult(DateTime observationDateTime)
        {
            ObservationDateTime = observationDateTime;
        }
    }
}
