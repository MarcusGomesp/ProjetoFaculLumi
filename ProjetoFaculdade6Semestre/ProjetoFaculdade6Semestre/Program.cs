using Microsoft.EntityFrameworkCore;
using ProjetoFaculdade6Semestre;
using ProjetoFaculdade6Semestre.Interfaces;
using ProjetoFaculdade6Semestre.Service;

var builder = WebApplication.CreateBuilder(args);


// configuração do APPDbContext com SQL Server
var conectionString = builder.Configuration.GetConnectionString("ConectFacul");
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(conectionString));


// Injeção de dependência para o serviço de cadastro
builder.Services.AddScoped<ICadastro, CadastroServices>();


// Add services to the container.

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
