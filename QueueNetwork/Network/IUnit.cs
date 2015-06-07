using System;
using System.Collections.Generic;

using QueueNetwork.Simulation;

namespace QueueNetwork.Network {
	/**
	 * A unit goes from a source to a sink
	 */
	public interface IUnit {
		void SetArriveTime(Time arriveTime);
		Time SetDepartTime(Time departTime);
	}
}

