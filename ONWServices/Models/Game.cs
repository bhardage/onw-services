using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using ONWServices.Models.Serializers;
using System;

namespace ONWServices.Models
{
    [BsonIgnoreExtraElements]
    public class Game : BaseDocument
    {
        [BsonElement("gameId")]
        [BsonSerializer(typeof(GuidStringSerializer))]
        public Guid GameId { get; set; }
    }
}
