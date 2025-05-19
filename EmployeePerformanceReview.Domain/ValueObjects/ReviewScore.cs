using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeePerformanceReview.Domain.ValueObjects
{
    public sealed class ReviewScore : IEquatable<ReviewScore>
    {
        public int Value { get; private set; }

        public ReviewScore() { }
        public ReviewScore(int value)
        {
            Value = value;
        }

        public static ReviewScore Create(int value)
        {
            if (value < 1 || value > 5) // or your business rule, e.g., 0–10
                throw new ArgumentOutOfRangeException(nameof(value), "Review score must be between 1 and 5.");

            return new ReviewScore(value);
        }

        public override bool Equals(object obj) => Equals(obj as ReviewScore);

        public bool Equals(ReviewScore other) => other != null && Value == other.Value;

        public override int GetHashCode() => Value.GetHashCode();

        public static bool operator ==(ReviewScore left, ReviewScore right) => Equals(left, right);

        public static bool operator !=(ReviewScore left, ReviewScore right) => !Equals(left, right);

        public override string ToString() => Value.ToString();
    }
}
