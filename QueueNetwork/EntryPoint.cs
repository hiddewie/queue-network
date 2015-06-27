using System;
using QueueNetwork;
using QueueNetwork.Distibution;
using QueueNetwork.Simulation;
using QueueNetwork.Simulation.Result;

namespace QueueNetwork {
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

			NetworkSimulation sim = new NetworkSimulation (network);
			SimulationResult[] results = sim.Simulate(new NumberSimulationGoal ());

			Console.WriteLine (network.NextDeparture ());
			Clock.advance (network.NextDeparture ());
			source.Depart ();
			queue.Depart ();
			Console.WriteLine (String.Format("Clock: {0}, Next departure: {1}", Clock.GetTime(), network.NextDeparture ()));
			Clock.advance (network.NextDeparture ());
			source.Depart ();
			source.Depart ();
			source.Depart ();
			queue.Depart ();
			Console.WriteLine (String.Format("Clock: {0}, Next departure: {1}", Clock.GetTime(), network.NextDeparture ()));
			Clock.advance (network.NextDeparture ());
			source.Depart ();
			queue.Depart ();
			queue.Depart ();
			Console.WriteLine (String.Format("Clock: {0}, Next departure: {1}", Clock.GetTime(), network.NextDeparture ()));

			Console.WriteLine ();
			Console.WriteLine ("--- Done ---");
			Console.ReadKey ();
		}
	}
}
