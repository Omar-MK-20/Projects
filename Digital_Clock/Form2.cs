using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Digital_Clock
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();

            double JulianDay = instance.ConvertToJulian(DateTime.Now, timeZone);
            double SunDeclination = instance.SunDeclination(JulianDay);
            double EquationOfTime = instance.EquationOfTime(JulianDay);
            double TransitTime = instance.TransitTime(timeZone, longitude, EquationOfTime);
            double[] SunAltitude = instance.SunAltitude(SunDeclination, latitude, elevation, prayerMethod, factorOfShadow);
            double[] HourAngel = instance.HourAngle(SunAltitude, SunDeclination, latitude);

            timeFajr.Text = instance.Fajr(TransitTime, HourAngel);
            timeSun.Text = instance.Sunrise(TransitTime, HourAngel);
            timeZuhr.Text = instance.Zuhr(TransitTime, HourAngel);
            timeAsr.Text = instance.Asr(TransitTime, HourAngel);
            timeMaghreb.Text = instance.Maghreb(TransitTime, HourAngel);
            timeIshaa.Text = instance.Ishaa(TransitTime, HourAngel);

        }

        double latitude = 31.199898; // in decimal degree
        double longitude = 30.065008; // in decimal degree
        double elevation = 36; // in meter
        double timeZone = 2; // in decimal degree
        int factorOfShadow = 1;
        double[] prayerMethod = { 19.5, 17.5 };


        PrayerTimesCalc instance = new PrayerTimesCalc(31.1999, 30.065, 36, 2, 1, prayerMethod:{prayerMethod; });



    }
}
