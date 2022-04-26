using trackingApi.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
namespace trackingApi.Services
{
    public class VehiclesService
    {
        private readonly IMongoCollection<Vehicle> _vehicles;

        public VehiclesService(
            IOptions<TrackingDatabaseSettings> trackingDatabaseSettings)
        {
            var mongoClient = new MongoClient(
                trackingDatabaseSettings.Value.ConnectionString);
            var mongoDatabase = mongoClient.GetDatabase(
                trackingDatabaseSettings.Value.DatabaseName);
            _vehicles= mongoDatabase.GetCollection<Vehicle>(
                trackingDatabaseSettings.Value.TrackingCollectionName);
        }

        public async Task<List<Vehicle>> GetAsync() =>
        await _vehicles.Find(_ => true).ToListAsync();

        public async Task<Vehicle?> GetAsync(string id) =>
            await _vehicles.Find(x => x.Id == id).FirstOrDefaultAsync();

        public async Task CreateAsync(Vehicle newVehicle) =>
            await _vehicles.InsertOneAsync(newVehicle);

        public async Task UpdateAsync(string id, Vehicle updatedVehicle) =>
        await _vehicles.ReplaceOneAsync(x => x.Id == id, updatedVehicle);

        public async Task RemoveAsync(string id) =>
            await _vehicles.DeleteOneAsync(x => x.Id == id);

    }
}
