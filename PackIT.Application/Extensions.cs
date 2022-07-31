using Microsoft.Extensions.DependencyInjection;
using PackIT.Domain.Factory;
using PackIT.Domain.Policies;
using PackIT.Shared;

namespace PackIT.Application
{
    public static class Extensions
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddCommands();
            services.AddSingleton<IPackingListFactory,PackingListFactory>();
            services.Scan(s => s.FromAssemblies(typeof(IPackingItemsPolicy).Assembly)
                .AddClasses(c => c.AssignableTo<IPackingItemsPolicy>())
                .AsImplementedInterfaces()
                .WithSingletonLifetime());
            return services;
        }
    }
}
