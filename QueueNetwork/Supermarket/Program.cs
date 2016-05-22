using System;
using QueueNetwork;
using System.Collections.Generic;

namespace Supermarket {

	public class ResultGatherer : IResultGatherer {

		public List<SimulationResult> GetResults () {
			return new List<SimulationResult> ();
		}

		public Interval<SimulationResult> GetConfidenceInterval (double confidencePercentage) {
			return new Interval<SimulationResult> (new SimulationResult (), new SimulationResult ());
		}
	}

	public static class MainClass {

		public static void Main () {
			Console.WriteLine ("--- Starting ---");

			Network network = new Network ();
			Source source = new DistributionSource (new ExponentialDistribution (1.0));
			network.Add (source);

			int numSinks = 6;
			float[] sinkSpeed = new float[]{ 1.0F, 1.0F, 2.0F, 2.0F, 5.0F, 10.0F };
			List<QueueLocation> queues = new List<QueueLocation>();
			List<Sink> sinks = new List<Sink>();

			int arrived = 0;
			int numArrivals = 500;
			for (int i = 0; i < numSinks; i++) {
				Sink sink = new Sink ();
				sink.Name = "Sink " + i;
				sinks.Add (sink);

				QueueLocation queue = new QueueLocation (new ExponentialDistribution (sinkSpeed [i]));
//				queue.PostArrive += (object sender, EventArgs e) => Console.WriteLine (String.Format ("Post arrive {0}", queue.HasUnits ()));
				queue.DepartLocation = sink;

				sink.PostArrive += (object sender, EventArgs e) => {
					Console.WriteLine (sink.Name + ": " + sink.Arrived);
					arrived++;

					Console.WriteLine("Total arrived: " + arrived);

					if (arrived == numArrivals) {
						for (int j = 0; j <  numSinks; j++) {
							Console.WriteLine("Sink " + j + ", arrived: " + sinks[j].Arrived);
						}
					}
				};

				network.Add (sink);
				network.Add (queue);

				queues.Add (queue);
			}

			DistributedRouter router = new DistributedRouter(queues, new DiscreteDistribution(new double[] {1.5/numSinks, 1.5/numSinks, 1.0/numSinks, 1.0/numSinks, 0.5/numSinks, 0.5/numSinks}));
			source.DepartLocation = router;
			network.Add (router);

			NetworkSimulation sim = new NetworkSimulation (network);

			ResultGatherer resultGatherer = new ResultGatherer ();
			sim.Simulate (new ReplicationMethod (resultGatherer, numArrivals, 1));
			List<SimulationResult> results = resultGatherer.GetResults ();
			Interval<SimulationResult> confidenceInterval = resultGatherer.GetConfidenceInterval (0.95);

			Console.WriteLine ();
			Console.WriteLine ("--- Done ---");
			Console.ReadKey ();
		}
	}

}
