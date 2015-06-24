using System;

namespace QueueNetwork {
	public static class EntryPoint {
		public static void Main () {
			Network network = new Network ();
			Source source = new PoissonSource (1.0);
			Sink sink = new Sink ();
			SimpleRouter router = new SimpleRouter (sink);
			source.DepartLocation = router;

			network.Add (source);
			network.Add (sink);

			Console.WriteLine (network.NextDeparture ());
			Clock.advance (network.NextDeparture ());
			source.Depart ();
			Console.WriteLine (String.Format("Clock: {0}, Next departure: {1}", Clock.GetTime(), network.NextDeparture ()));
			Clock.advance (network.NextDeparture ());
			source.Depart ();
			Console.WriteLine (String.Format("Clock: {0}, Next departure: {1}", Clock.GetTime(), network.NextDeparture ()));
			Clock.advance (network.NextDeparture ());
			source.Depart ();
			Console.WriteLine (String.Format("Clock: {0}, Next departure: {1}", Clock.GetTime(), network.NextDeparture ()));

			Console.WriteLine ();
			Console.WriteLine ("--- Done ---");
			Console.ReadKey ();
		}
	}
}

