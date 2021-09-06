using ATG.CodeTest.Models;
using ATG.CodeTest.Models.Options;
using ATG.CodeTest.Models.Repositories;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ATG.CodeTest.Services
{
    public class IncidentService : IIncidentService
    {
        private readonly IDateTimeService _dateTimeService;
        private readonly FailoverOptions _options;
        private readonly IIncidentRepository _incidentRepository;

        public IncidentService(IOptions<FailoverOptions> options, IDateTimeService dateTimeService, IIncidentRepository incidentRepository)
        {
            _dateTimeService = dateTimeService;
            _options = options.Value;
            _incidentRepository = incidentRepository;
        }

        public IQueryable<Incident> GetFailoverEvents()
        {
            // return all failed events from repository
            return _incidentRepository.FailoverEvents;
        }

        public bool IsFailoverEnabled()
        {
            // original logic specifies that the events are happening 10 or more minutes in the future
            // if more than 50 events occure more than 10 minutes in the future
            // .Where(x => x.DateTime > _dateTimeService.Now.AddMinutes(_options.FailoverWindowMinutes))
            // this doesn't seem to make sense as it seems like this should be if more than 50 events have occured in the last 10 minutes then failover
            // there's nothing in the spec clarifying this so it would normally be the point I check with the project manager
            if (
                _options.FailoverModeEnabled
                && _incidentRepository.FailoverEvents
                    .Where(x => x.DateTime > _dateTimeService.Now.AddMinutes(-_options.FailoverWindowMinutes))
                    .Count() > _options.MaxFailedRequests
                )
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
