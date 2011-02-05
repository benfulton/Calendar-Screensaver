using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using Google.GData.Calendar;
using Google.GData.Extensions;
using CalendarScreenSaver.Properties;

namespace CalendarScreenSaver
{
    public partial class Calendar : Form
    {
        private CalendarController _Controller;

        public Calendar(Rectangle bounds)
        {
            InitializeComponent();

            this.Bounds = bounds;

            gridCalendar.Rows.Add(6);
            foreach (DataGridViewRow row in gridCalendar.Rows)
            {
                row.Height = 110;
            }

            _Controller = new CalendarController(DateTime.Today);
            _Controller.Initialize(this);

            RefreshFeed();

            gridCalendar.ClearSelection();
        }

        public void SetMonth(string month)
        {
            lblMonthName.Text = month;
        }

        public void SetDate( int row, int col, DayInfo info)
        {
            gridCalendar.Rows[row].Cells[col].Value = info;
        }

        public void AddEvent(DateTime date, string text, bool isAllDay)
        {
            var firstDay = gridCalendar.Rows[0].Cells[0].Value as DayInfo;
            int index = (int)(date.Date - firstDay.date).TotalDays;
            if (index < 41)
            {
                var info = gridCalendar.Rows[index / 7].Cells[index % 7].Value as DayInfo;
                string formatted = isAllDay ? "" : date.ToShortTimeString() + ": ";
                info.eventList.Add(formatted + text);
            }
        }

        public void DimCell(int row, int column)
        {
            gridCalendar.Rows[row].Cells[column].Style.ForeColor = Color.Gray;
        }

        private void gridCalendar_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            Color c;
            var info = e.Value as DayInfo;
            e.Value = _Controller.FormatCell(info, out c);
            e.FormattingApplied = true;
            e.CellStyle.ForeColor = c;
            e.CellStyle.WrapMode = DataGridViewTriState.True;
        }

        ArrayList entryList;

        private void RefreshFeed()
        {
            var settings = new Settings();
            string calendarURI = settings.CalendarURI;
            string userName = settings.Username;
            string passWord = settings.Password;

            this.entryList = new ArrayList(50);
            ArrayList dates = new ArrayList(50);
            EventQuery query = new EventQuery();
           // query.SingleEvents = true;
            CalendarService service = new CalendarService("CalendarScreenSaver");

            if (userName != null && userName.Length > 0)
            {
                service.setUserCredentials(userName, passWord);
            }

            // only get event's for today - 1 month until today + 1 year

            query.Uri = new Uri(calendarURI);

            query.StartTime = DateTime.Now.AddDays(-28);
            query.EndTime = DateTime.Now.AddMonths(1);


            EventFeed calFeed = service.Query(query) as EventFeed;

            // now populate the calendar
            while (calFeed != null && calFeed.Entries.Count > 0)
            {
                var items = calFeed.Entries.Cast<EventEntry>()
                    .SelectMany(entry => entry.Times, (e, t) => new { e.Title.Text, t.StartTime, t.AllDay })
                    .OrderBy(w => w.StartTime);

                foreach (var item in items)
                {
                    AddEvent(item.StartTime, item.Text, item.AllDay);                    
                }
                    
                // just query the same query again.
                if (calFeed.NextChunk != null)
                {
                    query.Uri = new Uri(calFeed.NextChunk);
                    calFeed = service.Query(query) as EventFeed;
                }
                else
                    calFeed = null;
            }
        }

        private void Calendar_Load(object sender, EventArgs e)
        {
            Cursor.Hide();
            TopMost = true;
        }

        #region ScreenSaver close commands
        private Point mouseLocation;
        private void Calendar_KeyPress(object sender, KeyPressEventArgs e)
        {
            Application.Exit();

        }

        private void Calendar_MouseClick(object sender, MouseEventArgs e)
        {
            Application.Exit();

        }

        private void Calendar_MouseMove(object sender, MouseEventArgs e)
        {
            if (!mouseLocation.IsEmpty)
            {
                // Terminate if mouse is moved a significant distance
                if (Math.Abs(mouseLocation.X - e.X) > 5 ||
                    Math.Abs(mouseLocation.Y - e.Y) > 5)
                    Application.Exit();
            }
            // Update current mouse location
            mouseLocation = e.Location;

        }

        #endregion


    }

    public class DayInfo
    {
        public DateTime date;
        public List<string> eventList;
    }
}
