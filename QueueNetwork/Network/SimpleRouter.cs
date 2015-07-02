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

		public override void Trigger (Trigger t) {
			if (t is DepartTrigger) {
				CallPreEvent (new DepartEvent (this, RouteLocation));
				CallPostEvent (new DepartEvent (this, RouteLocation));
				RouteLocation.Arrive (currentUnit, this);

				return;
			}

			throw new UnknownEventException ();
		}
	}
}

