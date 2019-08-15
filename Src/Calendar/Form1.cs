using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Calendar
{
    public partial class Form1 : Form
    {
        List<Appointment> m_Appointments;

        public Form1()
        {
            InitializeComponent();

            m_Appointments = new List<Appointment>();

            DateTime m_Date = DateTime.Now;

            m_Date = m_Date.AddHours(10 - m_Date.Hour);
            m_Date = m_Date.AddMinutes(-m_Date.Minute);

            Appointment m_Appointment = new Appointment();

            m_Appointment.StartDate = m_Date;
            m_Appointment.EndDate = m_Date.AddMinutes(10);
            m_Appointment.Title = "test1\r\nmultiline";

            m_Appointments.Add(m_Appointment);

            m_Date = m_Date.AddDays(1);

            m_Appointment = new Appointment();
            m_Appointment.StartDate = m_Date.AddHours(2);
            m_Appointment.EndDate = m_Date.AddHours(3);
            m_Appointment.Title = "test2\r\n locked one";
            m_Appointment.Color = System.Drawing.Color.LightBlue;
            m_Appointment.Locked = true;

            m_Appointments.Add(m_Appointment);

            m_Date = m_Date.AddDays(-1);

            m_Appointment = new Appointment();
            m_Appointment.StartDate = m_Date;
            m_Appointment.EndDate = m_Date.AddHours(4);
            m_Appointment.EndDate = m_Appointment.EndDate.AddMinutes(15);
            m_Appointment.Color = System.Drawing.Color.Yellow;
            m_Appointment.Title = "test3\r\n some numbers 123456 and unicode chars (Russian) –усский текст and (Turkish) рьёЁцз÷зiЁ";

            m_Appointments.Add(m_Appointment);

            m_Appointment = new Appointment();
            m_Appointment.StartDate = m_Date;
            m_Appointment.EndDate = m_Date.AddDays(2);
            m_Appointment.Title = "More than one day";
            m_Appointment.AllDayEvent = true;
            m_Appointment.Color = System.Drawing.Color.Red;

            m_Appointments.Add(m_Appointment);

            m_Appointment = new Appointment();
            m_Appointment.StartDate = m_Date.AddDays(2);
            m_Appointment.EndDate = m_Date.AddDays(4);
            m_Appointment.Title = "More than one day (2)";
            m_Appointment.AllDayEvent = true;
            m_Appointment.Color = System.Drawing.Color.Coral;

            m_Appointments.Add(m_Appointment);

            m_Appointment = new Appointment();
            m_Appointment.StartDate = m_Date;
            m_Appointment.EndDate = m_Date.AddDays(4);
            m_Appointment.Title = "More than one day (3)";
            m_Appointment.AllDayEvent = true;
            m_Appointment.Color = System.Drawing.Color.Red;

            m_Appointments.Add(m_Appointment);

            dayView1.StartDate = DateTime.Now;
            dayView1.NewAppointment += new NewAppointmentEventHandler(dayView1_NewAppointment);
            dayView1.SelectionChanged += new EventHandler(dayView1_SelectionChanged);
            dayView1.ResolveAppointments += new Calendar.ResolveAppointmentsEventHandler(this.dayView1_ResolveAppointments);

            dayView1.MouseMove += new System.Windows.Forms.MouseEventHandler(this.dayView1_MouseMove);

            comboBox1.SelectedIndex = 1;
        }

        void dayView1_NewAppointment(object sender, NewAppointmentEventArgs args)
        {
            Appointment m_Appointment = new Appointment();

            m_Appointment.StartDate = args.StartDate;
            m_Appointment.EndDate = args.EndDate;
            m_Appointment.Title = args.Title;
            m_Appointment.Group = "2";

            m_Appointments.Add(m_Appointment);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //dayView1.DaysToShow = int.Parse( textBox1.Text );
        }

        private void dayView1_MouseMove(object sender, MouseEventArgs e)
        {
            label2.Text = dayView1.GetTimeAt(e.X, e.Y).ToString();
        }

        private void dayView1_SelectionChanged(object sender, EventArgs e)
        {
            if (dayView1.Selection == SelectionType.DateRange)
            {
                label3.Text = dayView1.SelectionStart.ToString() + ":" + dayView1.SelectionEnd.ToString();
            }
            else if (dayView1.Selection == SelectionType.Appointment)
            {
                label3.Text = dayView1.SelectedAppointment.StartDate.ToString() + ":" + dayView1.SelectedAppointment.EndDate.ToString();
            }
        }

        private void dayView1_ResolveAppointments(object sender, ResolveAppointmentsEventArgs args)
        {
            List<Appointment> m_Apps = new List<Appointment>();

            foreach (Appointment m_App in m_Appointments)
                if ((m_App.StartDate >= args.StartDate) &&
                    (m_App.StartDate <= args.EndDate))
                    m_Apps.Add(m_App);

            args.Appointments = m_Apps;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Appointment m_App = new Appointment();
            m_App.StartDate = dayView1.SelectionStart;
            m_App.EndDate = dayView1.SelectionEnd;
            m_App.BorderColor = Color.Red;

            m_Appointments.Add(m_App);

            dayView1.Invalidate();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button6_Click(object sender, EventArgs e)
        {
            dayView1.DaysToShow = 1;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            dayView1.DaysToShow = 3;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            dayView1.DaysToShow = 5;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            dayView1.DaysToShow = 7;
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox1.SelectedItem.ToString() == "Office 11")
            {
                dayView1.Renderer = new Office11Renderer();
            }
            else
            {
                dayView1.Renderer = new Office12Renderer();
            }
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            if (dayView1.SelectedAppointment != null)
            {
                colorDialog1.Color = dayView1.SelectedAppointment.Color;

                if (colorDialog1.ShowDialog(this) == DialogResult.OK)
                {
                    dayView1.SelectedAppointment.Color = colorDialog1.Color;
                }
            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            if (dayView1.SelectedAppointment != null)
            {
                colorDialog1.Color = dayView1.SelectedAppointment.BorderColor;

                if (colorDialog1.ShowDialog(this) == DialogResult.OK)
                {
                    dayView1.SelectedAppointment.BorderColor = colorDialog1.Color;
                }
            }
        }

        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            dayView1.HalfHourHeight = trackBar1.Value;
        }

        private void monthCalendar1_DateChanged(object sender, DateRangeEventArgs e)
        {
            dayView1.StartDate = monthCalendar1.SelectionStart;
        }

        private void button8_Click(object sender, EventArgs e)
        {
            dayView1.AllowScroll = !dayView1.AllowScroll;
        }

    }
}