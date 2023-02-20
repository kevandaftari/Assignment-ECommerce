
using Newtonsoft.Json;

var builder = WebApplication.CreateBuilder(args);

ECommerceService.API.Configuration.IoCContainerConfiguration.ConfigureService(builder.Services);

builder.Services.AddMvc().AddNewtonsoftJson(
                    opts => opts.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                );
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

app.UseAuthorization();

app.MapControllers();

app.Run();
