using System;
using System.Collections.Generic;

namespace QueueNetwork {
	public class Network : ITimed {
		private List<Component> components = new List<Component>();
		private List<ITimed> timedComponents = new List<ITimed>();

		public void Add(Component component) {
			components.Add (component);
			if (component is ITimed) {
				ITimed timedComponent = component as ITimed;
				timedComponents.Add (timedComponent);
			}
		}

		public double NextDeparture () {
			double departure = Constants.INF;
			foreach (ITimed c in timedComponents) {
				if (c.NextDeparture() < departure) {
					departure = c.NextDeparture ();
				}
			}
			return departure;
		}
	}
}

