using Microsoft.Extensions.DependencyInjection;
using VehicleRental.Repository.DbContext;
using VehicleRental.Web.Helpers;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

#region custom registrations
builder.Services.AddAutoMapper(typeof(AutoMapperProfile));
builder.Services.RegisterDependencies();
builder.Services.AddSingleton<IDapperContext, DapperContext>();
#endregion

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
