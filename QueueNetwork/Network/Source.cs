using System;

namespace QueueNetwork {
	public abstract class Source : Component, IDeparting {
		public event EventHandler PreDepart;
		public event EventHandler PostDepart;

		public IArriving DepartLocation {
			get;
			set;
		}

		public void CallPreDepart (DepartEventArgs eventArgs) {
			if (PreDepart != null) {
				PreDepart (this, eventArgs);
			}
		}
		public void CallPostDepart (DepartEventArgs eventArgs) {
			if (PostDepart != null) {
				PostDepart (this, eventArgs);
			}
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

