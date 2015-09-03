using System;

namespace QueueNetwork {
	public abstract class Trigger {
		public ITimed Target {
			get;
			protected set;
		}

		public Trigger (ITimed target) {
			this.Target = target;
		}
	}
}

