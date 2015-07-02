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

		public override void Trigger (Trigger t) {
			if (t is DepartTrigger) {
				if (RouteLocations.Count == 0) {
					throw new Exception ("No route locations, but receiving unit");
				}
				IArriving to = RouteLocations [distribution.NextRandom ()];

				CallPreEvent (new DepartEvent(this, to));
				Unit tempUnit = currentUnit;
				currentUnit = null;
				CallPostEvent (new DepartEvent(this, to));
				to.Arrive (tempUnit, this);
				return;
			}
			throw new UnknownEventException ();
		}
	}
}

