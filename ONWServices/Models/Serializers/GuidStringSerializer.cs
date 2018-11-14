using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Serializers;
using System;

namespace ONWServices.Models.Serializers
{
    /// <summary>
    /// Serializes <c>Guid</c>'s to <c>string</c>'s and back.
    /// </summary>
    /// <remarks>
    /// The default <c>Guid</c> serializer, <c>MongoDB.Bson.Serialization.Serializers.GuidSerializer</c>,
    /// serializes <c>Guid</c>'s as <c>byte</c> arrays. This class opts to serialize using the
    /// much-more-readable <c>string</c> format.
    /// </remarks>
    public class GuidStringSerializer : SerializerBase<Guid>
    {
        public override void Serialize(BsonSerializationContext context, BsonSerializationArgs args, Guid value)
        {
            context.Writer.WriteString(value.ToString());
        }

        public override Guid Deserialize(BsonDeserializationContext context, BsonDeserializationArgs args)
        {
            return new Guid(context.Reader.ReadString());
        }
    }
}
