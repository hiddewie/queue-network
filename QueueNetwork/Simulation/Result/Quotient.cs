using System;

namespace QueueNetwork {
	public class Quotient<T> {
		public Quotient (T numerator, T denominator) {
			Numerator = numerator;
			Denominator = denominator;
		}

		public T Numerator { get; set; }

		public T Denominator { get; set; }

		public override string ToString () {
			return string.Format ("{0} / {1}", Numerator, Denominator);
		}
	}
}

