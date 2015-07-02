using System;

using QueueNetwork.Simulation;

namespace QueueNetwork {
	public class Clock {

		private static double time = 0.0;

		public static double GetTime () {
			return time;
		}

		public static void AdvanceTo (double t) {
			if (time < t) {
				throw new Exception (String.Format("Invalid time {0}, while clock time is {1}", time, t));
			}
			time = t;
		}
	}
}

