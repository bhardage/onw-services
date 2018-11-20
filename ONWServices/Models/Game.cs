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
            SelectedRoles = new List<Role>
            {
                Role.Warewolf,
                Role.Seer,
                Role.Robber,
                Role.Troublemaker
            };
        }

        [BsonElement("gameId")]
        [BsonSerializer(typeof(GuidStringSerializer))]
        public Guid GameId { get; set; }

        public GameStatus Status { get; set; }
        public List<Role> SelectedRoles { get; set; }
    }

    public enum GameStatus
    {
        New,
        Closed
    }

    public enum Role
    {
        Warewolf,
        Troublemaker,
        Seer,
        Robber
    }
}
