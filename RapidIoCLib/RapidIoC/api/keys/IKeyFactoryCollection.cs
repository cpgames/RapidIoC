namespace cpGames.core.RapidIoC
{
    /// <summary>
    /// Contains all key factories. <see cref="Rapid" /> contains instance of this.
    /// </summary>
    public interface IKeyFactoryCollection
    {
        #region Methods
        /// <summary>
        /// Loop through all factories and try to generate a key.
        /// </summary>
        /// <param name="keyData">DataType used to create a key.</param>
        /// <param name="key">Key instance.</param>
        /// <returns>
        /// True if key is created.
        /// False if no factory found that supports provided dataType.
        /// False if a factory supports provided dataType, but fails to generate key for whatever reason.
        /// </returns>
        bool Create(object keyData, out IKey key);

        /// <summary>
        /// Loop through all factories and try to generate a key.
        /// </summary>
        /// <param name="keyData">DataType used to create a key.</param>
        /// <param name="key">Key instance.</param>
        /// <param name="errorMessage">If fails, this explains why.</param>
        /// <returns>
        /// True if key is created.
        /// False if no factory found that supports provided dataType.
        /// False if a factory supports provided dataType, but fails to generate key for whatever reason.
        /// </returns>
        bool Create(object keyData, out IKey key, out string errorMessage);

        /// <summary>
        /// Add new key factory.
        /// </summary>
        /// <param name="factory">Factory instance to add.</param>
        /// <returns>False if factory of this type already exists, otherwise true.</returns>
        bool AddFactory(IKeyFactory factory);

        /// <summary>
        /// Add new key factory.
        /// </summary>
        /// <param name="factory">Factory instance to add.</param>
        /// <param name="errorMessage">If fails, this explains why.</param>
        /// <returns>False if factory of this type already exists, otherwise true.</returns>
        bool AddFactory(IKeyFactory factory, out string errorMessage);
        #endregion
    }
}