using System;
using System.Collections.Generic;

namespace QueueNetwork {
	/**
	 * A location collects units, and outputs them in a later time
	 */
	public abstract class Location : IDeparting, IArriving, ITimed {
		public event EventHandler PreArrive;
		public event EventHandler PostArrive;
		public event EventHandler PreEvent;
		public event EventHandler PostEvent;

		public void CallPreArrive (ArriveEvent eventArgs) {
			if (PreArrive != null) {
				PreArrive (this, eventArgs);
			}
		}
		public void CallPostArrive (ArriveEvent eventArgs) {
			if (PostArrive != null) {
				PostArrive (this, eventArgs);
			}
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

		public IArriving DepartLocation {
			get;
			set;
		}
		public abstract Dictionary<Event, double> NextEvents();
		public abstract void Trigger(Event e);
		public abstract void Arrive(Unit unit);
		public abstract bool HasUnits();
	}
}
