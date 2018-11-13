using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;

namespace ONWServices.Models
{
    [BsonIgnoreExtraElements]
    public class Game : BaseDocument
    {
        [BsonElement("gameId")]
        public Guid GameId { get; set; }
    }
}
