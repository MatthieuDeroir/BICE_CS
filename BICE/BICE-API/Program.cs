using System.Data.SqlClient;
using System.Text.Json;
using System.Text.Json.Serialization;
using BICE.DAL.Wrappers;

var builder = WebApplication.CreateBuilder(args);

var configuration = new ConfigurationBuilder()
    .AddJsonFile("appsettings.json")
    .Build();

string connectionString = configuration.GetConnectionString("default");

builder.Services.AddSingleton(connectionString);

builder.Services.AddScoped(provider =>
{
    var connectionString = provider.GetRequiredService<string>();
    var connection = new SqlConnection(connectionString);
    return new SqlCommand { Connection = connection };
});

// Add services to the container.
builder.Services.AddScoped<IDbConnectionWrapper, DbConnectionWrapper>();
builder.Services.AddScoped<IDbCommandWrapper, DbCommandWrapper>();

builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
    options.JsonSerializerOptions.IgnoreNullValues = true;
    options.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
});


// Add services to the container.

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

