using System;

namespace RemindMe
{
	public class TimesCalculator
	{
		#region Fields

		//Adjusting Methods for Higher Latitudes
		const int NONE = 0;				//No adjustment
		const int MID_NIGHT = 1;		//middle of night
		const int ONE_SEVENTH = 2;		//1/7th of night
		const int ANGLE_BASED = 3;		//angle/60th of night

		const int TIME_24 = 0;
		const int TIME_12 = 1;

		const string INVALID_TIME = "--:--";

		private int dhuhrMinutes = 0;	//minutes after mid-day for Dhuhr
		private int adjustHighLats = 1;	//adjusting method for higher latitudes

		private DateTime dateTimeNow;
		private int timeFormat = 1;

		private double lat = 30.0588;
		private double lng = 31.2268;
		private int timeZone;
		private double julianDate;

		private int numIterations = 1;	//number of iterations needed to compute times

		private int method;

		private double[,] methodParams =
		{
			{ 19.5, 1, 0, 0, 17.5 },	//Egyptian General Authority of Survey
			{ 18.5, 1, 0, 1, 90 },		//Umm al-Qura, Makkah
			{ 15, 1, 0, 0, 15 },		//Islamic Society of North America (ISNA)
			{ 18, 1, 0, 0, 17 },		//Muslim World League (MWL)
			{ 18, 1, 0, 0, 18 }			//University of Islamic Sciences, Karachi
		};

		#endregion

		public int TimeFormat
		{
			set => timeFormat = value;
		}

		public void AddDay()
		{
			dateTimeNow = dateTimeNow.AddDays(1);
		}

		public TimesCalculator(DateTime dateTimeNow, double lat, double lng, int timeFormat, int method)
		{
			this.dateTimeNow = dateTimeNow.Add(TimeSpan.Zero);
			this.lat = lat;
			this.lng = lng;
			timeZone = TimeZone.CurrentTimeZone.GetUtcOffset(dateTimeNow).Hours;
			this.timeFormat = timeFormat;
			this.method = method;
		}

		#region Methods

		private string FloatToTime24(double time)
		{
			if (time < 0)
				return INVALID_TIME;
			time = FixHour(time + 0.5 / 60);	//add 0.5 minutes to round
			double hours = Math.Floor(time);
			double minutes = Math.Floor((time - hours) * 60);
			return string.Format("{0:00}:{1:00}", (int)hours, (int)minutes);
		}

		private string FloatToTime12(double time)
		{
			if (time < 0)
				return INVALID_TIME;
			time = FixHour(time + 0.5 / 60);	//add 0.5 minutes to round
			double hours = Math.Floor(time);
			double minutes = Math.Floor((time - hours) * 60);
			string suffix = hours >= 12 ? "PM" : "AM";
			hours = (hours + 12 - 1) % 12 + 1;
			return string.Format("{0:00}:{1:00} {2}", (int)hours, (int)minutes, suffix);
		}

		public string[] GetTimes()
		{
			julianDate = JulianDate(dateTimeNow.Year, dateTimeNow.Month, dateTimeNow.Day) - lng / (15 * 24);

			string[] times = ComputeDayTimes();
			return new string[] { times[0], times[1], times[2], times[3], times[4], times[6] };
		}

		//compute declination angle of sun and equation of time
		private double[] SunPosition(double jd)
		{
			double D = jd - 2451545.0;
			double g = FixAngle(357.529 + 0.98560028 * D);
			double q = FixAngle(280.459 + 0.98564736 * D);
			double L = FixAngle(q + 1.915 * dsin(g) + 0.020 * dsin(2 * g));

			double R = 1.00014 - 0.01671 * dcos(g) - 0.00014 * dcos(2 * g);
			double e = 23.439 - 0.00000036 * D;

			double d = darcsin(dsin(e) * dsin(L));
			double RA = darctan2(dcos(e) * dsin(L), dcos(L)) / 15;
			RA = FixHour(RA);
			double EqT = q / 15 - RA;

			return new double[] { d, EqT };
		}

		//compute equation of time
		private double EquationOfTime(double jd)
		{
			return SunPosition(jd)[1];
		}

		//compute declination angle of sun
		private double SunDeclination(double jd)
		{
			return SunPosition(jd)[0];
		}

		//compute mid-day (Dhuhr, Zawal) time
		private double ComputeMidDay(double t)
		{
			double T = EquationOfTime(julianDate + t);
			double Z = FixHour(12 - T);
			return Z;
		}

		//compute time for a given angle G
		private double ComputeTime(double G, double t)
		{
			//System.out.println("G: "+G);

			double D = SunDeclination(julianDate + t);
			double Z = ComputeMidDay(t);
			double V = ((double)1 / 15) * darccos((-dsin(G) - dsin(D) * dsin(lat)) /
					(dcos(D) * dcos(lat)));
			return Z + (G > 90 ? -V : V);
		}

		//compute the time of Asr
		private double ComputeAsr(int step, double t)	//Shafii: step=1, Hanafi: step=2
		{
			double D = SunDeclination(julianDate + t);
			double G = -darccot(step + dtan(Math.Abs(lat - D)));
			return ComputeTime(G, t);
		}

		//compute prayer times at given julian date
		private double[] ComputeTimes(double[] times)
		{
			double[] t = DayPortion(times);

			double Fajr = ComputeTime(180 - methodParams[method, 0], t[0]);
			double Sunrise = ComputeTime(180 - 0.833, t[1]);
			double Dhuhr = ComputeMidDay(t[2]);
			double Asr = ComputeAsr(1, t[3]);
			double Sunset = ComputeTime(0.833, t[4]); ;
			double Maghrib = ComputeTime(methodParams[method, 2], t[5]);
			double Isha = ComputeTime(methodParams[method, 4], t[6]);

			return new double[] { Fajr, Sunrise, Dhuhr, Asr, Sunset, Maghrib, Isha };
		}

		//adjust Fajr, Isha and Maghrib for locations in higher latitudes
		private double[] AdjustHighLatTimes(double[] times)
		{
			double nightTime = GetTimeDifference(times[4], times[1]);	//sunset to sunrise

			//Adjust Fajr
			double FajrDiff = NightPortion(methodParams[method, 0]) * nightTime;
			if (GetTimeDifference(times[0], times[1]) > FajrDiff)
				times[0] = times[1] - FajrDiff;

			//Adjust Isha
			double IshaAngle = (methodParams[method, 3] == 0) ? methodParams[method, 4] : 18;
			double IshaDiff = NightPortion(IshaAngle) * nightTime;
			if (GetTimeDifference(times[4], times[6]) > IshaDiff)
				times[6] = times[4] + IshaDiff;

			//Adjust Maghrib
			double MaghribAngle = (methodParams[method, 1] == 0) ? methodParams[method, 2] : 4;
			double MaghribDiff = NightPortion(MaghribAngle) * nightTime;
			if (GetTimeDifference(times[4], times[5]) > MaghribDiff)
				times[5] = times[4] + MaghribDiff;

			return times;
		}

		//the night portion used for adjusting times in higher latitudes
		private double NightPortion(double angle)
		{
			double val = 0;
			if (adjustHighLats == ANGLE_BASED)
				val = 1.0 / 60.0 * angle;
			if (adjustHighLats == MID_NIGHT)
				val = 1.0 / 2.0;
			if (adjustHighLats == ONE_SEVENTH)
				val = 1.0 / 7.0;

			return val;
		}

		private double[] DayPortion(double[] times)
		{
			for (int i = 0; i < times.Length; i++)
			{
				times[i] /= 24;
			}
			return times;
		}

		//compute prayer times at given julian date
		private string[] ComputeDayTimes()
		{
			double[] times = { 5, 6, 12, 13, 18, 18, 18 };	//default times

			for (int i = 0; i < numIterations; i++)
			{
				times = ComputeTimes(times);
			}

			times = AdjustTimes(times);
			return AdjustTimesFormat(times);
		}


		//adjust times in a prayer time array
		private double[] AdjustTimes(double[] times)
		{
			for (int i = 0; i < 7; i++)
			{
				times[i] += timeZone - lng / 15;
			}
			times[2] += dhuhrMinutes / 60;		//Dhuhr
			if (methodParams[method, 1] == 1)	//Maghrib
				times[5] = times[4] + methodParams[method, 2] / 60.0;
			if (methodParams[method, 3] == 1)	//Isha
				times[6] = times[5] + methodParams[method, 4] / 60.0;

			if (adjustHighLats != NONE)
			{
				times = AdjustHighLatTimes(times);
			}

			return times;
		}

		private string[] AdjustTimesFormat(double[] times)
		{
			string[] formatted = new string[times.Length];

			for (int i = 0; i < 7; i++)
			{
				if (timeFormat == TIME_12)
					formatted[i] = FloatToTime12(times[i]);
				else
					formatted[i] = FloatToTime24(times[i]);
			}
			return formatted;
		}

		//compute the difference between two times
		private double GetTimeDifference(double c1, double c2)
		{
			double diff = FixHour(c2 - c1); ;
			return diff;
		}

		//calculate julian date from a calendar date
		private double JulianDate(int year, int month, int day)
		{
			if (month <= 2)
			{
				year -= 1;
				month += 12;
			}
			double A = (double)Math.Floor(year / 100.0);
			double B = 2 - A + Math.Floor(A / 4);

			double JD = Math.Floor(365.25 * (year + 4716)) + Math.Floor(30.6001 * (month + 1)) + day + B - 1524.5;
			return JD;
		}

		//detect daylight saving in a given date
		private bool UseDayLightSaving(int year, int month, int day)
		{
			return TimeZone.CurrentTimeZone.IsDaylightSavingTime(new DateTime(year, month, day));
		}

		//degree sin
		private double dsin(double d)
		{
			return Math.Sin(DegreeToRadian(d));
		}

		//degree cos
		private double dcos(double d)
		{
			return Math.Cos(DegreeToRadian(d));
		}

		//degree tan
		private double dtan(double d)
		{
			return Math.Tan(DegreeToRadian(d));
		}

		//degree arcsin
		private double darcsin(double x)
		{
			return RadianToDegree(Math.Asin(x));
		}

		//degree arccos
		private double darccos(double x)
		{
			return RadianToDegree(Math.Acos(x));
		}

		//degree arctan
		private double darctan(double x)
		{
			return RadianToDegree(Math.Atan(x));
		}

		//degree arctan2
		private double darctan2(double y, double x)
		{
			return RadianToDegree(Math.Atan2(y, x));
		}

		//degree arccot
		private double darccot(double x)
		{
			return RadianToDegree(Math.Atan(1 / x));
		}

		//Radian to Degree
		private double RadianToDegree(double radian)
		{
			return (radian * 180.0) / Math.PI;
		}

		//degree to radian
		private double DegreeToRadian(double degree)
		{
			return (degree * Math.PI) / 180.0;
		}

		private double FixAngle(double angel)
		{
			angel = angel - 360.0 * (Math.Floor(angel / 360.0));
			angel = angel < 0 ? angel + 360.0 : angel;
			return angel;
		}

		//range reduce hours to 0..23
		private double FixHour(double hour)
		{
			hour = hour - (24.0 * Math.Floor(hour / 24.0));
			hour = hour < 0 ? hour + 24.0 : hour;
			return hour;
		}
		#endregion
	}
}
