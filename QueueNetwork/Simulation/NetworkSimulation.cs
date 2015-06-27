using System;

using QueueNetwork;
using QueueNetwork.Simulation.Result;
using QueueNetwork.Simulation.SimulationMethod;

namespace QueueNetwork.Simulation {
	public class NetworkSimulation {
		private Network network;

		public NetworkSimulation (Network network) {
			this.network = network;
		}

		public SimulationResult Simulate (ISimluationGoal goal) {
			
			while (!goal.Finished ()) {
				
			}

			return new SimulationResult ();
		}
	}
}

