# DayViewCalendar

This project is migrated from https://www.codeproject.com/Articles/12201/Calendar-DayView-Control

## Introduction
This article describes creating a day view control for visualizing schedule functions in required applications. I have cloned the Outlook day view appearance for similar use. Here is a screenshot:

Office 12 Theme:

Office XP Theme

## Background
Before writing this control, I was in need of a day view control that just looks like that in Outlook. I have found some commercial toolkits but none of them meets my requirements. Some of those want that all appointments be given before showing the control, some of those are not open source, etc. So I wrote this control in a "hurry development", and I think other people can use it. Pay back time for using the CodeProject :)

## What we have?

You can create your appointment class to hold special information (other than start, end dates and title).
You don't need to read all appointments from the DB or something like that.
You can specify how much days will be shown.
You can colorize appointments to show different views.

* In-place editing.
* Drag drop operations.
* No Win32 API.
* Theme based rendering.

By the way, it's compiled under the final release of .NET 2.0. If you don't have it, you need to recompile the project.

## Using the code

This control uses a class named "Appointment" to visualize the view. The DayView control doesn't pay attention to saving appointments to the DB or fetching them. So, you need to write your own DB logic and answer the events.

The control implements these events to interact with the hosting application:

dayView1.NewAppointment
dayView1.ResolveAppointments
dayView1.SelectionChanged
The sample application uses a list collection as the container for appointments. You may use a cached DB source too.

### NewAppointment event

This is raised when the user wants to create an appointment. Event arguments contain start date, end date, and title values of the new appointment. You can create your appointment class that inherits from the DayView.Appointment base class.

The sample application just creates a new appointment, and adds it to the list collection.

      void dayView1_NewAppointment(object sender, NewAppointmentEventArgs args)
      {
          Appointment m_Appointment = new Appointment();
          m_Appointment.StartDate = args.StartDate;
          m_Appointment.EndDate = args.EndDate;
          m_Appointment.Title = args.Title;
          m_Appointments.Add(m_Appointment);
      }

### ResolveAppointments event

This event is raised when the DayView control needs to show an appointment on a date. Event arguments contain the start date and end date of the required range of dates.

The sample application scans the list collection for a specified date range. You can fetch them from your own DB too.

    private void dayView1_ResolveAppointments(object sender, 
                          ResolveAppointmentsEventArgs args)
    {
        List<Appointment> m_Apps = new List<Appointment>();
    
        foreach (Appointment m_App in m_Appointments)
            if ((m_App.StartDate >= args.StartDate) && 
                (m_App.StartDate <= args.EndDate))
                m_Apps.Add(m_App);
    
        args.Appointments = m_Apps;
    }

### Selection Changed event

This event is raised when the user selects an appointment.

    private void dayView1_SelectionChanged(object sender, EventArgs e)
    {
        label3.Text = dayView1.SelectionStart.ToString() + 
                      ":" + dayView1.SelectionEnd.ToString();
    }

The sample application shows the start date and the end date of the selected appointment in a label.
