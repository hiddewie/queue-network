using System;
using System.Collections.Generic;

using QueueNetwork.Simulation;

namespace QueueNetwork.Network {
	/**
	 * A unit goes from a source to a sink
	 */
	public interface IUnit {
		void setArriveTime(Time arriveTime);
		Time setDepartTime(Time departTime);
	}
}

