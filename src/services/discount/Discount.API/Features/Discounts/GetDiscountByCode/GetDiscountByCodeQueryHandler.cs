using Discount.API.Repositories;
using Shared.Services;

namespace Discount.API.Features.Discounts.GetDiscountByCode;

public class GetDiscountByCodeQueryHandler(AppDbContext context,IIdentityService identityService) : IRequestHandler<GetDiscountByCodeQuery,ServiceResult<GetDiscountByCodeQueryResponse>>
{
    public async Task<ServiceResult<GetDiscountByCodeQueryResponse>> Handle(GetDiscountByCodeQuery request, CancellationToken cancellationToken) {
        var hasDiscount = await context.Discounts.SingleOrDefaultAsync(x => x.Code == request.Code);

        if (hasDiscount == null)
        {
            return ServiceResult<GetDiscountByCodeQueryResponse>.Error("Discount not found", HttpStatusCode.NotFound);
        }

        if (hasDiscount.Expired < DateTime.Now)
        {
            return ServiceResult<GetDiscountByCodeQueryResponse>.Error("Discount is Expired", HttpStatusCode.NotFound);

        }

        return ServiceResult<GetDiscountByCodeQueryResponse>.SuccessAsOk(new GetDiscountByCodeQueryResponse(hasDiscount.Code,hasDiscount.Rate));

    }
}