using System;

namespace QueueNetwork {
	public abstract class Router : Location {
		protected Unit currentUnit;

		public override void Arrive(Unit unit) {
			currentUnit = unit;
			Depart ();
		}

		public override double NextDeparture () {
			return Constants.INF;
		}
		public override bool HasUnits () {
			return false;
		}
	}
}

