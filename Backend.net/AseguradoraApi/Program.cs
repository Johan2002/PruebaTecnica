using AseguradoraApi.Data;
var builder = WebApplication.CreateBuilder(args);


// Agrega los servicios necesarios para la aplicaci�n.
// En este caso, se est�n agregando servicios para los controladores, Swagger, y un singleton para ClienteData.
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
// Habilita la exploraci�n de endpoints para Swagger/OpenAPI.
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Registra ClienteData como un servicio singleton, lo que significa que se crear� una �nica instancia durante toda la vida de la aplicaci�n.
builder.Services.AddSingleton<ClienteData>();
// Configura CORS (Cross-Origin Resource Sharing) para permitir solicitudes desde cualquier origen.
// Esto es �til cuando se tiene un frontend en un dominio diferente al backend.
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

// Aplica la pol�tica de CORS definida anteriormente.
app.UseCors("NuevaPolitica");

// Habilita el middleware de autorizaci�n para manejar la autorizaci�n de solicitudes.
app.UseAuthorization();

// Mapea los controladores de la API a las rutas definidas en la aplicaci�n.
app.MapControllers();

// Ejecuta la aplicaci�n.
app.Run();
