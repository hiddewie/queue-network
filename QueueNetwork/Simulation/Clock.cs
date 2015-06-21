using System;

using QueueNetwork.Simulation;

namespace QueueNetwork {
	public class Clock {

		private static double time = 0.0;

		public static double getTime () {
			return time;
		}
	}
}

