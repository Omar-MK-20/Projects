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
    public partial class Form1 : Form
    {

        public Form1()
        {
            InitializeComponent();

            double JulianDay = instance.ConvertToJulian(DateTime.Now, timeZone);
            double SunDeclination = instance.SunDeclination(JulianDay);
            double EquationOfTime = instance.EquationOfTime(JulianDay);
            double TransitTime = instance.TransitTime(timeZone, longitude, EquationOfTime);
            double[] SunAltitude = instance.SunAltitude(SunDeclination, latitude, elevation, fajrAngel, ishaaAngel, factorOfShadow);
            double[] HourAngel = instance.HourAngle(SunAltitude, SunDeclination, latitude);



        }

        double latitude = 31.199898; // in decimal degree
        double longitude = 30.065008; // in decimal degree
        double elevation = 36; // in meter
        double timeZone = 2; // in decimal degree
        int factorOfShadow = 1;
        double fajrAngel = 20;
        double ishaaAngel = 17.5;

        PrayerTimesCalc instance = new PrayerTimesCalc();


        private void Form1_Load(object sender, EventArgs e)
        {
            timer1.Start();
        }

        /* private void btnChangeFormat_Click(object sender, EventArgs e)
        {
            if (btnChangeFormat.Text == "12H")
            {
                formatAm_Pm = true;
                btnChangeFormat.Text = "24H";
            }
            else
            {
                formatAm_Pm = false;
                btnChangeFormat.Text = "12H";
            }
        }*/

        private void timer1_Tick(object sender, EventArgs e)
        {
            TimeSpan date1 = TimeSpan.Parse(DateTime.Now.ToString("hh:mm:ss"));
            // TimeSpan date2 = TimeSpan.Parse("06:17");
            // TimeSpan date3 = date1 - date2;



            lblTime.Text = date1.ToString();
            lblDay.Text = DateTime.Now.ToString("dddd");
            lblMonth.Text = DateTime.Now.ToString("d-MM-yyyy");
            PrayerTimesCalc julianDateObj = new PrayerTimesCalc();

            // lblJulianDate.Text = julianDateObj.ConvertToJulian(DateTime.Now).ToString();
            // lblJulianDate.Text = TimeZoneInfo.Local.ToString();
            // lblJulianDate.Text = julianDateObj.ConvertToJulian(DateTime.Parse("2009-06-12 12:00:00")).ToString();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnMinimize_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void btnShowBorder_Click(object sender, EventArgs e)
        {
            if (btnShowBorder.Text == "Show Border")
            {
                this.FormBorderStyle = FormBorderStyle.FixedSingle;
                btnShowBorder.Text = "Hide Border";
            }
            else
            {
                this.FormBorderStyle = FormBorderStyle.None;
                btnShowBorder.Text = "Show Border";
            }
        }

        private void lblTime_Click(object sender, EventArgs e)
        {

        }

        private void PrTimes_Click(object sender, EventArgs e)
        {
            Form2 F2 = new Form2();
            F2.ShowDialog();
        }
    }
}
