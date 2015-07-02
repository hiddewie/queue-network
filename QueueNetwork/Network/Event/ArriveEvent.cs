using System;

namespace QueueNetwork {
	public class ArriveEvent : Event {
		public Component At {
			get;
			private set;
		}
		public ArriveEvent () {
		}
	}
}

