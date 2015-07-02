using System;

namespace QueueNetwork.Simulation {
	public interface ISimulationGoal {
		bool Finished ();
		bool WarmedUp ();
		void OnSinkArrive (object sender, EventArgs e);
	}
}

