using System;

namespace QueueNetwork {
	public class Sink : Component, IArriving {
		public event EventHandler PreArrive;
		public event EventHandler PostArrive;

		public int Arrived {
			get;
			protected set;
		}

		public Sink () {
			Arrived = 0;
		}

		public void CallPreArrive (ArriveEvent eventArgs) {
			if (PreArrive != null) {
				PreArrive (this, eventArgs);
			}
		}
		public void CallPostArrive (ArriveEvent eventArgs) {
			if (PostArrive != null) {
				PostArrive (this, eventArgs);
			}
		}

		public void Arrive(Unit unit, Component source) {
			CallPreArrive (new ArriveEvent());
			unit.SystemDepartTime = Clock.GetTime ();
			unit.Sink = this;
			Arrived++;
			Console.WriteLine (String.Format(unit + " has arrived in sink at time {0}. The unit traveled from {1} to {2}", Clock.GetTime(), unit.Source, unit.Sink));
			CallPostArrive (new ArriveEvent());
		}
	}
}

