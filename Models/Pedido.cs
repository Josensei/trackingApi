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
        public string? Address { get; set; }
        public string? email { get; set; }
        public estado estadosdelpedido { get; set; } = estado.Aceptado;
    }
    public enum estado
    {
        Aceptado,
        Rreparado,
        Enviado,
        En_Reparto

    }
}