using System;
using System.Collections.Generic;

namespace QueueNetwork.Network {
	/**
	 * A router routes units from one location to another. It might absorb the unit when it acts like a sink.
	 */
	public interface IRouter {
		void Receive (IUnit unit);
	}
}

