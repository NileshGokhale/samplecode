using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Linq.Expressions;

namespace MongoLibrary
{
    public interface IGenericRepository<T>
    {
        string CollectionName { get; set; }
        T Get(int id);
        List<T> Get(Expression<Func<T, bool>> filter = null);
        void Add(T entity);
        void Delete(T entity);
        void Delete(int entityId);
        int GetNextIdentity(string collectionId);
    }
}
