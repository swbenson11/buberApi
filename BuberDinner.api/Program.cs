
using BuberDinner.Api.Filters;
using BuberDinner.Api.Middleware;
using BuberDinner.application;
using BuberDinner.infrastructure;

var builder = WebApplication.CreateBuilder(args);

{
    builder.Services.AddBuberApplication();
    builder.Services.AddBuberInfrastructure(builder.Configuration);

// Error option 1
    builder.Services.AddControllers(options => options.Filters.Add<ErrorHandlingFilterAttribute>());
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen();

}
var app = builder.Build();

// Error option 2
// app.UseMiddleware<ErrorHandlingMiddleware>();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// app.UseAuthorization();

app.MapControllers();

app.Run();
