using BookingTask.Middleware;
using BookingTask.Services;
using BookingTask.Services.Interfaces;
using DeskBooking;
using DeskBooking.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System.Reflection;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers().AddNewtonsoftJson(options =>
    options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
);

builder.Services.AddDbContext<SMCDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DbConnString")));
builder.Services.AddScoped<Seeder>();
builder.Services.AddAutoMapper(Assembly.GetExecutingAssembly());
builder.Services.AddScoped<ILocationService, LocationService>();
builder.Services.AddScoped<IDeskService, DeskService>();
builder.Services.AddScoped<IBookingService, BookingService>();

builder.Services.AddScoped<ErrorHandlingMiddleware>();


builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

var scope = app.Services.CreateScope();
var seeder = scope.ServiceProvider.GetRequiredService<Seeder>();
await seeder.Seed();
app.UseMiddleware<ErrorHandlingMiddleware>();
app.UseDeveloperExceptionPage();
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
