using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using ONWServices.Models.Serializers;
using System;
using System.Collections.Generic;

namespace ONWServices.Models
{
    [BsonIgnoreExtraElements]
    public class Game : BaseDocument
    {
        public Game() {
            Status = GameStatus.New;            
        }

        [BsonElement("gameId")]
        [BsonSerializer(typeof(GuidStringSerializer))]
        public Guid GameId { get; set; }

        public GameStatus Status { get; set; }
        public List<PlayerRole> SelectedRoles { get; set; }
    }

    public enum GameStatus
    {
        New
    }
}
