using System;

namespace QueueNetwork {
	public class DepartEvent : Event {
		public Component From {
			get;
			private set;
		}
		public IArriving To {
			get;
			private set;
		}

		public DepartEvent (Component from, IArriving to) {
			this.From = from;
			this.To = to;
		}
	}
}

