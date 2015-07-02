using System;

namespace QueueNetwork {
	public abstract class Event : EventArgs {
		public Network Owner {
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

