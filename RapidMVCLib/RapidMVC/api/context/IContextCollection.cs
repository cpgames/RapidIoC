using System.Collections.Generic;

namespace cpGames.core.RapidMVC
{
    /// <summary>
    ///     A collection of contexts
    /// </summary>
    public interface IContextCollection
    {
        #region Properties
        // Root context.
        IContext Root { get; }
        // Number of contexts (excluding root context).
        int Count { get; }
        // Enumeration of local contexts.
        IEnumerable<IContext> Contexts { get; }
        #endregion

        #region Methods
        // Find context by name, returns false if context does not exist.
        bool Find(string name, out IContext context, out string errorMessage);

        // Find context by name, try to create one if none exists.
        bool FindOrCreate(string name, out IContext context, out string errorMessage);

        // Check if context with this name exists.
        bool Exists(string name);
        #endregion
    }
}