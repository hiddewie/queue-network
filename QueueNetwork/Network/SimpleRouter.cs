using System;

namespace QueueNetwork {
	public class SimpleRouter : Router {
		public IArriving RouteLocation {
			get;
			set;
		}

		public SimpleRouter (IArriving routeLocation) {
			RouteLocation = routeLocation;
		}

		public override void Depart () {
			CallPreDepart (new DepartEventArgs());
			RouteLocation.Arrive (currentUnit);
			CallPostDepart (new DepartEventArgs());
		}
	}
}

