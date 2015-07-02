using System;
using System.Collections.Generic;

namespace QueueNetwork {
	public class Network : ITimed {
		public event EventHandler PreEvent;
		public event EventHandler PostEvent;

		private List<Component> components = new List<Component>();
		private List<ITimed> timedComponents = new List<ITimed>();
		private List<Sink> sinks = new List<Sink>();
		private List<Source> sources = new List<Source>();

		private double networkEventTime = Constants.INF;
		private ITimed nextEventComponent;
		private Event nextEvent;

		public void CallPreEvent (Event eventArgs) {
			if (PreEvent != null) {
				PreEvent (this, eventArgs);
			}
		}
		public void CallPostEvent (Event eventArgs) {
			if (PostEvent != null) {
				PostEvent (this, eventArgs);
			}
		}

		public void Add(Component component) {
			component.Parent = this;

			components.Add (component);
			if (component is ITimed) {
				timedComponents.Add (component as ITimed);
			}
			if (component is Sink) {
				sinks.Add (component as Sink);
			}
			if (component is Source) {
				sources.Add (component as Source);
			}
		}

		public Dictionary<Event, double> NextEvents () {
			networkEventTime = Constants.INF;
			foreach (ITimed c in timedComponents) {
				foreach (KeyValuePair<Event, double> entry in c.NextEvents()) {
					if (entry.Value < networkEventTime) {
						networkEventTime = entry.Value;
						nextEvent = entry.Key;
						nextEventComponent = c;
					}
				}
			}
			return new Dictionary<Event, double> {
				{new NetworkUpdateEvent(), networkEventTime}
			};
		}

		public void Trigger (Event e) {
			if (e is NetworkUpdateEvent) {
				// TODO: Update 
				return;
			}

			throw new UnknownEventException ();
		}

		public List<Source> GetSources () {
			return sources;
		}

		public List<Sink> GetSinks () {
			return sinks;
		}
	}
}

