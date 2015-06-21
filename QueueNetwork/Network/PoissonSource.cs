using System;

using QueueNetwork.Simulation.Distibution;

namespace QueueNetwork {
	public class PoissonSource : Source {
		private ExponentialDistribution Distribution;
		private double nextDeparture;

		public PoissonSource (double lambda) {
			Distribution = new ExponentialDistribution(lambda);
			nextDeparture = Distribution.NextDouble ();
		}

		public override double NextDeparture() {
			return nextDeparture;
		}

		public override void Depart () {
			nextDeparture = Distribution.NextDouble ();

			Unit unit = new Unit ();
			unit.ArriveTime = Clock.getTime ();

			this.DepartLocation.Arrive (unit);
		}

	}
}

