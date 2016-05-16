using System;

namespace QueueNetwork {
	public abstract class Component {
		public Network Parent {
			get;
			set;
		}
		public string Name {
			get;
			set;
		}
	}
}
