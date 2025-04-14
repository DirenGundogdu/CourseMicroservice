using Discount.API.Repositories;
using Shared.Services;

namespace Discount.API.Features.Discounts.Create;

public class CreateDiscountCommandHandler(AppDbContext context) : IRequestHandler<CreateDiscountCommand,ServiceResult>
{
    public async Task<ServiceResult> Handle(CreateDiscountCommand request, CancellationToken cancellationToken) {
        
        var hasCodeForUser =  await context.Discounts.AnyAsync(x => x.UserId == request.UserId && x.Code == request.Code,cancellationToken:cancellationToken);

        if (hasCodeForUser)
        {
            return  ServiceResult.Error( "Discount already exists for this user",HttpStatusCode.BadRequest);
        }
        
        var discount = new Discount() {
            Id = NewId.NextSequentialGuid(),
            Code = request.Code,
            Rate = request.Rate,
            UserId = request.UserId,
            Created = DateTime.Now,
            Expired = request.Expired
        };
        await context.Discounts.AddAsync(discount,cancellationToken);
        await context.SaveChangesAsync(cancellationToken);
        return ServiceResult.SuccessAsNoContent();
    }
}