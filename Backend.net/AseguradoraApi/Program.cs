using AseguradoraApi.Data;
var builder = WebApplication.CreateBuilder(args);


// Agrega los servicios necesarios para la aplicación.
// En este caso, se están agregando servicios para los controladores, Swagger, y un singleton para ClienteData.
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
// Habilita la exploración de endpoints para Swagger/OpenAPI.
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Registra ClienteData como un servicio singleton, lo que significa que se creará una única instancia durante toda la vida de la aplicación.
builder.Services.AddSingleton<ClienteData>();
// Configura CORS (Cross-Origin Resource Sharing) para permitir solicitudes desde cualquier origen.
// Esto es útil cuando se tiene un frontend en un dominio diferente al backend.
builder.Services.AddCors(options =>
{
    options.AddPolicy("NuevaPolitica", app =>
    {
        app.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod();
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Aplica la política de CORS definida anteriormente.
app.UseCors("NuevaPolitica");

// Habilita el middleware de autorización para manejar la autorización de solicitudes.
app.UseAuthorization();

// Mapea los controladores de la API a las rutas definidas en la aplicación.
app.MapControllers();

// Ejecuta la aplicación.
app.Run();
