using Emp_management.Data;
using Emp_management.DataLayer;
using Emp_management.DataLayer.Models;
using Emp_management.ServiceLayer;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Text.Json.Serialization;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

// Register DbContext
builder.Services.AddDbContext<EmployeeDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("EmpDbConnection")));

// Register custom services and repositories
builder.Services.AddScoped<EmployeeRepository>();
builder.Services.AddScoped<EmployeeService>();

// Add controllers
builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        // Configure enums to serialize as strings in JSON responses
        options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
    });

// Enable CORS for all origins (you can restrict this as needed later)
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});

// Configure Swagger/OpenAPI
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "Employee Management API",
        Version = "v1"
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline
if (app.Environment.IsDevelopment())
{
    // This will display detailed error messages in development mode.
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Employee Management API V1");
    });
}
else
{
    // If not in development, use a generic error page
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

// Enable CORS
app.UseCors("AllowAll");

// Middleware configurations
app.UseHttpsRedirection();

// You can log the errors globally using a custom error-handling middleware if needed
app.UseAuthorization();

app.MapControllers();

app.Run();
