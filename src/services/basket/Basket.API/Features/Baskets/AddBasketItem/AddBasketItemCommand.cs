using MediatR;
using Shared;

namespace Basket.API.Features.Baskets.AddBasketItem;

public record AddBasketItemCommand(Guid CourseId, string CourseName, decimal CoursePrice, string? ImageUrl):IRequestByServiceResult;
