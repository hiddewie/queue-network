using System;
using System.Collections.Generic;

namespace QueueNetwork {
	public interface IResultGatherer {
		List<SimulationResult> GetResults ();

		Interval<SimulationResult> GetConfidenceInterval (double confidenceIntervalPercentage);
	}
}

