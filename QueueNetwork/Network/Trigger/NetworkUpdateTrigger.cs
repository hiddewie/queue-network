using System;

namespace QueueNetwork {
	public class NetworkUpdateTrigger : Trigger {
		public Trigger OriginalTrigger {
			get;
			protected set;
		}

		public NetworkUpdateTrigger (ITimed target, Trigger originalTrigger) : base(target) {
			this.OriginalTrigger = originalTrigger;
		}
	}
}

