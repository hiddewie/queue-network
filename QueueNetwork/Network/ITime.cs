using System;
using System.Collections.Generic;

namespace QueueNetwork.Network {
	/**
	 * A time represents a specific time during the simulation
	 */
	public interface ITime {
		void setTime (double time);
		double getTime ();
		double diff (double other);
		int compareTo(double other);
	}
}

