
using BuberDinner.Api.Filters;
using BuberDinner.Api.Middleware;
using BuberDinner.application;
using BuberDinner.infrastructure;

var builder = WebApplication.CreateBuilder(args);

{
    builder.Services.AddBuberApplication();
    builder.Services.AddBuberInfrastructure(builder.Configuration);

// Error option 1
    // builder.Services.AddControllers(options => options.Filters.Add<ErrorHandlingFilterAttribute>());
    builder.Services.AddControllers();
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

// Error option 3
// Adds a middleware to the pipeline that will catch exceptions, log them, reset the request path, 
// and re-execute the request. The request will not be re-executed if the response has already started.
app.UseExceptionHandler("/error");
app.UseHttpsRedirection();

// app.UseAuthorization();

app.MapControllers();

app.Run();
