using System;

namespace QueueNetwork {
	public interface IDeparting {
		double NextDeparture ();
		Unit Depart();
	}
}

