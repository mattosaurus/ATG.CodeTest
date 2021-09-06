using ATG.CodeTest.Services;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace ATG.CodeTest.Extensions
{
    public static class LotServiceCollectionExtensions
    {
        public static IServiceCollection AddLotService(this IServiceCollection collection)
        {
            if (collection == null) throw new ArgumentNullException(nameof(collection));

            collection.AddSingleton<IDateTimeService, DateTimeService>();
            collection.AddSingleton<ILotService, LotService>();
            return collection;
        }
    }
}
