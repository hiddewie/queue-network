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
			Console.WriteLine(String.Format("Departing from SimpleRouter at time {0}", Clock.GetTime()));
			RouteLocation.Arrive (currentUnit);
		}
	}
}

