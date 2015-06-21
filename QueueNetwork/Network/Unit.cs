using System;
using System.Collections.Generic;

namespace QueueNetwork {
	/**
	 * A unit goes from a source to a sink
	 */
	public abstract class Unit {
		void SetArriveTime(double arriveTime);
		double SetDepartTime(double departTime);
	}
}

