using MessengerApplication.Models;
using MessengerApplication.Services;

var builder = WebApplication.CreateBuilder(args);
var origins = "*";
// Add services to the container.

builder.Services.AddCors(options =>
{
    options.AddPolicy("CorsPolicy", builder =>
    {
        builder.AllowAnyHeader();
        builder.AllowAnyMethod();
        if (origins is not { Length: > 0 }) return;

        if (origins.Contains("*"))
        {
            builder.AllowAnyHeader();
            builder.AllowAnyMethod();
            builder.SetIsOriginAllowed(host => true);
            builder.AllowCredentials();
        }
        else
        {
            builder.WithOrigins(origins);
        }
    });
});

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

// // Configure the HTTP request pipeline.
// if (app.Environment.IsDevelopment())
// {
//     app.UseSwagger();
//     app.UseSwaggerUI();
// }

app.UseHttpsRedirection();
app.UseCors("CorsPolicy");
app.UseAuthorization();

app.MapControllers();

app.Run();