using System;
using System.Collections.Generic;

namespace QueueNetwork {
	/**
	 * A unit goes from a source to a sink
	 */
	public class Unit {
		public double SystemArriveTime {
			get;
			set;
		}
		public double SystemDepartTime {
			get;
			set;
		}
		public Source Source {
			get;
			set;
		}
		public Sink Sink {
			get;
			set;
		}
	}
}

