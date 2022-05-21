using MessengerApplication.Models;
using MessengerApplication.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.Configure<MessengerApplicationDatabaseSettings>(
    builder.Configuration.GetSection("MessengerApplicationDatabase"));
builder.Services.AddScoped<UsersService>();
builder.Services.AddScoped<DatabaseProviderService>();
builder.Services.AddScoped<ChatsService>();
builder.Services.AddScoped<MessagesService>();

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