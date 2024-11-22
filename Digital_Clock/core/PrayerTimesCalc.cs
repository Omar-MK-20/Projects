using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Digital_Clock
{


    public class SunAltitude
    {
        private string name;
        private double fajrAltitude;
        private double ishaaAltitude;

        public SunAltitude(string name, double fajrAltitude, double ishaaAltitude)
        {
            this.name = name;
            this.fajrAltitude = fajrAltitude;
            this.ishaaAltitude = ishaaAltitude;
        }


        public void setName(string name)
        {
            this.name = name;
        }

        public string getName()
        {
            return this.name;
        }

        public void setFajr(double x)
        {
            this.fajrAltitude = x;
        }

        public double getFajr()
        {
            return this.fajrAltitude;
        }

        public void setIshaa(double x)
        {
            this.ishaaAltitude = x;
        }

        public double getIshaa()
        {
            return this.ishaaAltitude;
        }

    }

    public class PrayerTimesCalc
    {
        // used in this calculation "https://radhifadlillah.com/blog/2020-09-06-calculating-prayer-times/#elevation-of-target-location"


        // Requirements
        double latitude; // in decimal degree
        double longitude; // in decimal degree
        double elevation; // in meter
        double timeZone; // in decimal degree
        int factorOfShadow; // 1 for shafii  or  2 for hanafi.

        public double[] eygptMethod = { 19.5, 17.5 };   //Egyptian General Authority of Survey
        public double[] ummAlqura = { 18.5, 90 };       //Umm al-Qura, Makkah
        public double[] karachi = { 18, 18 };           //University of Islamic Sciences, Karachi
        public double[] prayerMethod;

        const int TIME_24 = 0;
        const int TIME_12 = 1;
        private int timeFormat = 1;
        const string INVALID_TIME = "--:--";



        public PrayerTimesCalc(double lat, double lon, double elevation, double timeZone, int factorOfShadow, double[] prayerMethod)
        {
            this.latitude = lat;
            this.longitude = lon;
            this.elevation = elevation;
            this.timeZone = timeZone;
            this.factorOfShadow = factorOfShadow;
            this.prayerMethod = prayerMethod;
        }

        

        #region Functions.

        // ACOT is acronym of arcus cotangent which is inverse of cotangent function.
        public static double Acot(double x)
        {
            return Math.PI / 2 - Math.Atan(x);
        }

        //degree to radian
        private double DegreeToRadian(double degree)
        {
            return (degree * Math.PI) / 180.0;
        }

        //Radian to Degree
        private double RadianToDegree(double radian)
        {
            return (radian * 180.0) / Math.PI;
        }

        //range reduce hours to 0..23
        private double FixHour(double hour)
        {
            hour = hour - (24.0 * Math.Floor(hour / 24.0));
            hour = hour < 0 ? hour + 24.0 : hour;
            return hour;
        }

        // convert double to timespan
        public string convertor(double time)
        {
            if (time < 0)
                return INVALID_TIME;
            time = FixHour(time + 0.5 / 60);    //add 0.5 minutes to round
            double hours = Math.Floor(time);
            double minutes = Math.Floor((time - hours) * 60);
            return string.Format("{0:00}:{1:00}", (int)hours, (int)minutes);

        }

        private string FloatToTime12(double time)
        {
            if (time < 0)
                return INVALID_TIME;
            time = FixHour(time + 0.5 / 60);    //add 0.5 minutes to round
            double hours = Math.Floor(time);
            double minutes = Math.Floor((time - hours) * 60);
            string suffix = hours >= 12 ? "PM" : "AM";
            hours = (hours + 12 - 1) % 12 + 1;
            return string.Format("{0:00}:{1:00} {2}", (int)hours, (int)minutes, suffix);
        }

        private string FloatToTime24(double time)
        {
            if (time < 0)
                return INVALID_TIME;
            time = FixHour(time + 0.5 / 60);    //add 0.5 minutes to round
            double hours = Math.Floor(time);
            double minutes = Math.Floor((time - hours) * 60);
            return string.Format("{0:00}:{1:00}", (int)hours, (int)minutes);
        }

        // Adjust Times Format
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

        #endregion


        #region Prayer Calculators.

        public double ConvertToJulian(DateTime dateTimeNow, double timeZone)
        {
            /* DateTimeOffset dateTimeOffset = new DateTimeOffset(dateTimeNow);
            double timeZoneOffset = dateTimeOffset.Offset.TotalHours;
            

            // If the timezone offset is negative, convert it to positive.
            if (timeZoneOffset < 0)
            {
                timeZoneOffset = -timeZoneOffset;
            }*/
            double timeZoneDouble = timeZone / 24;

            int year = dateTimeNow.Year;
            int month = dateTimeNow.Month;
            int day = dateTimeNow.Day;
            int hour = dateTimeNow.Hour;
            int minute = dateTimeNow.Minute;
            int second = dateTimeNow.Second;

            double julianDay = day - 32075 + 1461 * (year + 4800 + (month - 14) / 12) / 4 + 367 * (month - 2 - (month - 14) / 12 * 12) / 12 - 3 * ((year + 4900 + (month - 14) / 12) / 100) / 4;

            julianDay += (hour - timeZoneDouble) / 24;
            julianDay += (minute + second / 60) / (24 * 60);

            return julianDay;
        }

        /*
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

        //degree to radian
        private double DegreeToRadian(double degree)
        {
            return (degree * Math.PI) / 180.0;
        }

        //Radian to Degree
        private double RadianToDegree(double radian)
        {
            return (radian * 180.0) / Math.PI;
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

        //degree arcsin
        private double darcsin(double x)
        {
            return RadianToDegree(Math.Asin(x));
        }

        //degree arctan2
        private double darctan2(double y, double x)
        {
            return RadianToDegree(Math.Atan2(y, x));
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

            double SunDeclination = darcsin(dsin(e) * dsin(L));
            double RA = darctan2(dcos(e) * dsin(L), dcos(L)) / 15;
            RA = FixHour(RA);
            double EquationOfTime = q / 15 - RA;

            return new double[] { SunDeclination, EquationOfTime };
        }

        //compute declination angle of sun
        private double SunDeclination(double jd)
        {
            return SunPosition(jd)[0];
        }

        //compute equation of time
        private double EquationOfTime(double jd)
        {
            return SunPosition(jd)[1];
        }*/

        // calculate Sun Declination.
        public double SunDeclination(double julianDay)
        {
            double T = 2 * Math.PI * (julianDay - 2451545) / 365.25;
            double D1 = Math.Sin(DegreeToRadian(57.297 * T - 79.547));
            double D2 = Math.Sin(DegreeToRadian(2 * 57.297 * T - 82.682));
            double D3 = Math.Sin(DegreeToRadian(3 * 57.297 * T - 59.722));

            double Delta = 0.37877 + (23.264 * D1)
                + (0.3812 * D2)
                + (0.17132 * D3);
            return Delta;
        }

        // Calculate equation of time.
        public double EquationOfTime(double julianDay)
        {
            double U = (julianDay - 2451545) / 36525; // in degrees.
            double L0 = 280.46607 + 36000.7698 * U; // in degrees.
            double ET1000 = -(1789 + 237 * U) * Math.Sin(DegreeToRadian(L0))
                - (7146 - 62 * U) * Math.Cos(DegreeToRadian(L0))
                + (9934 - 14 * U) * Math.Sin(DegreeToRadian(2 * L0))
                - (29 + 5 * U) * Math.Cos(DegreeToRadian(2 * L0))
                + (74 + 10 * U) * Math.Sin(DegreeToRadian(3 * L0))
                + (320 - 4 * U) * Math.Cos(DegreeToRadian(3 * L0))
                - 212 * Math.Sin(DegreeToRadian(4 * L0));
            double ET = ET1000 / 1000; // in minutes.
            return ET;
        }

        // Calculate transit time.
        public double TransitTime(double timeZone, double Long, double EquationOfTime)
        {
            double TT = 12 + timeZone - (Long / 15) - (EquationOfTime / 60);
            return TT; // in hours
        }

        // Calculate Sun altitude for each prayer times
        public double[] SunAltitude(double SunDeclination, double lat, double elevation, double[] prayerMethod, double FactorOfShadow)
        {
            double SA_ASR_before = (FactorOfShadow + Math.Tan(DegreeToRadian(Math.Abs(SunDeclination - lat))));

            double SA_FAJR = -(prayerMethod[0]);
            double SA_SUNRISE = -0.8333 - (0.0347 * Math.Sqrt(elevation));
            double SA_ASR = RadianToDegree(Acot(SA_ASR_before));
            double SA_MAGHRIB = SA_SUNRISE;
            double SA_ISHA = -(prayerMethod[1]);

            double[] SA = { SA_FAJR, SA_SUNRISE, SA_ASR, SA_MAGHRIB, SA_ISHA };
            return SA;
        }

        // calculating hour angle.
        public double[] HourAngle(double[] SunAltitude, double SunDeclination, double lat)
        {
            double[] COS_HA = new double[SunAltitude.Length];
            double[] HA = new double[SunAltitude.Length];
            for (int i = 0; i < SunAltitude.Length; i++) 
            {
                // Console.WriteLine(i);
                COS_HA[i] = (Math.Sin(DegreeToRadian(SunAltitude[i])) - (Math.Sin(DegreeToRadian(lat)) * Math.Sin(DegreeToRadian(SunDeclination)))) / (Math.Cos(DegreeToRadian(lat)) * Math.Cos(DegreeToRadian(SunDeclination)));
                // Console.WriteLine(COS_HA[i]);
                HA[i] = RadianToDegree(Math.Acos((COS_HA[i]))) ;
                // Console.WriteLine(HA[i]);
                // Console.WriteLine();
            }

            return HA;
        }

        // Calculate prayer times.
        public double[] PrayerTimes(double TransitTime, double[] HourAngle)
        {
            double FAJR = TransitTime - HourAngle[0] / 15;
            double SUNRISE = TransitTime - HourAngle[1] / 15;
            double ZUHR = TransitTime; // + (2 / 60)
            double ASR = TransitTime + HourAngle[2] / 15;
            double MAGHRIB = TransitTime + HourAngle[3] / 15;
            double ISHA = TransitTime + HourAngle[4] / 15;

            double[] PT = { FAJR, SUNRISE, ZUHR, ASR, MAGHRIB, ISHA };
            return PT;
        }



        // fajr
        public string Fajr(double TransitTime, double[] HourAngle)
        {
            string F = FloatToTime12(PrayerTimes(TransitTime, HourAngle)[0]);
            return F;
        }

        // sunrise
        public string Sunrise(double TransitTime, double[] HourAngle)
        {
            string S = FloatToTime12(PrayerTimes(TransitTime, HourAngle)[1]);
            return S;
        }

        // zuhr
        public string Zuhr(double TransitTime, double[] HourAngle)
        {
            string Z = FloatToTime12(PrayerTimes(TransitTime, HourAngle)[2]);
            return Z;
        }

        // asr
        public string Asr(double TransitTime, double[] HourAngle)
        {
            string A = FloatToTime12(PrayerTimes(TransitTime, HourAngle)[3]);
            return A;
        }

        // maghreb
        public string Maghreb(double TransitTime, double[] HourAngle)
        {
            string M = FloatToTime12(PrayerTimes(TransitTime, HourAngle)[4]);
            return M;
        }

        // ishaa
        public string Ishaa(double TransitTime, double[] HourAngle)
        {
            string I = FloatToTime12(PrayerTimes(TransitTime, HourAngle)[5]);
            return I;
        }

        #endregion


        #region Instance
        




    }





}
