using System;

namespace QueueNetwork {
	public static class EntryPoint {
		public static void Main () {
			Console.WriteLine ("Hello World!");

			Network network = new Network ();

			Console.WriteLine (network);

			Console.ReadKey ();
		}
	}
}

