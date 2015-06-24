using System;

using QueueNetwork.Distibution;

namespace QueueNetwork {
	public class PoissonSource : Source {
		private ExponentialDistribution Distribution;
		private double nextDeparture;

		public PoissonSource (double lambda) {
			Distribution = new ExponentialDistribution(lambda);
			nextDeparture = Distribution.NextRandom ();
		}

		public override double NextDeparture() {
			return nextDeparture;
		}

		public override void Depart () {
			nextDeparture = Distribution.NextRandom ();

			Unit unit = new Unit ();
			unit.ArriveTime = Clock.GetTime ();

			this.DepartLocation.Arrive (unit);
		}
	}
}

