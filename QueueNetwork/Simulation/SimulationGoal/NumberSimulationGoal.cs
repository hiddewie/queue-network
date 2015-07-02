using System;

namespace QueueNetwork.Simulation {
	public class NumberSimulationGoal : ISimulationGoal {
		private int units, warmUp;
		private int count = 0;

		public NumberSimulationGoal (int units) : this(units, 0) {
		}
		public NumberSimulationGoal (int units, int warmUp) {
			this.units = units;
			this.warmUp = warmUp;
		}

		public void OnSinkArrive(object sender, EventArgs e) {
			count++;
		}

		public bool Finished() {
			return count >= units + warmUp;
		}

		public bool WarmedUp() {
			return count >= warmUp;
		}
	}
}

