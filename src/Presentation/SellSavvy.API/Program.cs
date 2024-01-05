using System.Reflection;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.EntityFrameworkCore;
using SellSavvy.Domain.Entities;
using SellSavvy.Domain.Validators;
using SellSavvy.Persistence.Configurations;
using SellSavvy.Persistence.Contexts;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<SellSavvyIdentityContext>(options =>
{
    //options.UseNpgsql(ConfigurationsDb.GetString("ConnectionStrings:PostgreSQL"));
    options.UseNpgsql("Server=91.151.83.102;Port=5432;Database=Anil_Akpinar_Test1.3;User,Id=ahmetkokteam;Password=obXRMG*U6rJ4R0cbHszpgEuFd");
});



builder.Services.AddControllers().AddFluentValidation(c=> c.RegisterValidatorsFromAssembly(Assembly.GetExecutingAssembly()));
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
