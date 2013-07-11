using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MongoDB.Bson;
using MongoDB.Driver;
using DTO;

namespace MongoLibrary
{
    public class UnitOfWork : IUnitOfWork
    {
        private MongoDatabase _database;
        private GenericRepository<GuestBookEntry> _guestBookEntryRepository;
        private GenericRepository<Blog> _blogRepository;

        internal MongoDatabase Database { get { return _database; } }
        
        #region Repositories
        public GenericRepository<Blog> BlogRepository
        {
            get
            {
                return _blogRepository ?? new GenericRepository<Blog>(this);
            }
        }
        public GenericRepository<GuestBookEntry> GuestBookEntryRepository
        {
            get
            {
                if (_guestBookEntryRepository == null)
                {
                    _guestBookEntryRepository = new GenericRepository<GuestBookEntry>(this);
                }
                return _guestBookEntryRepository;
            }
        }
        #endregion

        public UnitOfWork()
        {
            const string dbName = "GuestBook";
            const string connectionString = "mongodb://localhost"; //TODO:USer App setting helper
            var client = new MongoClient(connectionString);
            var server = client.GetServer();
            var settings = new MongoDatabaseSettings();
            _database = server.GetDatabase(dbName, settings);//TODO:User app sertting helper
        }

        #region IUnitOfWork Members

        public void SaveChanges()
        {
            throw new NotImplementedException();

        }

        public MongoCollection<BsonDocument> GuestBookEntries
        {
            get { return _database.GetCollection("GuestBookEntry"); }
        }

        #endregion
    }
}
