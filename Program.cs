using MassTransit;

using TestMassTransit;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSwaggerGen();
builder.Services.AddControllers();
builder.Services.AddMediator(cfg => cfg.AddConsumersFromNamespaceContaining<Consumers>());
builder.Services.Configure<RabbitMqTransportOptions>(builder.Configuration.GetSection("RabbitMqTransport"));

// Configuration
builder.Services.AddWorkerServices();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();
app.MapControllers();
app.Run();
