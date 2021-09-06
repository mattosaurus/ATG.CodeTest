using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ATG.CodeTest.Models.Repositories
{
    public class IncidentRepository : IIncidentRepository
    {
        public IQueryable<Incident> FailoverEvents { get; set; } = new List<Incident>().AsQueryable();
    }
}
