using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
    namespace trackingApi.Models
{
    public class Vehicle
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }
        
        [BsonElement("Name")]
        public string matricula { get; set; } = null!;
        public string? driver { get; set; }
        public string? position { get; set; } = null!;
        public string pedido { get; set; } = null!;
    }
}
