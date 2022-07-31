using Microsoft.Extensions.DependencyInjection;
using PackIT.Shared.Abstractions.Command;
using PackIT.Shared.Abstractions.Query;
using PackIT.Shared.Commands;
using PackIT.Shared.Queries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace PackIT
{
    public static class Extensions
    {
        public static IServiceCollection AddQueries(this IServiceCollection services)
        {

            var assembly = Assembly.GetCallingAssembly();
            services.AddSingleton<IQueryDispatcher, InMemoryQueryDispatcher>();
            services.Scan(s => s.FromAssemblies(assembly)
                .AddClasses(c => c.AssignableTo(typeof(IQueryHandler<,>)))
                .AsImplementedInterfaces()
                .WithScopedLifetime());

            return services;
        }
    }
}
