using System;
using System.Collections.Generic;
using QueueNetwork;

namespace QueueNetwork {
	public class DistributedRouter : Router {
		protected List<IArriving> RouteLocations = new List<IArriving> ();
		protected IDistribution<int> distribution;

		public DistributedRouter (IEnumerable<IArriving> routeLocations, IDistribution<int> distribution) {
			RouteLocations.AddRange(routeLocations);
			this.distribution = distribution;
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

				CallPreEvent (new DepartEvent (this, to));
				Unit tempUnit = currentUnit;
				currentUnit = null;
				CallPostEvent (new DepartEvent (this, to));
				to.Arrive (tempUnit, this);
				return;
			}
			throw new UnknownEventException ();
		}
	}
}

