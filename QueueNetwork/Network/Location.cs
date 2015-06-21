using System;
using System.Collections.Generic;

namespace QueueNetwork {
	/**
	 * A location collects units, and outputs them in a later time
	 */
	public abstract class Location {
		Unit Depart();
		void Arrive(Unit unit);
		bool HasUnits();
	}
}

