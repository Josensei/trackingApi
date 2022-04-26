using trackingApi.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
namespace trackingApi.Services
{
    public class PedidosService
    {   
        private readonly IMongoCollection<Pedido> _pedidos;
        
        public PedidosService(
            IOptions<TrackingDatabaseSettings> trachingDatabaseSettings)
        { 
            var mongoClient = new MongoClient(
                trachingDatabaseSettings.Value.ConnectionString);
            var mongoDatabase = mongoClient.GetDatabase(
                trachingDatabaseSettings.Value.DatabaseName);
            _pedidos = mongoDatabase.GetCollection<Pedido>(
                trachingDatabaseSettings.Value.PedidosCollectionName);
        }
        public async Task<List<Pedido>> GetAsync() =>
       await _pedidos.Find(_ => true).ToListAsync();

        public async Task<Pedido?> GetAsync(string id) =>
            await _pedidos.Find(x => x.Id == id).FirstOrDefaultAsync();

        public async Task CreateAsync(Pedido newPedido) =>
            await _pedidos.InsertOneAsync(newPedido);

        public async Task UpdateAsync(string id, Pedido updatedPedido) =>
        await _pedidos.ReplaceOneAsync(x => x.Id == id, updatedPedido);

        public async Task RemoveAsync(string id) =>
            await _pedidos.DeleteOneAsync(x => x.Id == id);

    }
}
