using Microsoft.EntityFrameworkCore;
using TelegramClass;
using WepApiTGBot.Controllers;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Configuration.SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.Config.json", optional: true, reloadOnChange: true);
builder.Services.Configure<ConfigurationBot>(options => builder.Configuration.GetSection("ConfigurationBot").Bind(options));

builder.Services.AddControllers().AddNewtonsoftJson();
builder.Services.Add(new ServiceDescriptor(typeof(IConfiguration), builder.Configuration));
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

string connection = builder.Configuration.GetConnectionString("DefaultConnection");

// добавляем контекст ApplicationContext в качестве сервиса в приложение
builder.Services.AddDbContext<ApplicationContext>(options => options.UseSqlServer(connection));

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
