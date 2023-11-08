using MongoDB.Driver;

namespace LaboratorioComponentes.Services
{
    public class MongoDBContext
    {
        private static IMongoDatabase _database;
        private static MongoClient _client;

        private MongoDBContext(string connectionString, string databaseName)
        {
            _client = new MongoClient(connectionString);
            _database = _client.GetDatabase(databaseName);
        }

        public IMongoDatabase Database => _database;

        private static MongoDBContext _instance;

        public static MongoDBContext GetInstance(string connectionString, string databaseName)
        {
            if (_instance == null)
            {
                _instance = new MongoDBContext(connectionString, databaseName);
            }
            return _instance;
        }
    }
}
