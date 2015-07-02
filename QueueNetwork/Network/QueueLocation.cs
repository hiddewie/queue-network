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
					nextDeparture = Clock.GetTime() + distribution.NextRandom ();
				} else {
					nextDeparture = Constants.INF;
				}

				active = value;
			}
			get {
				return active;
			}
		}

		public override void Arrive (Unit unit, Component source) {
			CallPreArrive (new ArriveEvent ());
			if (!HasUnits() && active) {
				nextDeparture = Clock.GetTime() + distribution.NextRandom ();
			}
			queue.Enqueue (unit);
			CallPostArrive (new ArriveEvent ());
		}

		public override bool HasUnits () {
			return queue.Count > 0;
		}

		public override void Trigger (Trigger t) {
			if (t is DepartTrigger) {
				if (!active) {
					throw new Exception ("Departing while queue is not active");
				}
				if (!HasUnits ()) {
					throw new Exception ("Departing while no units in queue");
				}
				CallPreEvent (new DepartEvent (this, DepartLocation));

				Unit unit = (Unit)queue.Dequeue ();
				if (HasUnits ()) {
					nextDeparture = Clock.GetTime () + distribution.NextRandom ();
				} else {
					nextDeparture = Constants.INF;
				}
				CallPostEvent (new DepartEvent (this, DepartLocation));
				DepartLocation.Arrive (unit, this);

				return;
			}

			throw new UnknownEventException ();
		}

		public override Dictionary<Trigger, double> NextTriggers () {
			return new Dictionary<Trigger, double> {
				{ new DepartTrigger (this), nextDeparture }
			};
		}
	}
}

