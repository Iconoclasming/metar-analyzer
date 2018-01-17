namespace MetarAnalyzer
{
    public class WindObservation
    {
        public int Direction { get; }
        public Speed Speed { get; }
        public int Gust { get; }
        public DirectionChangeValues DirectionChange { get; }

        public WindObservation(int direction, Speed speed, int gust, DirectionChangeValues directionChange)
        {
            Direction = direction;
            Speed = speed;
            Gust = gust;
            DirectionChange = directionChange;
        }

        protected bool Equals(WindObservation other)
        {
            return Direction == other.Direction && Speed.Equals(other.Speed) && Gust == other.Gust;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            return obj.GetType() == GetType() && Equals((WindObservation) obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = Direction;
                hashCode = (hashCode * 397) ^ Speed.GetHashCode();
                hashCode = (hashCode * 397) ^ Gust;
                hashCode = (hashCode * 397) ^ DirectionChange.GetHashCode();
                return hashCode;
            }
        }

        public override string ToString()
        {
            return $"{nameof(Direction)}: {Direction}, {nameof(Speed)}: {{{Speed}}}, {nameof(Gust)}: {Gust}," +
                   $" {nameof(DirectionChange)}: {{{DirectionChange}}}";
        }

        public struct DirectionChangeValues
        {
            public int From { get; }
            public int To { get; }

            public DirectionChangeValues(int @from, int to)
            {
                From = @from;
                To = to;
            }

            public bool Equals(DirectionChangeValues other)
            {
                return From == other.From && To == other.To;
            }

            public override bool Equals(object obj)
            {
                if (ReferenceEquals(null, obj)) return false;
                return obj is DirectionChangeValues && Equals((DirectionChangeValues) obj);
            }

            public override int GetHashCode()
            {
                unchecked
                {
                    return (From * 397) ^ To;
                }
            }

            public override string ToString()
            {
                return $"{nameof(From)}: {From}, {nameof(To)}: {To}";
            }
        }
    }
}
