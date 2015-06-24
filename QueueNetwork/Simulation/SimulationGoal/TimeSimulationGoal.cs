using System;

namespace QueueNetwork.Simulation {
	public class TimeSimulationGoal : ISimluationGoal {
		private double time, warmUp;
		
		public TimeSimulationGoal (double time, double warmUp) {
			this.time = time;
			this.warmUp = warmUp;
		}

		public bool Finished () {
			return Clock.GetTime() >= time + warmUp;
		}

		public bool WarmedUp () {
			return Clock.GetTime() >= warmUp;
		}
	}
}

