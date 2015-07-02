using System;
using System.Collections.Generic;
using QueueNetwork;
using QueueNetwork.Distibution;
using QueueNetwork.Simulation;
using QueueNetwork.Simulation.Result;
using QueueNetwork.Simulation.Method;

namespace QueueNetwork {
	public class ResultGatherer : IResultGatherer {
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
			Source source = new PoissonSource (1.0);
			Sink sink = new Sink ();
			QueueLocation queue = new QueueLocation (new ExponentialDistribution(3.0));
			queue.DepartLocation = sink;
			source.DepartLocation = queue;

			queue.PreArrive += (object sender, EventArgs e) => Console.WriteLine("QUEUE TEST@!");

			network.Add (source);
			network.Add (sink);

			ResultGatherer resultGatherer = new ResultGatherer ();

			NetworkSimulation sim = new NetworkSimulation (network);
			sim.Simulate(new ReplicationMethod (resultGatherer, 100, 10));
			List<SimulationResult> results = resultGatherer.GetResults ();
			Interval<SimulationResult> confidenceInterval = resultGatherer.GetConfidenceInterval (0.95);

			Console.WriteLine ();
			Console.WriteLine ("--- Done ---");
			Console.ReadKey ();
		}
	}
}
