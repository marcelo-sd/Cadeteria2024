using Cadeteria2024MD.Models.DTOs;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers().AddJsonOptions(options =>
{
    // Agregar el convertidor personalizado para Icliente
    options.JsonSerializerOptions.Converters.Add(new ClienteConvert());
});
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

// Configurar el logging
builder.Services.AddLogging(loggingBuilder =>
{
    loggingBuilder.ClearProviders(); // Limpia los proveedores de logging predeterminados
    loggingBuilder.AddConsole(); // Agrega el proveedor de logging en consola
    loggingBuilder.AddDebug(); // Agrega el proveedor de logging en debug
});




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
