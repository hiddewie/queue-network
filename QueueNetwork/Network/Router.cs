using System;
using System.Collections.Generic;

namespace QueueNetwork {
	public abstract class Router : Location {
		protected Unit currentUnit;

		public override void Arrive(Unit unit) {
			currentUnit = unit;
			Trigger (new DepartEvent ());
		}

		public override Dictionary<Event, double> NextEvents () {
			return new Dictionary<Event, double> {
				{new DepartEvent (), Constants.INF},
			};
		}
		public override bool HasUnits () {
			return false;
		}
	}
}

