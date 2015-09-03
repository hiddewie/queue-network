using System;

namespace QueueNetwork {
	public class NumberTimeSimulationGoal : ISimulationGoal {
		private NumberSimulationGoal numberSimulationGoal;
		private TimeSimulationGoal timeSimulationGoal;

		public NumberTimeSimulationGoal (int units, double time) : this (units, 0, time, 0.0) {
			
		}

		public NumberTimeSimulationGoal (int units, int unitsWarmup, double time, double timeWarmup) {
			numberSimulationGoal = new NumberSimulationGoal (units, unitsWarmup);
			timeSimulationGoal = new TimeSimulationGoal (time, timeWarmup);
		}

		public bool WarmedUp () {
			return numberSimulationGoal.WarmedUp () && timeSimulationGoal.WarmedUp ();
		}

		public bool Finished () {
			return numberSimulationGoal.Finished () && timeSimulationGoal.Finished ();
		}

		public void OnSinkArrive (object sender, EventArgs e) {
			numberSimulationGoal.OnSinkArrive (sender, e);
			timeSimulationGoal.OnSinkArrive (sender, e);
		}
	}
}

