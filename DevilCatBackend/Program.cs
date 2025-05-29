using DevilCatBackend.Data;
using DevilCatBackend.Models;
using DevilCatBackend.Services;
using Microsoft.AspNetCore.OData;
using Microsoft.EntityFrameworkCore;
using Microsoft.OData.ModelBuilder;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<ProductDbContext>(options
    => options.UseNpgsql(builder.Configuration["ConnectionStrings:product"]));

var odataBuilder = new ODataConventionModelBuilder();
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
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<ICategoryService, CategoryService>();

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

    var product = service.GetRequiredService<ProductDbContext>();
    product.Database.EnsureCreated();
    DbInitializer.InitProduct(product);
}

app.UseAuthorization();

app.MapControllers();

app.Run();
