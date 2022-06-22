enum TypeReference
{
    Equals,
    Anymore,
    Smaller,
    Similar
}

enum TypeSend
{
    sendMessage,
    copyMessage,
    sendPhoto,
    sendAudio,
    sendVideo,
    sendAnimation,
    sendVoice,
    sendMediaGroup,
    sendLocation,
    sendContact
}



var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers().AddNewtonsoftJson();
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
