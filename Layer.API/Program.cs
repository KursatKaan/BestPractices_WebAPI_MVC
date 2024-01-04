using FluentValidation.AspNetCore;
using Layer.API.Filters;
using Layer.API.Middlewares;
using Layer.Core.Abstract.Repositories;
using Layer.Core.Abstract.Services;
using Layer.Core.Abstract.UnitOfWorks;
using Layer.Repository;
using Layer.Repository.Repositories;
using Layer.Repository.UnitOfWorks;
using Layer.Service.Mapping;
using Layer.Service.Services;
using Layer.Service.Validations;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

// Fluent Validation
builder.Services.AddControllers(options => options.Filters.Add(new ValidateFilterAttribute())).AddFluentValidation(x=>x.RegisterValidatorsFromAssemblyContaining<ProductDtoValidator>());

// Remove Default API Fluent Validation
builder.Services.Configure<ApiBehaviorOptions>(options => options.SuppressModelStateInvalidFilter=true); //Varsayılan model bastırıldı.

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped(typeof(NotFoundFilter<>));

builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
builder.Services.AddScoped(typeof(IService<>), typeof(Service<>));
builder.Services.AddAutoMapper(typeof(MapProfile));

builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<IProductService, ProductService>();

builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
builder.Services.AddScoped<ICategoryService, CategoryService>();



builder.Services.AddDbContext<AppDbContext>(x =>
{
    x.UseSqlServer(builder.Configuration.GetConnectionString("SqlConnection"), option =>
    {
        //Options.MigrationsAssembly("Layer.Repository"); Bu Type güvenli bir durum değil. Katmanın ismi değişirse sorun çıkarır.

        option.MigrationsAssembly(Assembly.GetAssembly(typeof(AppDbContext)).GetName().Name); //Bu sayede Type güvenli hale getirdik.
    });
});



var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCustomException(); //Kendi oluşturduğumuz hata middleware'si.

app.UseAuthorization();

app.MapControllers();

app.Run();
