using Microsoft.EntityFrameworkCore;
using ProjetoFaculdade6Semestre;
using ProjetoFaculdade6Semestre.Interface.AI;
using ProjetoFaculdade6Semestre.Interfaces;
using ProjetoFaculdade6Semestre.Service;
using ProjetoFaculdade6Semestre.Service.AIService;
using ProjetoFaculdade6Semestre.Controllers;
using ProjetoFaculdade6Semestre.Interface;

var builder = WebApplication.CreateBuilder(args);

// configuração do APPDbContext com SQL Server
var conectionString = builder.Configuration.GetConnectionString("ConectFacul");

builder.Services.AddDbContext<AppDbContextLumi>(options =>
    options.UseSqlServer(conectionString));

// Injeção de dependência para os serviços
builder.Services.AddScoped<IUser, UserServices>();
builder.Services.AddScoped<IRole, RoleServices>();
builder.Services.AddScoped<RoleServices>();
builder.Services.AddScoped<ICv, CvServices>();
builder.Services.AddScoped<IResults, ResultService>();
builder.Services.AddScoped<ICandidatura, CandidaturaService>();
builder.Services.AddScoped<IOpenAIService, GeminiService>();

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// aplica CORS
app.UseCors("AllowAll");

app.UseAuthorization();

app.MapControllers();

app.Run();
