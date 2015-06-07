using System;

namespace QueueNetwork.Network {
	public abstract class Source : ISource {

		private IRouter router;

		public Source () {
		}
		public Source (IRouter router) {
			SetRouter (router);
		}

		public void SetRouter (IRouter router) {
			this.router = router;
		}

		public abstract IUnit GenerateUnit ();
	}
}

