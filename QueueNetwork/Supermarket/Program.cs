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

			// Create a network
			Network network = new Network ();
			// Create a source with an exp(1.0) distribution
			Source source = new DistributionSource (new ExponentialDistribution (1.0));
			network.Add (source);

			// There are 6 sinks, with different speeds
			int numSinks = 6;
			float[] queueSpeed = new float[]{ 1.0F, 1.0F, 2.0F, 2.0F, 5.0F, 10.0F };
			List<QueueLocation> queues = new List<QueueLocation> ();
			List<Sink> sinks = new List<Sink> ();

			// Simulate 500 arrivals
			int arrived = 0;
			int numArrivals = 500;
			for (int i = 0; i < numSinks; i++) {
				// Create a sink, give it a name and add it to the network
				Sink sink = new Sink ();
				sink.Name = "Sink " + i;
				sinks.Add (sink);

				// Add a queue and set the depart location to its sink
				QueueLocation queue = new QueueLocation (new ExponentialDistribution (queueSpeed [i]));
				queue.DepartLocation = sink;

				// Arrive event of a unit
				sink.PostArrive += (object sender, EventArgs e) => {
					Console.WriteLine (sink.Name + ": " + sink.Arrived);
					arrived++;

					Console.WriteLine ("Total arrived: " + arrived);

					if (arrived == numArrivals) {
						for (int j = 0; j < numSinks; j++) {
							Console.WriteLine ("Sink " + j + ", arrived: " + sinks [j].Arrived);
						}
					}
				};

				network.Add (sink);
				network.Add (queue);

				queues.Add (queue);
			}

			// Create a router which routes based on a discrete distribution. =
			DistributedRouter router = new DistributedRouter (queues, new DiscreteDistribution (new double[] {
				1.5 / numSinks,
				1.5 / numSinks,
				1.0 / numSinks,
				1.0 / numSinks,
				0.5 / numSinks,
				0.5 / numSinks
			}));
			// Set the source depart location to the router
			source.DepartLocation = router;
			network.Add (router);

			// Create the simulation
			NetworkSimulation sim = new NetworkSimulation (network);

			// Create a (non-functional) result gatherer
			ResultGatherer resultGatherer = new ResultGatherer ();
			// Simulate the simulation with network
			sim.Simulate (new ReplicationMethod (resultGatherer, numArrivals, 1));
			// Get the results
			List<SimulationResult> results = resultGatherer.GetResults ();
			Interval<SimulationResult> confidenceInterval = resultGatherer.GetConfidenceInterval (0.95);

			Console.WriteLine ();
			Console.WriteLine ("--- Done ---");
			Console.ReadKey ();
		}
	}
}
