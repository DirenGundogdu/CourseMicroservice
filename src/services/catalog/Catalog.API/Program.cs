using Catalog.API;
using Catalog.API.Features.Categories;
using Catalog.API.Features.Courses;
using Catalog.API.Options;
using Catalog.API.Repositories;
using Shared.Extensions;

var builder = WebApplication.CreateBuilder(args);

// builder.Services.AddTransient()

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddOptionsExt();

builder.Services.AddDatabaseServiceExt();

builder.Services.AddCommonServiceExt(typeof(CatalogAssembly));
builder.Services.AddVersioningExt();

var app = builder.Build();

app.AddSeedDataExt().ContinueWith(x => Console.WriteLine(x.IsFaulted ? x.Exception?.Message : "Seed data has been added"));
app.AddCategoryGroupEndpointExt(app.AddVersionSetExt()); 
app.AddCourseGroupEndpointExt(app.AddVersionSetExt());

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.Run();
