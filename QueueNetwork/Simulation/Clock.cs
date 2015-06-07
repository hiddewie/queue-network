using System;

using QueueNetwork.Simulation;

namespace QueueNetwork {
	public class Clock {

		private static Time time;

		public Clock () {
			time = new Time (0.0);
		}

		public static Time getTime () {
			return time;
		}
	}
}

