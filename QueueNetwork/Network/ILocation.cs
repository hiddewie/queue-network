using System;
using System.Collections.Generic;

namespace QueueNetwork.Network {
	/**
	 * A location collects units, and outputs them in a later time
	 */
	public interface ILocation {
		IUnit depart();
		void arrive(IUnit unit);
		bool hasUnits();
	}
}

