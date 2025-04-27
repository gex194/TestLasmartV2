using Microsoft.EntityFrameworkCore;
using TestLasmartV2.Data;
using TestLasmartV2.Repositories;
using TestLasmartV2.Repositories.CommentRepository;
using TestLasmartV2.Services.CommentService;
using TestLasmartV2.Services.PointService;

var builder = WebApplication.CreateBuilder(args);

//Adding application context
builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseInMemoryDatabase(databaseName:"TestLasmartV2"));

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();


//Registering services and repositories
builder.Services.AddScoped<IPointRepository, PointRepository>();
builder.Services.AddScoped<ICommentRepository, CommentRepository>();
builder.Services.AddScoped<IPointService, PointService>();
builder.Services.AddScoped<ICommentService, CommentService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

//app.UseAuthorization();

app.MapControllers();
app.UseDefaultFiles();
app.UseStaticFiles();

using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
    DataSeed.Seed(context);
}

app.Run();