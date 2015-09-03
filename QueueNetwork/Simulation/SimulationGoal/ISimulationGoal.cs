using System;

namespace QueueNetwork {
	public interface ISimulationGoal {
		bool Finished ();

		bool WarmedUp ();

		void OnSinkArrive (object sender, EventArgs e);
	}
}

