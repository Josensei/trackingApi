using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
    namespace trackingApi.Models
{
    public class Vehicle
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }
              
        public string Matricula { get; set; } = null!;
        public string? Driver { get; set; }
        public List<MyLocation> locations { get; set; }= new List<MyLocation>();

        //public string? Position { get; set; } = null!;
        public List<string> pedidos { get; set; } = new List<string>();
    }
}
