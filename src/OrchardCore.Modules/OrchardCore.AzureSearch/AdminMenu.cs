using Microsoft.Extensions.Localization;
using OrchardCore.Navigation;
using System;
using System.Threading.Tasks;

namespace OrchardCore.AzureSearch
{
    public class AdminMenu : INavigationProvider
    {
        public AdminMenu(IStringLocalizer<AdminMenu> localizer)
        {
            T = localizer;
        }

        public IStringLocalizer T { get; set; }

        public Task BuildNavigationAsync(string name, NavigationBuilder builder)
        {
            if (!String.Equals(name, "admin", StringComparison.OrdinalIgnoreCase))
            {
                return Task.CompletedTask;
            }

            builder
                .Add(T["Configuration"], "10", configuration => configuration
                    .AddClass("menu-configuration").Id("configuration")
                    .Add(T["Site"], "10", import => import
                        .Add(T["AzureSearch Indices"], "7", indexes => indexes
                            .Action("Index", "Admin", new { area = "OrchardCore.AzureSearch" })
                            .Permission(Permissions.ManageIndexes)
                            .LocalNav())
                        .Add(T["AzureSearch Queries"], "8", queries => queries
                            .Action("Query", "Admin", new { area = "OrchardCore.AzureSearch" })
                            .Permission(Permissions.ManageIndexes)
                            .LocalNav())))
                .Add(T["Configuration"], configuration => configuration
                    .Add(T["Settings"], settings => settings
                        .Add(T["Search"], T["Search"], entry => entry
                            .Action("Index", "Admin", new { area = "OrchardCore.Settings", groupId = "search" })
                            .Permission(Permissions.ManageIndexes)
                            .LocalNav()
                        )));

            return Task.CompletedTask;
        }
    }
}
