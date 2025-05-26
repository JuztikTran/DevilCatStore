using backend.Data;
using backend.Models;
using backend.Services;
using backend.Shared.Utils;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.OData;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OData.ModelBuilder;
using Microsoft.OpenApi.Models;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<UserDbContext>(
    options => options.UseNpgsql(builder.Configuration["ConnectionStrings:userDb"]));
builder.Services.AddDbContext<ProductDbContext>(
    options => options.UseNpgsql(builder.Configuration["ConnectionStrings:productDb"]));

var odataBuilder = new ODataConventionModelBuilder();
odataBuilder.EntitySet<Account>("Account");
odataBuilder.EntitySet<Category>("Category");
odataBuilder.EntitySet<CategoryItem>("CategoryItem");
odataBuilder.EntitySet<Product>("Product");
odataBuilder.EntitySet<ProductImages>("ProductImages");
odataBuilder.EntitySet<ProductVarriant>("ProductVarriant");
builder.Services.AddControllers()
    .AddOData(options => options
        .SetMaxTop(100)
        .Filter()
        .OrderBy()
        .Count()
        .Expand()
        .Select()
        .EnableQueryFeatures()
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

//// Get configuring JWT from appsettings.json
var jwtSettings = builder.Configuration.GetSection("JwtConfig");
var key = Encoding.UTF8.GetBytes(jwtSettings["Key"]!);
var username = builder.Configuration["Admin:username"];
var password = builder.Configuration["Admin:password"];
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
builder.Services.AddAuthorization();

builder.Services.AddScoped<ItokenService, TokenService>();
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IAccountService, AccountService>();
builder.Services.AddScoped<IProfileService, ProfileService>();
builder.Services.AddScoped<ICategoryService, CategoryService>();
builder.Services.AddScoped<IProductService, ProductService>();

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
    var context = service.GetRequiredService<UserDbContext>();

    context.Database.EnsureCreated();
    DbInitializer.InitUserDb(context, username!, password!);

    var product = service.GetRequiredService<ProductDbContext>();
    product.Database.EnsureCreated();
    DbInitializer.InitProductDb(product);
}

app.UseHttpsRedirection();

app.UseODataRouteDebug();

app.UseRouting();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
