using dao_library;
using dao_library.entity_framework;
using dao_library.Interfaces;
using Microsoft.EntityFrameworkCore;
using web_api.Services;
using web_api.Controllers;
using dao_library.Interfaces.login;
using dao_library.entity_framework.ef_login;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Configuración de CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowLocalhost", builder =>
    {
        builder.WithOrigins("http://localhost:3000") // El origen de tu frontend
               .AllowAnyMethod()  // Permitir cualquier método (GET, POST, etc.)
               .AllowAnyHeader(); // Permitir cualquier encabezado
    });
});

builder.Services.AddDbContext<ApplicationDbContext>(options =>
options.UseMySql(
    "Server=localhost;Database=Red_Poderosa;Uid=root;Pwd=2103;",
    new MySqlServerVersion(
        new Version (8, 0, 35)
    )
).UseLazyLoadingProxies()
);

builder.Services.AddScoped<IDAOFactory, DAOEFFactory>();
builder.Services.AddScoped<IDAOUserBan, DAOEFUserBan>(); 
builder.Services.AddScoped<UserBanController>();
builder.Services.AddHostedService<VerifyBansService>();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// Agregar CORS a la pipeline
app.UseCors("AllowLocalhost");


app.UseAuthorization();

app.MapControllers();

app.Run();
