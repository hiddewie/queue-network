using System;
using System.Collections.Generic;

using QueueNetwork.Distibution;

namespace QueueNetwork {
	public class DistributionSource : Source {
		private IDistribution<double> Distribution;
		private double nextDeparture;

		public DistributionSource (IDistribution<double> distribution) {
			Distribution = distribution;
			nextDeparture = Clock.GetTime() + Distribution.NextRandom ();
		}

		public override Dictionary<Trigger, double> NextTriggers() {
			return new Dictionary<Trigger, double> {
				{new DepartTrigger(this), nextDeparture}
			};
		}

		public override void Trigger (Trigger t) {
			if (t is DepartTrigger) {
				CallPreEvent (new DepartEvent (this, DepartLocation));
				nextDeparture = Clock.GetTime() + Distribution.NextRandom ();

				Unit unit = new Unit ();
				unit.SystemArriveTime = Clock.GetTime ();
				unit.Source = this;

				CallPostEvent (new DepartEvent (this, DepartLocation));
				DepartLocation.Arrive (unit, this);

				return;
			}

			throw new UnknownEventException ();
		}
	}
}

