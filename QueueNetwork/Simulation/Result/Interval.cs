using System;

namespace QueueNetwork {
	public class Interval<T> {
		public Interval (T start, T end) {
			Start = start;
			End = end;
		}

		public T Start { get; set; }

		public T End { get; set; }

		public override string ToString () {
			return string.Format ("[{0}, {1}]", Start, End);
		}
	}
}

