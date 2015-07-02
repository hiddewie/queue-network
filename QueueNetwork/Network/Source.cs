using System;
using System.Collections.Generic;

namespace QueueNetwork {
	public abstract class Source : Component, IDeparting {
		public event EventHandler PreEvent;
		public event EventHandler PostEvent;

		public IArriving DepartLocation {
			get;
			set;
		}

		public void CallPreEvent (Event eventArgs) {
			if (PreEvent != null) {
				PreEvent (this, eventArgs);
			}
		}
		public void CallPostEvent (Event eventArgs) {
			if (PostEvent != null) {
				PostEvent (this, eventArgs);
			}
		}

		public Source () {
		}
		public Source (Location location) {
			this.DepartLocation = location;
		}
		public abstract Dictionary<Event, double> NextEvents ();
		public abstract void Trigger (Event e);
	}
}

