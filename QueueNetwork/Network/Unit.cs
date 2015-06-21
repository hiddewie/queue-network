using System;
using System.Collections.Generic;

namespace QueueNetwork {
	/**
	 * A unit goes from a source to a sink
	 */
	public abstract class Unit {
		abstract public void SetArriveTime(double arriveTime);
		abstract public double SetDepartTime(double departTime);
	}
}

