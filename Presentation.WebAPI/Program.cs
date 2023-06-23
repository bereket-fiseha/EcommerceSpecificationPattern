using Application.Interface;
using Application.Service;
using Autofac;
using Autofac.Core;
using Domain.Interface.Repository.Common;
using Infrastructure.Repository.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using AutoMapper;
using System.Text.Json.Serialization;
using Domain.Interface.DomainLogic;
using Domain.DomainLogic;
using Microsoft.AspNetCore.Mvc;
using ActionFilters.ActionFilters;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddApiVersioning(config =>
{
    // Specify the default API Version as 1.0
    config.DefaultApiVersion = new ApiVersion(1, 0);
    // If the client hasn't specified the API version in the request, use the default API version number 
    config.AssumeDefaultVersionWhenUnspecified = true;
    // Advertise the API versions supported for the particular endpoint
    config.ReportApiVersions = true;
});
builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
}); 
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<ECDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("ECDB"));

});
builder.Services.AddScoped<ValidationFilterAttribute>();
builder.Services.AddScoped(typeof(IGenericRepository<>),typeof(GenericRepository<>));

builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();


builder.Services.AddScoped<IItemService, ItemService>();

builder.Services.AddScoped<IItemCategoryService, ItemCategoryService>();
builder.Services.AddScoped<ITaxService, TaxService>();

builder.Services.AddScoped<IOrderCartService, OrderCartService>();
builder.Services.AddScoped<IItemRelatedLogic, ItemRelatedLogic>();
builder.Services.AddScoped<IOrderDetailService, OrderDetailService>();


builder.Services.AddScoped<ICustomerService, CustomerService>();

//builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());
//builder.Host.ConfigureContainer<Autofac.ContainerBuilder>(autofacConfigure =>
//{
//    autofacConfigure
//    .RegisterModule(new );

//}); 
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
