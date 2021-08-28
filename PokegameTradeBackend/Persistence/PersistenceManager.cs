using MongoDB.Driver;

namespace PokegameTradeBackend
{
    public class PersistenceManager
    {
        private readonly string mongoUrl = "localhost:27017";
        private readonly string dbName = "PokegameTradeApp";

        private static PersistenceManager instance;

        private IMongoDatabase database;

        private PersistenceManager()
        {
            MongoClient dbClient = new MongoClient(mongoUrl);
            this.database = dbClient.GetDatabase(dbName);
        }

        public static PersistenceManager GetInstance()
        {
            if (instance == null)
            {
                instance = new PersistenceManager();
            }
            return instance;
        }

        public void Insert<T>(string collectionName, T obj)
        {
            var collection = database.GetCollection<T>(collectionName);
            collection.InsertOne(obj);
        }

        public T GetById<T>(string collectionName, string id)
        {
            var collection = database.GetCollection<T>(collectionName);
            var filter = Builders<T>.Filter.Eq("_id", id);
            return collection.Find(filter).First();
        }

        public void DeleteById<T>(string collectionName, string id)
        {
            var collection = database.GetCollection<T>(collectionName);
            var filter = Builders<T>.Filter.Eq("_id", id);
            collection.DeleteOne(filter);
        }
    }
}
