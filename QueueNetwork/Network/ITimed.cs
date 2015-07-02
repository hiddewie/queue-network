using System;
using System.Collections.Generic;

namespace QueueNetwork {
	public interface ITimed {
		event EventHandler PreEvent;
		event EventHandler PostEvent;
		void CallPreEvent (Event eventArgs);
		void CallPostEvent (Event eventArgs);

		Dictionary<Event, double> NextEvents();
		void Trigger(Event e);
	}
}

