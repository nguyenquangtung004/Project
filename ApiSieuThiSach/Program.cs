using ApiSieuThiSach.Config;
using ApiSieuThiSach.sevice;
using MongoDB.Driver;
var builder = WebApplication.CreateBuilder(args);
// Đọc cấu hình MongoDB từ appsettings.json và map vào class MongoDbSettings
builder.Services.Configure<MongoDbSettings>(builder.Configuration.GetSection("MongoDbSettings"));

// Đăng ký MongoDB Client như một Singleton
// IMongoClient sẽ được inject vào các service cần nó
builder.Services.AddSingleton<IMongoClient>(sp => new MongoClient(builder.Configuration.GetValue<string>("MongoDbSettings:ConnectionString")));
// Đăng ký BookService
// AddScoped: một instance mới sẽ được tạo cho mỗi HTTP request
builder.Services.AddScoped<BookService>();
builder.Services.AddScoped<AuthorService>();

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
