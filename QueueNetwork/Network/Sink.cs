using System;

namespace QueueNetwork {
	public class Sink : Component, IArriving {
		public event EventHandler PreArrive;
		public event EventHandler PostArrive;

		private int arrived = 0;

		public Sink () {
		}

		public void CallPreArrive (ArriveEventArgs eventArgs) {
			if (PreArrive != null) {
				PreArrive (this, eventArgs);
			}
		}
		public void CallPostArrive (ArriveEventArgs eventArgs) {
			if (PostArrive != null) {
				PostArrive (this, eventArgs);
			}
		}

		public void Arrive(Unit unit) {
			CallPreArrive (new ArriveEventArgs());
			unit.SystemDepartTime = Clock.GetTime ();
			unit.Sink = this;
			arrived++;
			Console.WriteLine (String.Format(unit + " has arrived in sink at time {0}. The unit traveled from {1} to {2}", Clock.GetTime(), unit.Source, unit.Sink));
			CallPostArrive (new ArriveEventArgs());
		}

		public int Arrived () {
			return arrived;
		}
	}
}

