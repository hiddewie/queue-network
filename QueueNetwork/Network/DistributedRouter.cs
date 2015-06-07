using System;
using System.Collections.Generic;

namespace QueueNetwork.Network {
	public class DistributedRouter : IRouter {
		private List<ILocation> routeLocations;

		public DistributedRouter (List<ILocation> routeLocations) {
			foreach (ILocation loc in routeLocations) {
				AddRouteLocation (loc);
			}
		}

		public void AddRouteLocation (ILocation routeLocation) {
			routeLocations.Add (routeLocation);
		}
		public List<ILocation> GetRouteLocations () {
			return routeLocations;
		}
	}
}

