using OpenAiApi.Contracts;
using OpenAiApi.Database;
using OpenAiApi.Services;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddHttpClient();
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

#region Services Dependencies

builder.Services.AddScoped<IDatabaseService, DatabaseService>();
builder.Services.AddScoped<IOpenAiService, OpenAiService>();

#endregion Services Dependencies

#region Datasbase

var migrationAssemblyName = typeof(Program).GetTypeInfo().Assembly.GetName().Name;
builder.Services.AddSqlServerDatabase(builder.Configuration.GetConnectionString("AzureDbConnection"), migrationAssemblyName);

#endregion Datasbase

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