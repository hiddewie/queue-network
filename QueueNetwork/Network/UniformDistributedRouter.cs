using System;
using System.Collections.Generic;
using QueueNetwork;

namespace QueueNetwork {
	public class UniformDistributedRouter : DistributedRouter {
		public UniformDistributedRouter (List<IArriving> routeLocations) : base(routeLocations, new UniformDiscreteDistribution(0, routeLocations.Count - 1)) {
		}

		public void AddRouteLocation (Location routeLocation) {
			RouteLocations.Add (routeLocation);
			distribution = new UniformDiscreteDistribution (0, RouteLocations.Count - 1);
		}
	}
}

