using System;
using MongoDB.Bson.Serialization.Attributes;

namespace MongoDB_DotnetCore.Model
{
    public abstract class MongoBase
    {
        [BsonId]
        public Object _id {get; set;}
    }
}
