using System;

namespace QueueNetwork {
	public class Sink : Component, IArriving {
		private int arrived = 0;

		public Sink () {
		}

		public void Arrive(Unit unit) {
			arrived++;
			Console.WriteLine (String.Format(unit + " has arrived in sink at time {0}", Clock.GetTime()));
		}

		public int Arrived () {
			return arrived;
		}
	}
}

