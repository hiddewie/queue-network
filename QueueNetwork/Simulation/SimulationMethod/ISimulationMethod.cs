using System;
using System.Collections.Generic;

namespace QueueNetwork.Simulation.SimulationMethod {
	public interface ISimulationMethod {
		double GetFixedTiming();
		bool ContinueSimulation ();
		bool ContinueRun (double simTime, int numCustomersDelayed, int numReplications);
		void RunStarted ();
		void RunFinished (double simTime, int numCustomersDelayed);
	}
}

