using System;
using System.Collections.Generic;

namespace QueueNetwork.Network {
	/**
	 * A unit goes from a source to a sink
	 */
	public interface IUnit {
		void setArriveTime(ITime arriveTime);
		ITime setDepartTime(ITime departTime);
	}
}

