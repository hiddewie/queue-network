using System;
using System.Collections.Generic;
using QueueNetwork;

namespace QueueNetwork {
	public class ResultGatherer : IResultGatherer {

		/*public void OnSinkArrive (object sender, A e) {
			//Console.WriteLine(e.
		}*/

		public List<SimulationResult> GetResults() {
			return new List<SimulationResult> ();
		}

		public Interval<SimulationResult> GetConfidenceInterval(double confidencePercentage) {
			return new Interval<SimulationResult> (new SimulationResult(), new SimulationResult() );
		}
	}
	public static class EntryPoint {
		
		public static void Main () {
			Network network = new Network ();
			Source source = new DistributionSource (new ExponentialDistribution(1.0));
			Sink sink = new Sink ();
			QueueLocation queue = new QueueLocation (new ExponentialDistribution(3.0));
			queue.PostArrive += (object sender, EventArgs e) => Console.WriteLine(String.Format("Post arrive {0}", queue.HasUnits()));
			queue.DepartLocation = sink;
			source.DepartLocation = queue;

			sink.PreArrive += (object sender, EventArgs e) => Console.WriteLine(sink.Arrived);

			network.Add (source);
			network.Add (sink);
			network.Add (queue);

			ResultGatherer resultGatherer = new ResultGatherer ();

			//sink.PostArrive += resultGatherer.OnSinkArrive;

			NetworkSimulation sim = new NetworkSimulation (network);
			sim.Simulate(new ReplicationMethod (resultGatherer, 15, 3));
			List<SimulationResult> results = resultGatherer.GetResults ();
			Interval<SimulationResult> confidenceInterval = resultGatherer.GetConfidenceInterval (0.95);

			Console.WriteLine ();
			Console.WriteLine ("--- Done ---");
			Console.ReadKey ();
		}
	}
}
