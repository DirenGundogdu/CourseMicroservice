using Microsoft.EntityFrameworkCore;
using Order.API.Endpoints.Orders;
using Order.Application;
using Order.Application.Contracts.Repositories;
using Order.Application.Contracts.UnitOfWork;
using Order.Persistence;
using Order.Persistence.Repositories;
using Order.Persistence.UnitOfWork;
using Shared.Extensions;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddCommonServiceExt(typeof(OrderApplicationAssembly));
builder.Services.AddDbContext<AppDbContext>(opt => {
    opt.UseSqlServer(builder.Configuration.GetConnectionString("SqlServer"));
});

builder.Services.AddScoped(typeof(IGenericRepository<,>),typeof(GenericRepository<,>));
builder.Services.AddScoped<IOrderRepository, OrderRepository>();
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

builder.Services.AddVersioningExt();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.AddOrderGroupEndpointExt(app.AddVersionSetExt());

app.UseHttpsRedirection();

app.Run();
