using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.Drawing;

namespace CalendarScreenSaver
{
    public class CalendarController
    {
        private DateTime _DateMonth;
        public CalendarController(DateTime dateMonth)
        {
            _DateMonth = dateMonth;

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

        public void Initialize(Calendar cal)
        {
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

        }

        public string FormatCell(DayInfo info, out Color color)
        {
            if (_DateMonth.Month == info.date.Month)
                color = Color.LawnGreen;
            else
                color = Color.Gray;

            return info.date.Day.ToString() + Environment.NewLine + string.Join(Environment.NewLine, info.eventList);
        }


    }
}
