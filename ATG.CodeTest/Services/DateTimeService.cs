using System;
using System.Collections.Generic;
using System.Text;

namespace ATG.CodeTest.Services
{
    public class DateTimeService : IDateTimeService
    {
        public DateTime Now => DateTime.Now;
    }
}
