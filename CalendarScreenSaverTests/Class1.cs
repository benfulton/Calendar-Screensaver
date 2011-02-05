using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;
using CalendarScreenSaver;

namespace CalendarScreenSaverTests
{
    public class Class1
    {
        [Fact]
        public void foo()
        {
            CalendarController newfoo = new CalendarController(DateTime.Parse("1/1/11"));
            var row1 = newfoo.GetRow(0);
            Assert.Equal("26", row1[0].ToString());
            Assert.Equal("1", row1[6].ToString());

            var row2 = newfoo.GetRow(1);
            Assert.Equal("2", row2[0].ToString());
            Assert.Equal("8", row2[6].ToString());

            Assert.Null(newfoo.GetRow(6));
        }
    }
}
