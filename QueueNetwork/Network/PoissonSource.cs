using System;
using System.Collections.Generic;

using QueueNetwork.Distibution;

namespace QueueNetwork {
	public class PoissonSource : Source {
		private ExponentialDistribution Distribution;
		private double nextDeparture;

		public PoissonSource (double lambda) {
			Distribution = new ExponentialDistribution(lambda);
			nextDeparture = Distribution.NextRandom ();
		}

		public override Dictionary<Event, double> NextEvents() {
			return new Dictionary<Event, double> {
				{new DepartEvent(), nextDeparture}
			};
		}

		public override void Trigger (Event e) {
			if (e is DepartEvent) {
				CallPreEvent (new DepartEvent ());
				nextDeparture = Distribution.NextRandom ();

				Unit unit = new Unit ();
				unit.SystemArriveTime = Clock.GetTime ();
				unit.Source = this;

				this.DepartLocation.Arrive (unit);
				CallPostEvent (new DepartEvent ());

				return;
			}

			throw new UnknownEventException ();
		}
	}
}

