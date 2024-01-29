using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using SellSavvy.API.Service;
using SellSavvy.Domain.Identity;
using SellSavvy.Persistence.Contexts;
using System.Text;
using SellSavvy.Domain.Validators;
using SellSavvy.API.Validators;
using FluentValidation.AspNetCore;
using SellSavvy.API.Services;

var builder = WebApplication.CreateBuilder(args);

//Add fluent validation

// Add services to the container.
builder.Services.AddDbContext<SellSavvyIdentityContext>(options =>
{
    //options.UseNpgsql(ConfigurationsDb.GetString("ConnectionStrings:PostgreSQL"));
    options.UseNpgsql("Server=91.151.83.102;Port=5432;Database=Anil_Akpinar_Test1.3;User Id=hakanamaratliteam;Password=oyi6IkdAQ*pL7qutMo4Q4FgpL;");
    //"Server=91.151.83.102;Port=5432;Database=Anil_Akpinar_Test1.3;User,Id=ahmetkokteam;Password=obXRMG*U6rJ4R0cbHszpgEuFd"
});

builder.Services.AddOutputCache(options =>
{
    options.AddBasePolicy(builder =>
    {
        builder.Expire(TimeSpan.FromSeconds(10));
    });
});


builder.Services.AddIdentity<Person,Role>(options =>
{
    options.Password.RequiredLength = 3;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequireDigit = false;
    options.Password.RequireLowercase = false;
    options.Password.RequireUppercase = false;
    options.User.RequireUniqueEmail = true;
}).AddEntityFrameworkStores<SellSavvyIdentityContext>()
                .AddDefaultTokenProviders();

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters()
    {
        ValidateActor = true,
        ValidateIssuer = true,
        ValidateAudience = true,
        RequireExpirationTime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = builder.Configuration.GetSection("Jwt:Issuer").Value,
        ValidAudience = builder.Configuration.GetSection("Jwt:Audience").Value,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration.GetSection("Jwt:Key").Value))

    };
}
);


builder.Services.AddScoped<IAuthService, AuthService>();
//Fluent Validation Injection
builder.Services.AddControllersWithViews()
    .AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<CategoryPostValidator>()
                                   .RegisterValidatorsFromAssemblyContaining<EntityBaseValidator>()
                                   );


builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddSingleton<RegisterService>(new RegisterService());
builder.Services.AddSingleton<RequestCountService>(new RequestCountService());


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//builder.Services.AddDbContext<SellSavvyIdentityContext>();


app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();
app.UseOutputCache();

app.MapControllers();

app.Run();
     