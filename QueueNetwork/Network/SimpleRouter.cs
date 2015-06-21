using System;

namespace QueueNetwork {
	public class SimpleRouter : Router {

		private Location routeLocation;

		public SimpleRouter (Location routeLocation) {
			SetRouteLocation (routeLocation);
		}

		public Location GetRouteLocation () {
			return routeLocation;
		}

		public void SetRouteLocation (Router routeLocation) {
			this.routeLocation = routeLocation;
		}
	}
}

