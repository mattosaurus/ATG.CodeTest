using System;
using System.Collections.Generic;
using System.Text;

namespace ATG.CodeTest.Models.Options
{
    public class FailoverOptions
    {
        public int MaxFailedRequests { get; set; } = 50;

        public bool FailoverModeEnabled { get; set; } = true;

        public int FailoverWindowMinutes { get; set; } = 10;
    }
}
