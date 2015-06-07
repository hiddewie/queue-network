using System;
using System.Collections.Generic;

namespace QueueNetwork.Simulation {
	/**
	 * A time represents a specific time during the simulation
	 */
	public class Time : IComparable {
		private readonly double timestamp;

		public Time (double time) {
			timestamp = time;
		}

		public double getTime () {
			return timestamp;
		}
		public double diff (Time other) {
			return other.timestamp - timestamp;
		}
		public int compareTo(object obj) {
			if (obj == null) {
				return 1;
			}

			Time other = obj as Time;
			if (obj == null) {
				throw new ArgumentException("Object is not a Time");
			}
			return timestamp.CompareTo (other.timestamp);
		}
	}
}

