using System.Net;
using Microsoft.AspNetCore.Http;

namespace Shared.Extensions;

public static class EndpointResultExt
{
   public static IResult ToGenericResult<T>(this ServiceResult<T> result) {
      return result.Status switch {
         HttpStatusCode.OK => Results.Ok(result.Data),
         HttpStatusCode.Created => Results.Created(result.UrlAsCreated, result.Data),
         HttpStatusCode.NotFound => Results.NotFound(result.Fail!),
         _ => Results.Problem(result.Fail!)
      };

      // return result.IsSuccess
      //    ? Results.Ok(result.Data)
      //    : Results.Problem(result.Fail!.Detail, statusCode: (int)result.Status);

   }
   
   public static IResult ToGenericResult(this ServiceResult result) {
      return result.Status switch {
         HttpStatusCode.NoContent => Results.NoContent(),
         HttpStatusCode.NotFound => Results.NotFound(result.Fail!),
         _ => Results.Problem(result.Fail!)
      };
   }
}