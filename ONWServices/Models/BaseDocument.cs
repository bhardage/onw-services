using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace ONWServices.Models
{
    public class BaseDocument
    {
        [BsonId]
        public ObjectId Id { get; set; }
    }
}
