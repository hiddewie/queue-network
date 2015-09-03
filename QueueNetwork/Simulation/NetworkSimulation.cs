using System;
using System.Collections.Generic;

using QueueNetwork;

namespace QueueNetwork {
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

			KeyValuePair<Trigger, double> nextTrigger;
			do {
				nextTrigger = DetermineNextTrigger (network.NextTriggers ());
				Clock.AdvanceTo (nextTrigger.Value);
				network.Trigger (nextTrigger.Key);
			} while (!simulationMethod.Goal.Finished ());
		}

		private KeyValuePair<Trigger, double> DetermineNextTrigger (Dictionary<Trigger, double> events) {
			if (events.Count == 0) {
				throw new Exception ("Cannot deptermine next trigger of empty event list");
			}
			KeyValuePair<Trigger, double> ret = new KeyValuePair<Trigger, double> (null, Constants.INF);
			foreach (KeyValuePair<Trigger, double> item in events) {
				if (item.Value < ret.Value) {
					ret = item;
				}
			}
			if (ret.Value == Constants.INF) {
				throw new Exception ("Infinite trigger found");
			}
			return ret;
		}
	}
}

