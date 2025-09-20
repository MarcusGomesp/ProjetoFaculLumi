using Microsoft.EntityFrameworkCore;
using ProjetoFaculdade6Semestre;
using ProjetoFaculdade6Semestre.Interface.AI;
using ProjetoFaculdade6Semestre.Interfaces;
using ProjetoFaculdade6Semestre.Service;
using ProjetoFaculdade6Semestre.Service.AIService;
using ProjetoFaculdade6Semestre.Controllers;
using ProjetoFaculdade6Semestre.Interface;

var builder = WebApplication.CreateBuilder(args);


// configura��o do APPDbContext com SQL Server
var conectionString = builder.Configuration.GetConnectionString("ConectFacul");

builder.Services.AddDbContext<AppDbContextLumi>(options =>
    options.UseSqlServer(conectionString));


// Inje��o de depend�ncia para o servi�o de cadastro
builder.Services.AddScoped<IUser, UserServices>();
builder.Services.AddScoped<IRole, RoleServices>();
builder.Services.AddScoped<IOpenAIService, GeminiService>();


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
