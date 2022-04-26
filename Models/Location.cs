using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace trackingApi.Models
{
    public class Location
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; } 
        public DateTime CreationDate { get; set; }   
        public double lon { get; set; }
        public double lat { get; set; } 


    }
}