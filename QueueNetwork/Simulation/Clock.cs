using System;

using QueueNetwork.Simulation;

namespace QueueNetwork {
	public class Clock {

		private static double time = 0.0;

		public static double getTime () {
			return time;
		}

		public static void advance (double t) {
			if (t <= 0) {
				throw new Exception (String.Format("Invalid advance time {0}", time));
			}
			time += t;
		}
	}
}

