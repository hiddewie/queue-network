using System;
using System.Collections.Generic;

namespace QueueNetwork.Network {
	public class DistributedRouter : IRouter {
		private List<ILocation> routeLocations;

		public DistributedRouter (List<ILocation> routeLocation) {
			setRouteLocation (routeLocation);
		}
		public DistributedRouter (List<ILocation> routeLocations) {
			foreach (ILocation loc in routeLocations) {
				addRouteLocation (loc);
			}
		}

		public void addRouteLocation (ILocation routeLocation) {

		}
		public List<ILocation> getRouteLocations () {
			return routeLocations;
		}
	}
}

