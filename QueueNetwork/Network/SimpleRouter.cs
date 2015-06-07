using System;

namespace QueueNetwork.Network {
	public class SimpleRouter : IRouter {

		private ILocation routeLocation;

		public SimpleRouter (ILocation routeLocation) {
			SetRouteLocation (routeLocation);
		}

		public ILocation GetRouteLocation () {
			return routeLocation;
		}

		public void SetRouteLocation (IRouter routeLocation) {
			this.routeLocation = routeLocation;
		}
	}
}

