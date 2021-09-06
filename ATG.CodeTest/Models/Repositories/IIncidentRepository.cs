using System.Linq;

namespace ATG.CodeTest.Models.Repositories
{
    public interface IIncidentRepository
    {
        IQueryable<Incident> FailoverEvents { get; set; }
    }
}