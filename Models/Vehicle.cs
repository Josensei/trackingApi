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
        public string Matrícula { get; set; } = null!;

        public string? Driver { get; set; } = null!;

        public string? position { get; set; } = null!;

        public List<Pedido> pedido { get; set; } = new List<Pedido>();

    }
}
