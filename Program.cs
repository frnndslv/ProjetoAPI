
using System.Text.Json;
using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

//Configuração Swagger no builder
//http://localhost:porta/swagger/index.html
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Configura a serialização do JSON para evitar referência circular
builder.Services.Configure<Microsoft.AspNetCore.Http.Json.JsonOptions>(
  options => options.SerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles
);

//Configuração banco MySQL
builder.Services.AddDbContext<BancoDeDados>();

var app = builder.Build();

//Configuração Swagger no app
app.UseSwagger();
app.UseSwaggerUI(opt =>
    {
      opt.SwaggerEndpoint("/swagger/v1/swagger.json", "Estoque");
    });

//APIs
app.MapGet("/", () => "APIs relacionadas com EF + Swagger");
app.MapFuncionarioApi();
app.MapProdutoApi();
app.MapClienteApi();
app.MapFornecedorApi();

app.Run();