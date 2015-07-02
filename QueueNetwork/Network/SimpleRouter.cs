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

		public override void Trigger (Event e) {
			if (e is DepartEvent) {
				CallPreEvent (new DepartEvent ());
				RouteLocation.Arrive (currentUnit);
				CallPostEvent (new DepartEvent ());

				return;
			}

			throw new UnknownEventException ();
		}
	}
}

