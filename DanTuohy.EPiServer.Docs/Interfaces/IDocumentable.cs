namespace Tuohy.Epi.Docs.Interfaces
{
    /// <summary>
    /// Add this to EPiServer property attributes to allow documentation to be generated for
    /// </summary>
    public interface IDocumentable
    {
        /// <summary>
        /// The display name of the attribute
        /// </summary>
        string Name { get; }

        /// <summary>
        /// The value/description of the attribute
        /// </summary>
        string Value { get; }
    }
}
