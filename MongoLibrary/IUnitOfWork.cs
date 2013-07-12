namespace MongoLibrary
{
    /// <summary>
    /// Inter face for UnitofWork pattern
    /// </summary>
    interface IUnitOfWork
    {
        /// <summary>
        /// Saves the changes.
        /// </summary>
        void SaveChanges();
    }
}
