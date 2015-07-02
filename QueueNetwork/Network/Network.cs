using System;
using System.Collections.Generic;

namespace QueueNetwork {
	public class Network : Component, ITimed {
		public event EventHandler PreEvent;
		public event EventHandler PostEvent;

		private List<Component> components = new List<Component>();
		private List<ITimed> timedComponents = new List<ITimed>();
		private List<Sink> sinks = new List<Sink>();
		private List<Source> sources = new List<Source>();

		private double networkEventTime = Constants.INF;

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

		public Dictionary<Trigger, double> NextTriggers () {
			networkEventTime = Constants.INF;
			ITimed nextComponent = null;
			Trigger nextTrigger = null;


			foreach (ITimed c in timedComponents) {
				foreach (KeyValuePair<Trigger, double> entry in c.NextTriggers()) {
					if (entry.Value < networkEventTime) {
						networkEventTime = entry.Value;
						nextTrigger = entry.Key;
						nextComponent = c;
					}
				}
			}
			return new Dictionary<Trigger, double> {
				{new NetworkUpdateTrigger(nextComponent, nextTrigger), networkEventTime}
			};
		}

		public void Trigger (Trigger t) {
			if (t is NetworkUpdateTrigger) {
				NetworkUpdateTrigger nue = t as NetworkUpdateTrigger;

				CallPreEvent (new NetworkUpdateEvent (nue.OriginalTrigger));
				CallPostEvent (new NetworkUpdateEvent (nue.OriginalTrigger));
				nue.Target.Trigger(nue.OriginalTrigger);

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

