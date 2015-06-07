using System;

namespace QueueNetwork.Network {
	public class SimpleRouter : IRouter {

		private ILocation routeLocation;

		public SimpleRouter (ILocation routeLocation) {
			setRouteLocation (routeLocation);
		}

		public ILocation getRouteLocation () {
			return routeLocation;
		}

		public void setRouteLocation (IRouter routeLocation) {
			this.routeLocation = routeLocation;
		}
	}
}

