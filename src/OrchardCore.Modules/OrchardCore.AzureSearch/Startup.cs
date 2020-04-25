using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;
using OrchardCore.BackgroundTasks;
using OrchardCore.ContentManagement;
using OrchardCore.ContentManagement.Handlers;
using OrchardCore.ContentTypes.Editors;
using OrchardCore.Deployment;
using OrchardCore.DisplayManagement.Descriptors;
using OrchardCore.DisplayManagement.Handlers;
using OrchardCore.Navigation;
using OrchardCore.Modules;
using OrchardCore.Queries;
using OrchardCore.Recipes;
using OrchardCore.Security.Permissions;
using OrchardCore.Settings;
using OrchardCore.AzureSearch.Services;
using OrchardCore.AzureSearch.Settings;
using OrchardCore.AzureSearch.Deployment;
using OrchardCore.AzureSearch.Recipes;
using OrchardCore.AzureSearch.Drivers;
using OrchardCore.AzureSearch.Handlers;

namespace OrchardCore.AzureSearch
{
    /// <summary>
    /// These services are registered on the tenant service collection
    /// </summary>
    public class Startup : StartupBase
    {
        public override void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton<AzureSearchIndexingState>();
            services.AddScoped<AzureSearchIndexingService>();
            services.AddScoped<ISearchQueryService, SearchQueryService>();

            services.AddScoped<IContentTypePartDefinitionDisplayDriver, ContentTypePartIndexSettingsDisplayDriver>();
            services.AddScoped<IContentPartFieldDefinitionDisplayDriver, ContentPartFieldIndexSettingsDisplayDriver>();
            services.AddScoped<INavigationProvider, AdminMenu>();
            services.AddScoped<IPermissionProvider, Permissions>();
            services.AddSingleton<AzureSearchIndexManager>();

            services.AddScoped<IDisplayDriver<ISite>, AzureSearchSiteSettingsDisplayDriver>();
            services.AddScoped<IDisplayDriver<Query>, AzureSearchQueryDisplayDriver>();

            services.AddScoped<IContentHandler, AzureSearchIndexingContentHandler>();
            services.AddAzureSearchQueries();

            // AzureSearchQuerySource is registered for both the Queries module and local usage
            services.AddScoped<IQuerySource, AzureSearchQuerySource>();
            services.AddScoped<AzureSearchQuerySource>();
            services.AddRecipeExecutionStep<AzureSearchIndexStep>();
        }

        public override void Configure(IApplicationBuilder app, IEndpointRouteBuilder routes, IServiceProvider serviceProvider)
        {
            routes.MapAreaControllerRoute(
                name: "AzureSearch.Search",
                areaName: "OrchardCore.AzureSearch",
                pattern: "Search/{id?}",
                defaults: new { controller = "Search", action = "Index" }
            );
        }
    }

    [RequireFeatures("OrchardCore.Deployment")]
    public class DeploymentStartup : StartupBase
    {
        public override void ConfigureServices(IServiceCollection services)
        {
            services.AddTransient<IDeploymentSource, AzureSearchIndexDeploymentSource>();
            services.AddSingleton<IDeploymentStepFactory>(new DeploymentStepFactory<AzureSearchIndexDeploymentStep>());
            services.AddScoped<IDisplayDriver<DeploymentStep>, AzureSearchIndexDeploymentStepDriver>();

            services.AddTransient<IDeploymentSource, AzureSearchSettingsDeploymentSource>();
            services.AddSingleton<IDeploymentStepFactory>(new DeploymentStepFactory<AzureSearchSettingsDeploymentStep>());
            services.AddScoped<IDisplayDriver<DeploymentStep>, AzureSearchSettingsDeploymentStepDriver>();
        }
    }

    [Feature("OrchardCore.AzureSearch.Worker")]
    public class AzureSearchWorkerStartup : StartupBase
    {
        public override void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton<IBackgroundTask, IndexingBackgroundTask>();
        }
    }

    [Feature("OrchardCore.AzureSearch.ContentPicker")]
    public class AzureSearchContentPickerStartup : StartupBase
    {
        public override void ConfigureServices(IServiceCollection services)
        {
            services.AddScoped<IContentPickerResultProvider, AzureSearchContentPickerResultProvider>();
            services.AddScoped<IContentPartFieldDefinitionDisplayDriver, ContentPickerFieldAzureSearchEditorSettingsDriver>();
            services.AddShapeAttributes<AzureSearchContentPickerShapeProvider>();
        }
    }
}
