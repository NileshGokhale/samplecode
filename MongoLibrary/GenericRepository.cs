using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using MongoDB.Driver.Builders;
using DTO;
namespace MongoLibrary
{
    public class GenericRepository<T> : IGenericRepository<T> where
        T : MongoEntityBase
    {
        private const string COUNTER_COLLECTION = "counter";
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

        public T Get(int id)
        {
            var query = Query<T>.EQ(e => e.EntityId, id);
            var doc = dbSet.FindOne(query);
            return doc;
        }

        public List<T> Get(System.Linq.Expressions.Expression<Func<T, bool>> filter = null)
        {
            return _uoW.Database.GetCollection<T>(_collectionName).AsQueryable().Where(filter).ToList();
        }

        public void Add(T entity)
        {
            entity.EntityId = GetNextIdentity(entity.GetType().Name);
            dbSet.Insert(entity);
        }

        public void Delete(T entity)
        {
            var query = Query<T>.EQ(e => e.EntityId, entity.EntityId);
            dbSet.Remove(query);
        }

        public void Delete(int entityId)
        {
            var query = Query<T>.EQ(e => e.EntityId, entityId);
            Delete(dbSet.FindOne(query));
        }

        #endregion

        #region IGenericRepository<T> Members

        public string CollectionName { get { return _collectionName; } set { _collectionName = value; } }

        #endregion

        #region IGenericRepository<T> Members


        public int GetNextIdentity(string collectionId)
        {
            bool hasCollection = _uoW.Database.CollectionExists(COUNTER_COLLECTION);
            if (!hasCollection) _uoW.Database.CreateCollection(COUNTER_COLLECTION);
            var counters = _uoW.Database.GetCollection(COUNTER_COLLECTION);
            var query = Query.EQ(collectionId, collectionId);
            var cnt = counters.Find(query);
            if (!cnt.Any())
            {
                BsonDocument document = new BsonDocument();
                Dictionary<string, object> element = new Dictionary<string, object>();
                element.Add(collectionId, collectionId);
                element.Add("value", 0);
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
                    val = retVal;
                }
                var update = Update.Set("value", retVal);
                counters.Update(query, update);
            }
            return retVal;
        }

        #endregion
    }
}
