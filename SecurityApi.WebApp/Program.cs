using Microsoft.IdentityModel.Tokens;
using SecurityApi.Core.Security.Extensions;
using SecurityApi.Core.Security.Middlewares;
using SecurityApi.Infrastructure.Data.Extensions;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddJwtSecurity(builder.Configuration);
builder.Services.AddMockRepositories();
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
app.UseMiddleware<TokenMiddleware>();
app.MapControllers();

app.Run();
