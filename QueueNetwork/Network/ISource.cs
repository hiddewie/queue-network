using System;
using System.Collections.Generic;

namespace QueueNetwork.Network {
	/**
	 * A source generates units
	 */
	public interface ISource {
		void setRouter(IRouter router);
		IUnit generateUnit ();
	}
}

