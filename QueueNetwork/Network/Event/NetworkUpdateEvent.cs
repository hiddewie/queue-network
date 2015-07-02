using System;

namespace QueueNetwork {
	public class NetworkUpdateEvent : Event {
		public Trigger ComponentTrigger {
			get;
			protected set;
		}
		public NetworkUpdateEvent (Trigger componentTrigger) {
			this.ComponentTrigger = componentTrigger;
		}
	}
}

