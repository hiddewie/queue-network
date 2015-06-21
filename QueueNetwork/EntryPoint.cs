using System;

namespace QueueNetwork {
	public static class EntryPoint {
		public static void Main () {
			Console.WriteLine ("Hello World!");

			Network network = new Network ();
			Source source = new PoissonSource (1.0);
			Sink sink = new Sink ();
			source.DepartLocation = sink;

			network.Add (source);
			network.Add (sink);

			Console.WriteLine (network.NextDeparture ());
			source.Depart ();
			Clock.advance (network.NextDeparture ());
			Console.WriteLine (network.NextDeparture ());
			source.Depart ();
			Clock.advance (network.NextDeparture ());
			Console.WriteLine (network.NextDeparture ());
			source.Depart ();

			Console.ReadKey ();
		}
	}
}

