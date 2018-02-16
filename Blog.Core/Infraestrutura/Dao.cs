using System.Collections.Generic;
using System.Linq;
using Blog.Core.Domain;
using Blog.Core.Framework;
using MongoDB.Bson;
using MongoDB.Driver;

namespace Blog.Core.Infraestrutura
{
    public abstract class Dao<T> where T : Entidade
    {
        protected readonly string Collection;
        protected MongoClient Client;
        protected IMongoDatabase Db;

        protected Dao(string collection)
        {
            Collection = collection;
            var connectionString = EnviromentVariables.GetOrDefault("mongodb-connection", "mongodb://172.17.0.2:27017");
            var database = EnviromentVariables.GetOrDefault("mongodb-database", "blog");
            Client = new MongoClient(connectionString);
            Db = Client.GetDatabase(database);
        }

        public virtual void InsertOne(T obj)
        {
            var doc = MapTo(obj);
            Db.GetCollection<BsonDocument>(Collection).InsertOne(doc);
            var id = doc["_id"];
            obj.Id = id.AsObjectId.ToString();
        }

        public virtual IEnumerable<T> Find(FilterDefinition<BsonDocument> filter)
        {
            return Db.GetCollection<BsonDocument>(Collection).Find(filter).ToEnumerable().Select(MapTo);
        }

        public T FindOne(FilterDefinition<BsonDocument> filter)
        {
            return Db.GetCollection<BsonDocument>(Collection).Find(filter).ToEnumerable().Select(MapTo)
                .FirstOrDefault();
        }

        protected abstract T MapTo(BsonDocument document);
        protected abstract BsonDocument MapTo(T obj);
    }
}