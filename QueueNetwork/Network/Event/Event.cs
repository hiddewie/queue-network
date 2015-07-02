using System;

namespace QueueNetwork {
	public abstract class Event : EventArgs {
		public Network Network {
			get;
			set;
		}

		public Component Receiver {
			get;
			set;
		}

		public double IssueTime {
			get;
			set;
		}
	}
}

