using dao_library;
using dao_library.entity_framework;
using dao_library.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<ApplicationDbContext>(options =>
options.UseMySql(
    "Server=localhost;Database=Red_Poderosa;Uid=root;Pwd=2103;",
    new MySqlServerVersion(
        new Version (8, 0, 35)
    )
).UseLazyLoadingProxies()
);

builder.Services.AddScoped<IDAOFactory, DAOEFFactory>();

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
