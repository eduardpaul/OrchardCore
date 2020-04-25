using OrchardCore.Modules.Manifest;

[assembly: Module(
    Name = "AzureSearch",
    Author = "The Orchard Team",
    Website = "https://orchardproject.net",
    Version = "2.0.0"
)]

[assembly: Feature(
    Id = "OrchardCore.AzureSearch",
    Name = "AzureSearch",
    Description = "Creates AzureSearch indexes to support search scenarios, introduces a preconfigured container-enabled content type.",
    Dependencies = new[]
    {
        "OrchardCore.Indexing",
        "OrchardCore.Liquid"
    },
    Category = "Content Management"
)]

[assembly: Feature(
    Id = "OrchardCore.AzureSearch.Worker",
    Name = "AzureSearch Worker",
    Description = "Provides a background task to keep local indices in sync with other instances.",
    Dependencies = new[] { "OrchardCore.AzureSearch" },
    Category = "Content Management"
)]

[assembly: Feature(
    Id = "OrchardCore.AzureSearch.ContentPicker",
    Name = "AzureSearch Content Picker",
    Description = "Provides a AzureSearch content picker field editor.",
    Dependencies = new[] { "OrchardCore.AzureSearch", "OrchardCore.ContentFields" },
    Category = "Content Management"
)]
