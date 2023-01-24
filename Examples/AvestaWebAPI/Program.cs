using Avesta.Repository.EntityRepository;
using Avesta.Repository.EntityRepositoryRepository;
using AvestaWebAPI.Data;
using AvestaWebAPI.Model;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


builder.Services.AddDbContext<AppDbContext>(opt => opt.UseSqlite("Data source = avesta_app.db"));
builder.Services.AddScoped<IRepository<AvestaCrudEntity>, EntityRepository<AvestaCrudEntity, AppDbContext>>();


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
