using ATG.CodeTest.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ATG.CodeTest.Services
{
    public interface IIncidentService
    {
        IQueryable<Incident> GetFailoverEvents();

        public bool IsFailoverEnabled();
    }
}
