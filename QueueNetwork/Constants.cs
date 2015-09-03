using System;

namespace QueueNetwork {
	public class Constants {
		public static readonly double INF = 1.0e30;
		public static readonly double SECOND = 1.0;
		public static readonly double MINUTE = 60.0 * SECOND;
		public static readonly double HOUR = 60 * MINUTE;
		public static readonly double WORK_DAY = 8.0 * HOUR;
		public static readonly double DAY = 24.0 * HOUR;

		public static double Erf (double x) {
			// constants
			double a1 = 0.254829592;
			double a2 = -0.284496736;
			double a3 = 1.421413741;
			double a4 = -1.453152027;
			double a5 = 1.061405429;
			double p = 0.3275911;

			// Save the sign of x
			int sign = 1;
			if (x < 0)
				sign = -1;
			x = Math.Abs (x);

			// A&S formula 7.1.26
			double t = 1.0 / (1.0 + p * x);
			double y = 1.0 - (((((a5 * t + a4) * t) + a3) * t + a2) * t + a1) * t * Math.Exp (-x * x);

			return sign * y;
		}

		public static string FormatTime (double time) {
			if (time < 0) {
				return "-" + FormatTime (-time);
			}
			double hours = (time % Constants.DAY) / Constants.HOUR;
			double minutes = (hours * 60.0) % 60.0;
			double seconds = (minutes * 60.0) % 60.0;
			return string.Format ("{0:00}:{1:00}:{2:00}", (int)hours, (int)minutes, (int)seconds);
		}
	}
}

