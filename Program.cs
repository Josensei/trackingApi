using trackingApi.Models;
using trackingApi.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.Configure<TrackingDatabaseSettings>(
    builder.Configuration.GetSection("TrackingDatabase"));
builder.Services.AddSingleton<VehiclesService>();
builder.Services.AddSingleton<PedidosService>();
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}




app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
