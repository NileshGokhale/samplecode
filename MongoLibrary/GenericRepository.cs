using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataAccessObjects;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using MongoDB.Driver.Builders;

namespace MongoLibrary
{
    /// <summary>
    /// Generic repository for database crud operation on T entity
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class GenericRepository<T> : IGenericRepository<T> where
        T : MongoEntityBase
    {
        private const string CounterCollection = "counter";
        private UnitOfWork _uoW;
        private MongoCollection<T> dbSet;
        private string _collectionName;
        public GenericRepository(UnitOfWork uoW)
        {
            _uoW = uoW;
            Type type = typeof(T);
            _collectionName = type.Name;
            dbSet = _uoW.Database.GetCollection<T>(type.Name);
        }

        #region IGenericRepository<T> Members

        /// <summary>
        /// Gets the specified id.
        /// </summary>
        /// <param name="id">The id.</param>
        /// <returns></returns>
        public T Get(int id)
        {
            var query = Query<T>.EQ(e => e.EntityId, id);
            var doc = dbSet.FindOne(query);
            return doc;
        }

        /// <summary>
        /// Gets the specified filter.
        /// </summary>
        /// <param name="filter">The filter.</param>
        /// <returns>
        /// [cref]
        /// </returns>
        public List<T> Get(System.Linq.Expressions.Expression<Func<T, bool>> filter = null)
        {
            IQueryable<T> query = dbSet.AsQueryable();
            if (filter != null)
            {
                query = query.Where(filter);
            }
            return query.ToList();
        }

        /// <summary>
        /// Adds the specified entity.
        /// </summary>
        /// <param name="entity">The entity.</param>
        public void Add(T entity)
        {
            entity.EntityId = GetNextIdentity(entity.GetType().Name);
            dbSet.Insert(entity);
        }

        /// <summary>
        /// Deletes the specified entity.
        /// </summary>
        /// <param name="entity">The entity.</param>
        public void Delete(T entity)
        {
            var query = Query<T>.EQ(e => e.EntityId, entity.EntityId);
            dbSet.Remove(query);
        }

        /// <summary>
        /// Deletes the specified entity id.
        /// </summary>
        /// <param name="entityId">The entity id.</param>
        public void Delete(int entityId)
        {
            var query = Query<T>.EQ(e => e.EntityId, entityId);
            Delete(dbSet.FindOne(query));
        }

        #endregion

        #region IGenericRepository<T> Members

        /// <summary>
        /// Gets or sets the name of the collection.
        /// </summary>
        /// <value>
        /// The name of the collection.
        /// </value>
        public string CollectionName { get { return _collectionName; } set { _collectionName = value; } }

        #endregion

        #region IGenericRepository<T> Members


        /// <summary>
        /// Gets the next identity.
        /// </summary>
        /// <param name="collectionId">The collection id.</param>
        /// <returns></returns>
        public int GetNextIdentity(string collectionId)
        {
            bool hasCollection = _uoW.Database.CollectionExists(CounterCollection);
            if (!hasCollection) _uoW.Database.CreateCollection(CounterCollection);
            var counters = _uoW.Database.GetCollection(CounterCollection);
            var query = Query.EQ(collectionId, collectionId);
            var cnt = counters.Find(query);
            if (!cnt.Any())
            {
                var document = new BsonDocument();
                var element = new Dictionary<string, object> { { collectionId, collectionId }, { "value", 0 } };
                document.AddRange(element);
                counters.Insert(document);
            }
            cnt = counters.Find(query);
            int retVal = 0;
            if (cnt.Any())
            {
                var val = cnt.Select(x => x.GetValue(2)).FirstOrDefault();
                if (val != null)
                {
                    retVal = val.ToInt32();
                    retVal += 1;
                }
                var update = Update.Set("value", retVal);
                counters.Update(query, update);
            }
            return retVal;
        }

        #endregion
    }
}
