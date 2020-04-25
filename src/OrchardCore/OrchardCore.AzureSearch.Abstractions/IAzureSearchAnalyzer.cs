namespace OrchardCore.AzureSearch
{
    /// <summary>
    /// Represents an <see cref="Analyzer"/> instance that is available in the system.
    /// </summary>
    public interface IAzureSearchAnalyzer
    {
        string Name { get; }
        /*
        Analyzer CreateAnalyzer();
        */
    }
}
