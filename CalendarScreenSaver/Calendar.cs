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
            lvDay.Bounds = gridCalendar.Bounds;

            _Controller = new CalendarController(new CalendarService(new Settings()));
            _Controller.Initialize(this, DateTime.Today);

            gridCalendar.ClearSelection();

#if DEBUG
            timer1.Interval = 10000;
#endif
        }

        public void Clear()
        {
            lvDay.Clear();
            gridCalendar.Rows.Clear();
            gridCalendar.Rows.Add(6);
            foreach (DataGridViewRow row in gridCalendar.Rows)
            {
                row.Height = 110;
            }
        }
        public void SetTitle(string month)
        {
            lblMonthName.Text = month;
        }

        public CalendarMode Mode
        {
            get
            {
                return gridCalendar.Visible ? CalendarMode.Month : CalendarMode.Day;
            }
            set
            {
                gridCalendar.Visible = value == CalendarMode.Month;
                lvDay.Visible = value == CalendarMode.Day;
            }
        }

        public void SetDate( int row, int col, DayInfo info)
        {
            gridCalendar.Rows[row].Cells[col].Value = info;
        }

        public void AddAgendaItem(DateTime dateTime, string text, bool isAllDay)
        {
            lvDay.Items.Add( GetLineItem(dateTime, text, isAllDay));
        }

        private static string GetLineItem(DateTime date, string text, bool isAllDay)
        {
            string formatted = isAllDay ? "" : date.ToShortTimeString() + ": ";
            string lineItem = formatted + text;
            return lineItem;
        }

        public void AddEvent(DateTime date, string text, bool isAllDay)
        {
            var firstDay = gridCalendar.Rows[0].Cells[0].Value as DayInfo;
            int index = (int)(date.Date - firstDay.date).TotalDays;
            if (index >= 0 && index < 41)
            {
                var info = gridCalendar.Rows[index / 7].Cells[index % 7].Value as DayInfo;
                string lineItem = GetLineItem(date, text, isAllDay);
                info.eventList.Add(lineItem);
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
            if (info != null)
            {
                e.Value = _Controller.FormatCell(info, out c);
                e.FormattingApplied = true;
                e.CellStyle.ForeColor = c;
                e.CellStyle.WrapMode = DataGridViewTriState.True;
            }
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
