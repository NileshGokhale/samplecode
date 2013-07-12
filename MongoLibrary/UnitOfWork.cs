using System;
using DataAccessObjects;
using MongoDB.Bson;
using MongoDB.Driver;

namespace MongoLibrary
{
    /// <summary>
    /// Unit of work implementation
    /// </summary>
    public class UnitOfWork : IUnitOfWork
    {
        private readonly MongoDatabase _database;
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
                return _guestBookEntryRepository ?? (_guestBookEntryRepository = new GenericRepository<GuestBookEntry>(this));
            }
        }
        #endregion

        /// <summary>
        /// Initializes a new instance of the <see cref="UnitOfWork"/> class.
        /// </summary>
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

        /// <summary>
        /// Saves the changes.
        /// </summary>
        /// <exception cref="System.NotImplementedException"></exception>
        public void SaveChanges()
        {
            throw new NotImplementedException();

        }

        /// <summary>
        /// Gets the guest book entries.
        /// </summary>
        /// <value>
        /// The guest book entries.
        /// </value>
        public MongoCollection<BsonDocument> GuestBookEntries
        {
            get { return _database.GetCollection("GuestBookEntry"); }
        }

        #endregion
    }
}
