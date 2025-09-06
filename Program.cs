using Examen_Pe�afiel.Datos;
using Examen_Pe�afiel.Datos.Repositorio;
using System.Data;
using System.Data.SqlClient;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("Examen_Pe�afiel");
builder.Services.AddTransient<IDbConnection>(sp => new SqlConnection(connectionString));
builder.Services.AddTransient<IUsuarioRepo, UsuarioRepo>();


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


app.UseAuthorization();

app.MapControllers();

app.Run();