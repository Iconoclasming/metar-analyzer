using System.Collections.Generic;

namespace MetarAnalyzer
{
    public class VisibilityObservation
    {
        public int VisibilityValue { get; }
        public int VisibilityHeight { get; }
        public string VisibilityDirection { get; }

        public VisibilityObservation(int visibilityValue, int visibilityHeight, string visibilityDirection)
        {
            VisibilityValue = visibilityValue;
            VisibilityHeight = visibilityHeight;
            VisibilityDirection = visibilityDirection;
        }

        public override bool Equals(object obj)
        {
            var observation = obj as VisibilityObservation;
            return observation != null &&
                   VisibilityValue == observation.VisibilityValue &&
                   VisibilityHeight == observation.VisibilityHeight &&
                   VisibilityDirection == observation.VisibilityDirection;
        }

        public override int GetHashCode()
        {
            var hashCode = -1479429754;
            hashCode = hashCode * -1521134295 + VisibilityValue.GetHashCode();
            hashCode = hashCode * -1521134295 + VisibilityHeight.GetHashCode();
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(VisibilityDirection);
            return hashCode;
        }

        public override string ToString()
        {
            return $"{nameof(VisibilityValue)}: {VisibilityValue} m, " +
                $"{nameof(VisibilityHeight)}: {VisibilityHeight} m, " +
                $"{nameof(VisibilityDirection)}: {VisibilityDirection}";
        }
    }
}
