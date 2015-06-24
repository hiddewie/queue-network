using System;
using System.Collections.Generic;

namespace QueueNetwork {
	/**
	 * A location collects units, and outputs them in a later time
	 */
	public abstract class Location : IDeparting, IArriving, ITimed {
		public event EventHandler PreArrive;
		public event EventHandler PostArrive;
		public event EventHandler PreDepart;
		public event EventHandler PostDepart;

		public void CallPreArrive (ArriveEventArgs eventArgs) {
			if (PreArrive != null) {
				PreArrive (this, eventArgs);
			}
		}
		public void CallPostArrive (ArriveEventArgs eventArgs) {
			if (PostArrive != null) {
				PostArrive (this, eventArgs);
			}
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

		public IArriving DepartLocation {
			get;
			set;
		}
		public abstract void Depart();
		public abstract double NextDeparture();
		public abstract void Arrive(Unit unit);
		public abstract bool HasUnits();
	}
}
