using ATG.CodeTest.Models.Options;
using ATG.CodeTest.Models.Repositories;
using ATG.CodeTest.Services;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace ATG.CodeTest.Extensions
{
    public static class IncidentServiceCollectionExtensions
    {
        public static IServiceCollection AddIncidentService(this IServiceCollection collection, Action<FailoverOptions> setupAction)
        {
            if (collection == null) throw new ArgumentNullException(nameof(collection));
            if (setupAction == null) throw new ArgumentNullException(nameof(setupAction));

            collection.Configure(setupAction);
            collection.AddSingleton<IIncidentRepository, IncidentRepository>();
            collection.AddSingleton<IIncidentService, IncidentService>();
            return collection;
        }

        public static IServiceCollection AddIncidentService(this IServiceCollection collection, FailoverOptions options)
        {
            return AddIncidentService(collection, builder => {
                builder = options;
            });
        }

        public static IServiceCollection AddIncidentService(this IServiceCollection collection, bool failoverModeEnabled, int maxFailedRequests, int failoverWindowMinutes)
        {
            return AddIncidentService(collection, builder => {
                builder.FailoverModeEnabled = failoverModeEnabled;
                builder.MaxFailedRequests = maxFailedRequests;
                builder.FailoverWindowMinutes = failoverWindowMinutes;
            });
        }

        public static IServiceCollection AddFailoverService(this IServiceCollection collection)
        {
            return AddIncidentService(collection, builder => { });
        }
    }
}
