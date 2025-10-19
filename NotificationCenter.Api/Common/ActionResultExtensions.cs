using Microsoft.AspNetCore.Mvc;
using NotificationCenter.Application.Common;

namespace NotificationCenter.Api.Common;

public static class ActionResultExtensions
{
    public static ActionResult ToActionResult(this Result res, ControllerBase ctrl)
        => res.IsSuccess
            ? ctrl.NoContent()
            : ctrl.ToProblemResult(res.Error);

    public static ActionResult<T> ToActionResult<T>(this Result<T> res, ControllerBase ctrl)
        => res.IsSuccess
            ? ctrl.Ok(res.Value)
            : ctrl.ToProblemResult<T>(res.Error);

    private static ActionResult ToProblemResult(this ControllerBase ctrl, Error err)
    {
        var problem = new ProblemDetails
        {
            Title = err.Message,
            Status = (int)err.Status,
            Detail = err.Message,
        };

        problem.Extensions["code"] = err.Code;
        if (err.Details is not null)
            problem.Extensions["details"] = err.Details;

        return new ObjectResult(problem)
        {
            StatusCode = (int)err.Status
        };
    }

    private static ActionResult<T> ToProblemResult<T>(this ControllerBase ctrl, Error err)
    {
        var problem = new ProblemDetails
        {
            Title = err.Message,
            Status = (int)err.Status,
            Detail = err.Message,
        };

        problem.Extensions["code"] = err.Code;
        if (err.Details is not null)
            problem.Extensions["details"] = err.Details;

        return new ObjectResult(problem)
        {
            StatusCode = (int)err.Status
        };
    }
}