using System;
using System.Collections;
using QueueNetwork.Distibution;

namespace QueueNetwork {
	public class QueueLocation : Location {
		private IDistribution<double> distribution;
		private Queue queue = new Queue();
		private double nextDeparture = Constants.INF;

		public QueueLocation (IDistribution<double> distribution) {
			this.distribution = distribution;
		}

		private bool active = true;
		public bool Active {
			set {
				if (value && HasUnits ()) {
					nextDeparture = distribution.NextRandom ();
				} else {
					nextDeparture = Constants.INF;
				}

				active = value;
			}
			get {
				return active;
			}
		}

		public override void Arrive (Unit unit) {
			if (!HasUnits () && active) {
				nextDeparture = distribution.NextRandom ();
			}
			queue.Enqueue (unit);
		}

		public override bool HasUnits () {
			return queue.Count > 0;
		}

		public override void Depart () {
			if (!active) {
				throw new Exception ("Departing while queue is not active");
			}
			if (!HasUnits ()) {
				throw new Exception ("Departing while no units in queue");
			}
			DepartLocation.Arrive ((Unit) queue.Dequeue ());
		}

		public override double NextDeparture () {
			return nextDeparture;
		}
	}
}

