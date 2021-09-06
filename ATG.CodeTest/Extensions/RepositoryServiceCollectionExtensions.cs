using ATG.CodeTest.Models.Options;
using ATG.CodeTest.Models.Repositories;
using Microsoft.Extensions.DependencyInjection;
using System;
using ATG.CodeTest.Types;

namespace ATG.CodeTest.Extensions
{
    public static class RepositoryServiceCollectionExtensions
    {
        public delegate IRepository ServiceResolver(RepositoryType repositoryType);

        public static IServiceCollection AddRepositories(this IServiceCollection collection)
        {
            if (collection == null) throw new ArgumentNullException(nameof(collection));

            collection.AddSingleton<IRepository, ArchivedRepository>();
            collection.AddSingleton<IRepository, FailoverRepository>();
            collection.AddSingleton<IRepository, MainRepository>();

            collection.AddTransient<ServiceResolver>(serviceProvider => reporitoryTypeName =>
            {
                switch (reporitoryTypeName)
                {
                    case RepositoryType.Archive:
                        return serviceProvider.GetService<ArchivedRepository>();
                    case RepositoryType.Failover:
                        return serviceProvider.GetService<FailoverRepository>();
                    case RepositoryType.Main:
                        return serviceProvider.GetService<MainRepository>();
                    default:
                        return null;
                }
            });

            return collection;
        }
    }
}
