using System;

namespace QueueNetwork {
	public abstract class Source : Component, IDeparting {
		public IArriving DepartLocation {
			get;
			set;
		}

		public Source () {
		}
		public Source (Location location) {
			this.DepartLocation = location;
		}
		public abstract double NextDeparture ();
		public abstract void Depart ();
	}
}

