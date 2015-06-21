using System;
using System.Collections.Generic;

namespace QueueNetwork {
	/**
	 * A location collects units, and outputs them in a later time
	 */
	public abstract class Location : IDeparting {
		public abstract Unit Depart();
		public abstract double NextDeparture();
		public abstract void Arrive(Unit unit);
		public abstract bool HasUnits();
	}
}
