using Microsoft.Extensions.DependencyInjection;

namespace OrchardCore.AzureSearch
{
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        /// Adds AzureSearch queries services.
        /// </summary>
        public static IServiceCollection AddAzureSearchQueries(this IServiceCollection services)
        {
            services.AddScoped<IAzureSearchQueryService, AzureSearchQueryService>();
            return services;
        }
    }
}