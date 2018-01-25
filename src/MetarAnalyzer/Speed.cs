namespace MetarAnalyzer
{
    public enum SpeedUnit
    {
        Mps,
        Kt
    }

    public struct Speed
    {
        public int Value { get; }
        public SpeedUnit Unit { get; }

        public Speed(int value, SpeedUnit unit)
        {
            Value = value;
            Unit = unit;
        }

        public bool Equals(Speed other)
        {
            return Value == other.Value && Unit == other.Unit;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            return obj is Speed && Equals((Speed) obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return (Value * 397) ^ (int) Unit;
            }
        }

        public override string ToString()
        {
            return $"{Value} {Unit.ToString().ToLower()}";
        }
    }
}
