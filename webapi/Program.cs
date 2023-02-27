using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Serializers;
using MongoDB.Driver;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddSingleton<IMongoClient>(ServiceProvider =>
{
    BsonSerializer.RegisterSerializer(new GuidSerializer(BsonType.String));
    BsonSerializer.RegisterSerializer(new DateTimeOffsetSerializer(BsonType.String));
    var settings = builder.Configuration.GetSection(nameof(webapi.Settings.MongoDB)).Get<webapi.Settings.MongoDB>();
    return new MongoClient(settings.ConnectionString);
});

builder.Services.AddControllers(options => {
    options.SuppressAsyncSuffixInActionNames = false;
});
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
