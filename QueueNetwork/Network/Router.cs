using System;
using System.Collections.Generic;

namespace QueueNetwork {
	/**
	 * A router routes units from one location to another. It might absorb the unit when it acts like a sink.
	 */
	public abstract class Router : Component {
		public abstract void Receive (Unit unit);
	}
}

