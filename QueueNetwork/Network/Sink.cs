using System;

namespace QueueNetwork {
	public class Sink : Component, IArriving {
		public Sink () {
		}

		public void Arrive(Unit unit) {
			Console.WriteLine (String.Format(unit + " has arrived in sink at time {0}", Clock.getTime()));
		}
	}
}

