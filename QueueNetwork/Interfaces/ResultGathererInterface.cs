using System;
using System.Collections.Generic;

namespace QueueNetwork {
	public interface ResultGathererInterface {
		void Initialize();
		void Gather(double simTime);
		List<SimulationResult> GetResults();
		Interval<SimulationResult> GetConfidenceInterval(double confidenceIntervalPercentage);
	}
}

