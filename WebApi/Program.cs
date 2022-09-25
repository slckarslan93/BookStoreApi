using Microsoft.EntityFrameworkCore;
using WebApi.DBOperations;
using WebApi.Middlewares;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddDbContext<BookStoreDbContext>(opt =>
{
    opt.UseSqlServer("server=.;database = BookStoreDb; integrated security = true;");
});



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

//app.UseCustomExceptionMiddleware();

app.MapControllers();

app.Run();
