using System;

namespace QueueNetwork {
	public class Component {
		public Network Parent {
			get;
			set;
		}
		public String Name {
			get;
			protected set;
		}
	}
}
