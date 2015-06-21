using System;

namespace QueueNetwork {
	public abstract class Source : Component {

		private Router router {
			get;
			set;
		}

		public Source () {
		}
		public Source (Router router) {
			this.router = router;
		}

		public abstract Unit GenerateUnit ();
	}
}

