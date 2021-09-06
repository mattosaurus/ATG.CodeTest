using System;
using System.Collections.Generic;
using System.Text;

namespace ATG.CodeTest.Models
{
    public class Incident
    {
        public Incident(DateTime incidentDateTime)
        {
            DateTime = incidentDateTime;
        }

        public DateTime DateTime { get; set; }
    }
}
