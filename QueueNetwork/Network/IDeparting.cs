using System;

namespace QueueNetwork {
	public interface IDeparting : ITimed {
		event EventHandler PreDepart;
		event EventHandler PostDepart;
		void CallPreDepart (DepartEventArgs eventArgs);
		void CallPostDepart (DepartEventArgs eventArgs);

		IArriving DepartLocation {
			get;
			set;
		}
		void Depart();
	}
}

