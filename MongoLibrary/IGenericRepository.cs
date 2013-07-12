using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace MongoLibrary
{
    /// <summary>
    /// Interface for generic repository
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IGenericRepository<T>
    {
        /// <summary>
        /// Gets or sets the name of the collection.
        /// </summary>
        /// <value>
        /// The name of the collection.
        /// </value>
        string CollectionName { get; set; }
        /// <summary>
        /// Gets the specified id.
        /// </summary>
        /// <param name="id">The id.</param>
        /// <returns></returns>
        T Get(int id);
        /// <summary>
        /// Gets the specified filter.
        /// </summary>
        /// <param name="filter">The filter.</param>
        /// <returns></returns>
        List<T> Get(Expression<Func<T, bool>> filter = null);
        /// <summary>
        /// Adds the specified entity.
        /// </summary>
        /// <param name="entity">The entity.</param>
        void Add(T entity);
        /// <summary>
        /// Deletes the specified entity.
        /// </summary>
        /// <param name="entity">The entity.</param>
        void Delete(T entity);
        /// <summary>
        /// Deletes the specified entity id.
        /// </summary>
        /// <param name="entityId">The entity id.</param>
        void Delete(int entityId);
        /// <summary>
        /// Gets the next identity.
        /// </summary>
        /// <param name="collectionId">The collection id.</param>
        /// <returns></returns>
        int GetNextIdentity(string collectionId);
    }
}
