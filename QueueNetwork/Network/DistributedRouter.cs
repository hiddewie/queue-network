using System;
using System.Collections.Generic;
using QueueNetwork.Simulation.Distibution;

namespace QueueNetwork {
	public class DistributedRouter : Router {
		private List<Location> RouteLocations;
		private UniformDistribution Distribution = new UniformDistribution (0, 1);

		private Unit currentUnit;

		public DistributedRouter (List<Location> routeLocations) {
			foreach (Location loc in routeLocations) {
				AddRouteLocation (loc);
			}
		}

		public void AddRouteLocation (Location routeLocation) {
			RouteLocations.Add (routeLocation);
		}
		public List<Location> GetRouteLocations () {
			return RouteLocations;
		}

		public override void Depart () {
			if (RouteLocations.Count == 0) {
				throw new Exception ("No route locations, but receiving unit");
			}
			double x = Distribution.NextDouble ();
			double p = 1.0 / RouteLocations.Count;
			double soFar = p;
			int index = 0;
			while (soFar < x) {
				index++;
				soFar += p;
			}
			RouteLocations [index].Arrive (currentUnit);
			currentUnit = null;
		}
	}
}

