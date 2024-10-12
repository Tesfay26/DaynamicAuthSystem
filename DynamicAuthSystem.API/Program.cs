using DynamicAuthSystem.API.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Configure services using ServiceExtensions
builder.Services.ConfigureDatabase(builder.Configuration);
builder.Services.ConfigureIdentity();
builder.Services.ConfigureAuthentication(builder.Configuration);
builder.Services.ConfigureApplicationServices();
builder.Services.RegisterMediatRHandlers();
builder.Services.RegisterValidators();
builder.Services.RegisterAutoMapper();

// Add controllers and other services
builder.Services.AddControllers();
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
app.UseAuthentication(); // Ensure authentication is used
app.UseAuthorization();

app.MapControllers();

app.Run();
