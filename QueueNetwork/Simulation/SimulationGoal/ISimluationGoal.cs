using System;

namespace QueueNetwork.Simulation {
	public interface ISimluationGoal {
		bool Finished ();
		bool WarmedUp ();
	}
}

