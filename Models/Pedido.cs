using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace trackingApi.Models
{
    public class Pedido
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; } = null!;

        [BsonElement("Name")]
        public string OrderNum { get; set; } = null!;

        public string? Adress { get; set; } = null!;

    }
}