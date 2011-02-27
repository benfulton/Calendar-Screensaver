using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;
using CalendarScreenSaver;
using System.Drawing;

namespace CalendarScreenSaverTests
{
    public class Class1
    {
        [Fact]
        public void Calendar_rows_are_created_properly()
        {
            CalendarController controller = new CalendarController(new MockService());
            controller.Initialize(new MockView(), DateTime.Parse("1/1/11"));
            var row1 = controller.GetRow(0);
            Assert.Equal( DateTime.Parse("12/26/10"), row1[0]);
            Assert.Equal(DateTime.Parse("1/1/11"), row1[6]);

            var row2 = controller.GetRow(1);
            Assert.Equal(DateTime.Parse("1/2/11"), row2[0]);
            Assert.Equal(DateTime.Parse("1/8/11"), row2[6]);

            Assert.Null(controller.GetRow(6));
        }

        [Fact]
        public void Todays_date_is_colored_differently()
        {
            Color todayColor, yesterdayColor;
            CalendarController controller = new CalendarController(new MockService());
            controller.Initialize(new MockView(), DateTime.Today);
            controller.FormatCell(new DayInfo { date = DateTime.Today }, out todayColor);
            controller.FormatCell(new DayInfo { date = DateTime.Today.AddDays(-1) }, out yesterdayColor);
            Assert.NotEqual(todayColor, yesterdayColor);
        }

        [Fact]
        public void Timer_Firing_Alternates_this_month_and_next_month()
        {
            CalendarController controller = new CalendarController(new MockService());
            MockView view = new MockView();
            controller.Initialize(view, DateTime.Parse("1/1/11"));
            Assert.Contains("January 2011", view._month);
            controller.TimerFired(view);
            Assert.Equal( "February 2011", view._month);
            controller.TimerFired(view);
            Assert.Contains("January 2011", view._month);
        }

        public class MockService : ICalendarService
        {
            #region ICalendarService Members

            public Google.GData.Calendar.EventFeed QueryData(DateTime start, DateTime end)
            {
                return new Google.GData.Calendar.EventFeed(null,null);
            }

            public Google.GData.Calendar.EventFeed Next(Google.GData.Calendar.EventFeed calFeed)
            {
                return null;
            }

            #endregion
        }

        public class MockView : ICalendarView
        {
            public string _month;

            #region ICalendarView Members

            public void Clear()
            {
            }

            public void SetMonth(string month)
            {
                _month = month;
            }

            public void SetDate(int row, int col, DayInfo info)
            {
            }

            public void AddEvent(DateTime date, string text, bool isAllDay)
            {
                throw new NotImplementedException();
            }

            public void DimCell(int row, int column)
            {
                throw new NotImplementedException();
            }

            public void StartTimer()
            {
            }

            #endregion
        }

    }
}
