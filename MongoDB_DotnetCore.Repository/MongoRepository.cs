using MongoDB_DotnetCore.Model;
using System.Linq;
using MongoDB.Bson;
using MongoDB.Driver;
using System.Collections.Generic;

namespace MongoDB_DotnetCore.Repository
{
    public class MongoRepository
    {
        private IMongoCollection<TaskModel> _collection;
        public MongoRepository(string connStr, string dbName, string collName){
            var url = new MongoUrl(connStr);
            var client = new MongoClient(url);
            var dataBase = client.GetDatabase(dbName);
            _collection = dataBase.GetCollection<TaskModel>(collName);
        }

        public List<TaskModel> GetTasks(){
            var list = _collection.Find(new BsonDocument()).ToList();
            return list;
        }

        public TaskModel GetTask(int id){
            var filter = Builders<TaskModel>.Filter.Eq("ID", id);
            var result = _collection.Find(filter).FirstOrDefault();
            return result;
        }

        public void AddTask(TaskModel model){
            _collection.InsertOne(model);
        }

        public void UpdateTask(TaskModel model){
            var filter = Builders<TaskModel>.Filter.Eq(x => x.ID, model.ID);
            var updateDefinition = Builders<TaskModel>.Update
                .Set(x => x.Description, model.Description)
                .Set(x => x.Name, model.Name);

            _collection.UpdateOne(filter, updateDefinition);
        }

        public void DeleteTask(int id){
            var filter = Builders<TaskModel>.Filter.Eq("ID", id);
            var result = _collection.DeleteOne(filter);
        }
    }
}
