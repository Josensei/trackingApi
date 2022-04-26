namespace trackingApi.Models
{
    public class VehiclesDatabaseSettings
    {
        public string ConnectionString { get; set; } = null!;
        public string DatabaseName { get; set; } = null!;
        public string TrackingCollectionName { get; set; } = null!;
    }
}
