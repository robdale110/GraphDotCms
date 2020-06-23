using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace GraphDotCms.Domain.Entities
{
    public class Value
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        [BsonElement("key")]
        public string Key { get; set; }
        [BsonElement("value")]
        public string TheValue { get; set; }
    }
}
