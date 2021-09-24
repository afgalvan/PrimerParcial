using System;

namespace Presentation.Utils
{
    public readonly struct ARange
    {
        public        IComparable Start { get; }
        public        IComparable End   { get; }
        public static ARange      All   => new(int.MinValue, int.MaxValue);

        public ARange(int start, int end)
        {
            Start = start;
            End   = end;
        }

        public bool HasValue(IComparable value)
        {
            return value.CompareTo(Start) >= 0 && value.CompareTo(End) <= 0;
        }
    }
}
