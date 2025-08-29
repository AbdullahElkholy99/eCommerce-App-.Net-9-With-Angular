using Ecom.API.Middleware;
using Ecom.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

// ✅ Register Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.InfrastructureConfiguration(builder.Configuration);

// ✅ Register AutoMapper
builder.Services.AddAutoMapper(typeof(Program));

// Register MemoryCache
builder.Services.AddMemoryCache();

// Register CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("CORSPolicy", policy =>
    {
        policy.WithOrigins("https://localhost:4200")
              .AllowAnyHeader()
              .AllowAnyMethod()
              .AllowCredentials();
    });
});



var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    //app.MapOpenApi();
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Enable CORS
app.UseCors("CORSPolicy");

// Use custom exception handling middleware
app.UseMiddleware<ExceptionsMiddleware>();

// Handle status code pages
app.UseStatusCodePagesWithReExecute("/Errors/{0}");

// Redirect HTTP to HTTPS
app.UseHttpsRedirection();

// Enable authentication and authorization middleware
app.UseAuthorization();

// Map controller routes
app.MapControllers();

// Initialize the database
app.Run();
