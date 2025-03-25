using AutoMapper;
using Catalog.API.Features.Categories.DTOs;

namespace Catalog.API.Features.Categories;

public class CategoryMapping:Profile
{
    public CategoryMapping()
    {
        CreateMap<Category, CategoryDto>().ReverseMap();
    }
}