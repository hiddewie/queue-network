using System;

using QueueNetwork;

namespace QueueNetwork {
	public class Clock {

		private static double time = 0.0;

		public static double GetTime () {
			return time;
		}

		public static void AdvanceTo (double t) {
			if (t < time) {
				throw new Exception (String.Format ("Invalid time {0}, while clock time is {1}", time, t));
			}
			time = t;
		}
	}
}

