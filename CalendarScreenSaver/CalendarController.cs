using System;
using System.Collections.Generic;
using System.Linq;
using System.Drawing;
using CalendarScreenSaver.Properties;
using Google.GData.Calendar;

namespace CalendarScreenSaver
{
    public enum CalendarMode { Month, Day };

    public interface ICalendarView
    {
        void Clear();
        void SetTitle(string title);
        void AddEvent(DateTime date, string text, bool isAllDay);
        void SetDate(int i, int j, DayInfo info);
        void DimCell(int row, int column);
        void StartTimer();
        CalendarMode Mode { get; set; }

        void AddAgendaItem(DateTime dateTime, string text, bool isAllDay);
    }

    public class CalendarController
    {
        private DateTime _DateMonth;
        private DateTime _InitialDate;
        private ICalendarService _Service;
        public CalendarController(ICalendarService service)
        {
            _Service = service;
        }

        DateTime LastSunday(DateTime date)
        {
            return date.AddDays(0 - date.DayOfWeek);
        }

        public DateTime[] GetRow(int row)
        {
            var firstDay = new DateTime(_DateMonth.Year, _DateMonth.Month, 1);

            var firstDayOfWeek = LastSunday(firstDay).AddDays(row * 7);

            if (firstDayOfWeek.Month == firstDay.AddMonths(1).Month)
                return null;

            return Enumerable.Range(0, 7).Select(i => firstDayOfWeek.AddDays(i)/*.Day.ToString()*/).ToArray();
        }

        private void Refresh(ICalendarView cal, CalendarData data)
        {
            _DateMonth = data.date;
            cal.Mode = data.mode;

            var firstDay = new DateTime(_DateMonth.Year, _DateMonth.Month, 1);


            cal.Clear();

            if (data.mode == CalendarMode.Month)
            {
                var firstDayOfWeek = LastSunday(firstDay);

                for (int i = 0; i < 6; i++)
                {
                    for (int j = 0; j < 7; j++)
                    {
                        cal.SetDate(i, j, new DayInfo { date = firstDayOfWeek.AddDays(i * 7 + j) });
                    }
                }

                cal.SetTitle(firstDay.ToString("MMMM yyyy"));
            }
            else if (_DateMonth == _InitialDate)
            {
                cal.SetTitle("Today's Agenda");
            }
            else
            {
                cal.SetTitle("Tomorrow's Agenda");
            }
            RefreshFeed(cal, data);
        }

        public void Initialize(ICalendarView cal, DateTime dateMonth)
        {
            _InitialDate = dateMonth;
            Refresh(cal, new CalendarData( dateMonth, CalendarMode.Month));
        }

        public string FormatCell(DayInfo info, out Color color)
        {
            if (info.date.Date == DateTime.Today)
                color = Color.Salmon;
            else if (_DateMonth.Month == info.date.Month)
                color = Color.LawnGreen;
            else
                color = Color.Gray;

            return info.date.Day.ToString() + Environment.NewLine + string.Join(Environment.NewLine, info.eventList);
        }

        public void RefreshFeed(ICalendarView calendar, CalendarData data)
        {
            EventFeed calFeed;
            if (data.mode == CalendarMode.Month)
                calFeed = _Service.QueryData( _DateMonth.AddDays(-28), _DateMonth.AddMonths(1));
            else
                calFeed = _Service.QueryData(data.date, data.date.AddDays(1));

            if (calFeed == null || calFeed.Entries.Count == 0)
                calendar.AddAgendaItem(_DateMonth, "No agenda items", true);
            
            // now populate the calendar
            while (calFeed != null && calFeed.Entries.Count > 0)
            {
                var items = calFeed.Entries.Cast<EventEntry>()
                    .SelectMany(entry => entry.Times, (e, t) => new { e.Title.Text, t.StartTime, t.AllDay })
                    .OrderBy(w => w.StartTime);

                foreach (var item in items)
                {
                    if (data.mode == CalendarMode.Month)
                        calendar.AddEvent(item.StartTime, item.Text, item.AllDay);
                    else
                        calendar.AddAgendaItem(item.StartTime, item.Text, item.AllDay);
                }

                calFeed = _Service.Next(calFeed);
            }

            calendar.StartTimer();
        }

        public void TimerFired(ICalendarView calendar)
        {
            CalendarData data;
            if (calendar.Mode == CalendarMode.Month && _DateMonth.Month == _InitialDate.Month)
                data = new CalendarData( _InitialDate.AddMonths(1), CalendarMode.Month);
            else if (calendar.Mode == CalendarMode.Month)
                data = new CalendarData( _InitialDate, CalendarMode.Day);
            else if (calendar.Mode == CalendarMode.Day && _DateMonth == _InitialDate)
                data = new CalendarData( _InitialDate.AddDays(1), CalendarMode.Day);
            else
                data = new CalendarData( _InitialDate, CalendarMode.Month);
 
            Refresh(calendar, data);
            
        }

    }

    public struct CalendarData
    {
        public CalendarData ( DateTime dt, CalendarMode md)
	{
            date = dt;
            mode = md;
	}
        public readonly DateTime date;
        public readonly CalendarMode mode;
    }



    public interface ICalendarService
    {
        EventFeed QueryData(DateTime start, DateTime end);
        EventFeed Next(EventFeed calFeed);
    }

    internal class CalendarService : ICalendarService
    {
        private EventQuery _Query;
        private string _Uri;
        private Google.GData.Calendar.CalendarService _Service = new Google.GData.Calendar.CalendarService("CalendarScreenSaver");

        public CalendarService(Settings settings)
        {
            string userName = settings.Username;
            string passWord = settings.Password;

            if (userName != null && userName.Length > 0)
            {
                _Service.setUserCredentials(userName, passWord);
            }

            _Uri = settings.CalendarURI;
        }

        public EventFeed QueryData(DateTime start, DateTime end)
        {
            _Query = new EventQuery();
            _Query.Uri = new Uri(_Uri);

            _Query.StartTime = start;
            _Query.EndTime = end;

            return _Service.Query(_Query) as EventFeed;
        }

        public EventFeed Next(EventFeed calFeed)
        {
            // just query the same query again.
            if (calFeed.NextChunk != null)
            {
                _Query.Uri = new Uri(calFeed.NextChunk);
                calFeed = _Service.Query(_Query) as EventFeed;
            }
            else
                calFeed = null;

            return calFeed;
        }
    }
}
