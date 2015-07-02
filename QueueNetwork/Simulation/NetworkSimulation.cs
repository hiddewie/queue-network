using System;
using System.Collections.Generic;

using QueueNetwork;
using QueueNetwork.Simulation.Result;
using QueueNetwork.Simulation.Method;

namespace QueueNetwork.Simulation {
	public class NetworkSimulation {
		private Network network;

		public NetworkSimulation (Network network) {
			this.network = network;
		}

		public void Simulate (SimulationMethod simulationMethod) {
			List<Sink> sinks = network.GetSinks ();
			foreach (Sink sink in sinks) {
				sink.PostArrive += simulationMethod.Goal.OnSinkArrive;
			}

			KeyValuePair<Event, double> nextEvent;
			do {
				nextEvent = DetermineNextEvent(network.NextEvents());
				Clock.AdvanceTo(nextEvent.Value);
				network.Trigger(nextEvent.Key);
			} while (!simulationMethod.Goal.Finished ());
		}

		private KeyValuePair<Event, double> DetermineNextEvent(Dictionary<Event, double> events) {
			if (events.Count == 0) {
				throw new Exception ("Cannot deptermine next event of empty event list");
			}
			KeyValuePair<Event, double> ret = new KeyValuePair<Event, double>(null, Constants.INF);
			foreach (KeyValuePair<Event, double> item in events) {
				if (item.Value < ret.Value) {
					ret = item;
				}
			}
			if (ret.Value == Constants.INF) {
				throw new Exception ("Infinite event found");
			}
			return ret;
		}
	}
}

