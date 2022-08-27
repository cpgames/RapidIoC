using System.Collections.Generic;

namespace cpGames.core.RapidIoC
{
    /// <summary>
    /// A collection of contexts
    /// </summary>
    public interface IContextCollection
    {
        #region Properties
        /// <summary>
        /// Root context. There can only be one of these.
        /// </summary>
        IContext Root { get; }

        /// <summary>
        /// Number of local contexts (i.e. excluding root context).
        /// </summary>
        int Count { get; }

        /// <summary>
        /// Enumeration of local contexts.
        /// </summary>
        IEnumerable<IContext> Contexts { get; }
        #endregion

        #region Methods
        /// <summary>
        /// Check if context exists.
        /// </summary>
        /// <param name="key">>Unique key for context.</param>
        /// <returns>rue if exists, otherwise false.</returns>
        Outcome ContextExists(IKey key);

        /// <summary>
        /// Find context by name, returns false if context does not exist.
        /// </summary>
        /// <param name="key">Unique name for context.</param>
        /// <param name="context">Context instance if found, otherwise null.</param>
        /// <returns>True if successful, otherwise false.</returns>
        Outcome FindContext(IKey key, out IContext? context);

        /// <summary>
        /// Find context by name, try to create one if none exists.
        /// </summary>
        /// <param name="key">Unique key for context.</param>
        /// <param name="context">Context instance if found, otherwise null.</param>
        /// <returns>True if successful, otherwise false.</returns>
        Outcome FindOrCreateContext(IKey key, out IContext? context);
        #endregion
    }
}