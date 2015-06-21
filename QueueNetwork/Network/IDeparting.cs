using System;

namespace QueueNetwork {
	public interface IDeparting : ITimed {
		IArriving DepartLocation {
			get;
			set;
		}
		void Depart();
	}
}

