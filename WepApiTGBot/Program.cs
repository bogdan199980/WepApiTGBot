
using WepApiTGBot.Controllers;
using static WepApiTGBot.Controllers.BotController;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Configuration.SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
builder.Services.Configure<List<UpdateProcessing>>(options => builder.Configuration.GetSection("UpdateProcessings").Bind(options));

builder.Services.AddControllers().AddNewtonsoftJson();
builder.Services.Add(new ServiceDescriptor(typeof(IConfiguration), builder.Configuration));
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
