using System;

namespace QueueNetwork.Simulation {
	public class NumberSimulationGoal : ISimluationGoal {
		private int units, warmUp;
		private int count = 0;

		public NumberSimulationGoal (int units, int warmUp) {
			this.units = units;
			this.warmUp = warmUp;
		}

		public bool Finished() {
			return count >= units + warmUp;
		}

		public bool WarmedUp() {
			return count >= warmUp;
		}
	}
}

