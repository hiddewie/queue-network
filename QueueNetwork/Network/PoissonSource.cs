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
			CallPreDepart (new DepartEventArgs ());
			nextDeparture = Distribution.NextRandom ();

			Unit unit = new Unit ();
			unit.SystemArriveTime = Clock.GetTime ();
			unit.Source = this;

			this.DepartLocation.Arrive (unit);
			CallPostDepart (new DepartEventArgs ());
		}
	}
}

