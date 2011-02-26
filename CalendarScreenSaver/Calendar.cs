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
    public partial class Calendar : Form, ICalendarView
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

            _Controller = new CalendarController(new CalendarService(new Settings()));
            _Controller.Initialize(this, DateTime.Today);

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
            if (index >= 0 && index < 41)
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

        public void StartTimer()
        {
            timer1.Start();
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

        private void timer1_Tick(object sender, EventArgs e)
        {
            _Controller.TimerFired(this);
        }


    }

    public class DayInfo
    {
        public DateTime date;
        public List<string> eventList = new List<string>();
    }
}
