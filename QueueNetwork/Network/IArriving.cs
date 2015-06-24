using System;

namespace QueueNetwork {
	public interface IArriving {
		event EventHandler PreArrive;
		event EventHandler PostArrive;
		void CallPreArrive (ArriveEventArgs eventArgs);
		void CallPostArrive (ArriveEventArgs eventArgs);
		void Arrive (Unit unit);
	}
}

