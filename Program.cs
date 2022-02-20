using EmployeesApi.Services;
using Microsoft.Net.Http.Headers;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddHttpClient<IColumbusService, ColumbusService>()
    .ConfigureHttpClient(client => {
        client.BaseAddress = new Uri(builder.Configuration["BaseAddress"]);
        var apiKey = builder.Configuration["ApiKey"];
        client.DefaultRequestHeaders.Add(HeaderNames.Accept, "application/json");
        client.DefaultRequestHeaders.Add("ApiKey", apiKey);
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
