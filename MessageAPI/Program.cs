using MessageAPI.Data;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("MessageDatabase");
builder.Services.AddDbContext<MessageDbContext>(options =>
    options.UseNpgsql(connectionString));  // PostgreSQL için Npgsql kullanýlýyor


// Add services to the container.

builder.Services.AddControllers().AddJsonOptions(option => {
    option.JsonSerializerOptions.PropertyNamingPolicy = null;
    option.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());

} );
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
