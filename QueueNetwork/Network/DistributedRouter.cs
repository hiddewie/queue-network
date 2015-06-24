using System;
using System.Collections.Generic;
using QueueNetwork.Distibution;

namespace QueueNetwork {
	public class DistributedRouter : Router {
		private List<IArriving> RouteLocations = new List<IArriving>();
		private UniformDistribution Distribution = new UniformDistribution (0, 1);

		public DistributedRouter (List<IArriving> routeLocations) {
			foreach (Location loc in routeLocations) {
				AddRouteLocation (loc);
			}
		}

		public void AddRouteLocation (Location routeLocation) {
			RouteLocations.Add (routeLocation);
		}
		public List<IArriving> GetRouteLocations () {
			return RouteLocations;
		}

		public override void Depart () {
			if (RouteLocations.Count == 0) {
				throw new Exception ("No route locations, but receiving unit");
			}
			double x = Distribution.NextRandom ();
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

