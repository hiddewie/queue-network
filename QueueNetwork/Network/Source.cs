using System;

namespace QueueNetwork.Network {
	public abstract class Source : ISource {

		private IRouter router;

		public Source () {
		}
		public Source (IRouter router) {
			setRouter (router);
		}

		public void setRouter (IRouter router) {
			this.router = router;
		}

		public abstract IUnit generateUnit ();
	}
}

