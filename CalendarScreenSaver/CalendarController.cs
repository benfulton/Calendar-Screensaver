using System;
using System.Collections.Generic;
using System.Linq;
using System.Drawing;
using CalendarScreenSaver.Properties;
using Google.GData.Calendar;

namespace CalendarScreenSaver
{
    public interface ICalendarView
    {
        void SetMonth(string month);
        void SetDate(int row, int col, DayInfo info);
        void AddEvent(DateTime date, string text, bool isAllDay);
        void DimCell(int row, int column);
        void StartTimer();
    }

    public class CalendarController
    {
        private DateTime _DateMonth;
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

        public void Initialize(ICalendarView cal, DateTime dateMonth)
        {
            _DateMonth = dateMonth;

            var firstDay = new DateTime(_DateMonth.Year, _DateMonth.Month, 1);

            var firstDayOfWeek = LastSunday(firstDay);

            for (int i = 0; i < 6; i++)
            {
                for (int j = 0; j < 7; j++)
                {
                    cal.SetDate(i, j, new DayInfo { date = firstDayOfWeek.AddDays(i * 7 + j), eventList = new List<string>() });
                }
            }

            cal.SetMonth(firstDay.ToString("MMMM yyyy"));

            RefreshFeed(cal);
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

        public void RefreshFeed(ICalendarView calendar)
        {
            var calFeed = _Service.QueryData( _DateMonth.AddDays(-28), _DateMonth.AddMonths(1));

            // now populate the calendar
            while (calFeed != null && calFeed.Entries.Count > 0)
            {
                var items = calFeed.Entries.Cast<EventEntry>()
                    .SelectMany(entry => entry.Times, (e, t) => new { e.Title.Text, t.StartTime, t.AllDay })
                    .OrderBy(w => w.StartTime);

                foreach (var item in items)
                {
                    calendar.AddEvent(item.StartTime, item.Text, item.AllDay);
                }

                calFeed = _Service.Next(calFeed);
            }

            calendar.StartTimer();
        }

        public void TimerFired(ICalendarView calendar)
        {
            Initialize(calendar, DateTime.Today.AddMonths(1));
            RefreshFeed(calendar);
        }

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
