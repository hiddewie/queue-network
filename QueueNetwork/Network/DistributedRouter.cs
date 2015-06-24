using System;
using System.Collections.Generic;
using QueueNetwork.Distibution;

namespace QueueNetwork {
	public class DistributedRouter : Router {
		private List<IArriving> RouteLocations = new List<IArriving>();
		private UniformDiscreteDistribution distribution;

		public DistributedRouter (List<IArriving> routeLocations) {
			foreach (Location loc in routeLocations) {
				AddRouteLocation (loc);
			}
			distribution = new UniformDiscreteDistribution (0, RouteLocations.Count - 1);
		}

		public void AddRouteLocation (Location routeLocation) {
			RouteLocations.Add (routeLocation);
			distribution = new UniformDiscreteDistribution (0, RouteLocations.Count - 1);
		}
		public List<IArriving> GetRouteLocations () {
			return RouteLocations;
		}

		public override void Depart () {
			if (RouteLocations.Count == 0) {
				throw new Exception ("No route locations, but receiving unit");
			}
			RouteLocations [distribution.NextRandom()].Arrive (currentUnit);
			currentUnit = null;
		}
	}
}

