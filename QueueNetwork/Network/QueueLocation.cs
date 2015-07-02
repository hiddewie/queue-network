using System;
using System.Collections;
using System.Collections.Generic;
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
			CallPreArrive (new ArriveEvent ());
			if (!HasUnits () && active) {
				nextDeparture = distribution.NextRandom ();
			}
			queue.Enqueue (unit);
			CallPostArrive (new ArriveEvent ());
		}

		public override bool HasUnits () {
			return queue.Count > 0;
		}

		public override void Trigger (Event e) {
			if (e is DepartEvent) {
				if (!active) {
					throw new Exception ("Departing while queue is not active");
				}
				if (!HasUnits ()) {
					throw new Exception ("Departing while no units in queue");
				}
				CallPreEvent (new DepartEvent ());
				DepartLocation.Arrive ((Unit)queue.Dequeue ());
				CallPostEvent (new DepartEvent ());

				return;
			}

			throw new UnknownEventException ();
		}

		public override Dictionary<Event, double> NextEvents () {
			return new Dictionary<Event, double> {
				{ new DepartEvent (), nextDeparture }
			};
		}
	}
}

