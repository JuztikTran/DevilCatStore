using DevilCatBackend.Data;
using DevilCatBackend.Models;
using DevilCatBackend.Services;
using DevilCatBackend.Shared.Utils;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.OData;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OData.ModelBuilder;
using Microsoft.OpenApi.Models;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<UserDbContext>(options
    => options.UseNpgsql(builder.Configuration["ConnectionStrings:user"]));

builder.Services.AddDbContext<ProductDbContext>(options
    => options.UseNpgsql(builder.Configuration["ConnectionStrings:product"]));

var odataBuilder = new ODataConventionModelBuilder();
odataBuilder.EntitySet<Account>("Account");
odataBuilder.EntitySet<Address>("Address");
odataBuilder.EntitySet<Category>("Category");
builder.Services.AddControllers()
    .AddOData(options => options
        .SetMaxTop(100)
        .Filter()
        .OrderBy()
        .Count()
        .Expand()
        .Select()
        .AddRouteComponents("odata", odataBuilder.GetEdmModel())
        );
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    var jwtSecuritySchema = new OpenApiSecurityScheme
    {
        BearerFormat = "JWT",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.Http,
        Scheme = JwtBearerDefaults.AuthenticationScheme,
        Description = "Enter your JWT Access Token",
        Reference = new OpenApiReference
        {
            Id = JwtBearerDefaults.AuthenticationScheme,
            Type = ReferenceType.SecurityScheme
        }
    };

    options.AddSecurityDefinition("Bearer", jwtSecuritySchema);
    options.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        { jwtSecuritySchema, Array.Empty<string>() }
    });
});

var jwtSettings = builder.Configuration.GetSection("JwtConfig");
var key = Encoding.UTF8.GetBytes(jwtSettings["Key"]!);

// Config Authentication with JWT
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
{
    options.RequireHttpsMetadata = false;
    options.SaveToken = true;
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = builder.Configuration["JwtConfig:Issuer"],
        ValidAudience = builder.Configuration["JwtConfig:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(key),
        ClockSkew = TimeSpan.Zero
    };
});
// Add configuring Authorization
builder.Services.AddAuthorization();

builder.Services.AddScoped<ITokenService, TokenService>();
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IAccountService, AccountService>();
builder.Services.AddScoped<ICategoryService, CategoryService>();

var account = new Account
{
    ID = Guid.NewGuid().ToString("N"),
    UserName = builder.Configuration["AccountData:username"]!,
    Password = BCrypt.Net.BCrypt.HashPassword(builder.Configuration["AccountData:password"]!),
    FirstName = builder.Configuration["AccountData:firstName"]!,
    LastName = builder.Configuration["AccountData:lastName"]!,
    Email = builder.Configuration["AccountData:email"]!,
    AccountType = "Local",
    Role = "Admin",
    GoogleID = "",
    FacebookeID = "",
    CreateAt = DateTime.Now,
    UpdateAt = DateTime.Now,
};

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

using (var scope = app.Services.CreateScope())
{
    var service = scope.ServiceProvider;

    var user = service.GetRequiredService<UserDbContext>();
    user.Database.EnsureCreated();
    DbInitializer.InitUser(user, account);

    var product = service.GetRequiredService<ProductDbContext>();
    product.Database.EnsureCreated();
    DbInitializer.InitProduct(product);
}


app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
