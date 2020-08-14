using MongoDB_DotnetCore.Model;
using System.Linq;
using MongoDB.Driver;
using MongoDB.Bson;
using System.Collections.Generic;

namespace MongoDB_DotnetCore.Repository
{
    public class MongoBaseRepository
    {
        private IMongoCollection<TaskModel> _collection;
        public MongoBaseRepository(string connStr, string dbName, string collName){
            var url = new MongoUrl(connStr);
            var client = new MongoClient(url);
            var dataBase = client.GetDatabase(dbName);
            _collection = dataBase.GetCollection<TaskModel>(collName);
        }

        public List<TaskModel> GetTasks(){
            var list = _collection.Find(new BsonDocument()).ToList();
            return list;
        }
    }
}
