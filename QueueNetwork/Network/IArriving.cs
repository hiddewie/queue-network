using System;

namespace QueueNetwork {
	public interface IArriving {
		event EventHandler PreArrive;
		event EventHandler PostArrive;
		void CallPreArrive (ArriveEvent eventArgs);
		void CallPostArrive (ArriveEvent eventArgs);
		void Arrive (Unit unit);
	}
}

