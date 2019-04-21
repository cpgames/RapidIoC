namespace cpGames.core.RapidMVC
{
    /// <summary>
    /// Key factory allows creation of keys from different types of data.
    /// Feel free to implement your own factory.
    /// </summary>
    public interface IKeyFactory
    {
        #region Methods
        /// <summary>
        /// Create new key.
        /// </summary>
        /// <param name="keyData">DataType used to create a key.</param>
        /// <param name="key">Key instance.</param>
        /// <param name="errorMessage">If fails, this explains why.</param>
        /// <returns>
        /// True if key created.
        /// True if factory does not handle provided keyData dataType. <see cref="IKeyFactory" /> will handle choosing the right factory.
        /// False if dataType is handled, but key creation failed for whatever reason.
        /// </returns>
        bool Create(object keyData, out IKey key, out string errorMessage);
        #endregion
    }
}