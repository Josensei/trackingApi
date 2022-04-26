using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace trackingApi.Models
{
    public class Pedido
    {

        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }
        public string? OrderNum { get; set; }
        public string? Adress { get; set; }
    }
}