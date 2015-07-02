using System;
using System.Collections.Generic;
using QueueNetwork.Simulation;
using QueueNetwork.Simulation.Result;

namespace QueueNetwork.Simulation.Method {
	public abstract class SimulationMethod {
		public SimulationMethod(ISimulationGoal goal, IResultGatherer resultGatherer) {
			this.Goal = goal;
			this.ResultGatherer = resultGatherer;
		}

		public ISimulationGoal Goal {
			get;
			set;
		}

		public IResultGatherer ResultGatherer {
			get;
			set;
		}
	}
}

