using System;
using System.Collections.Generic;

namespace QueueNetwork {
	public abstract class Router : Location {
		protected Unit currentUnit;

		public override void Arrive(Unit unit, Component source) {
			currentUnit = unit;
			Trigger (new DepartTrigger (this));
		}

		public override Dictionary<Trigger, double> NextTriggers () {
			return new Dictionary<Trigger, double> {
				{new DepartTrigger (this), Constants.INF},
			};
		}
		public override bool HasUnits () {
			return false;
		}
	}
}

