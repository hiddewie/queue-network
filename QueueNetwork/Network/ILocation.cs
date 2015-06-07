using System;
using System.Collections.Generic;

namespace QueueNetwork.Network {
	/**
	 * A location collects units, and outputs them in a later time
	 */
	public interface ILocation {
		IUnit Depart();
		void Arrive(IUnit unit);
		bool HasUnits();
	}
}

