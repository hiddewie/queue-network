using System;

namespace QueueNetwork {
	public class Component {
		protected Network Parent {
			get;
			set;
		}
		public String Name {
			get;
			protected set;
		}
	}
}
