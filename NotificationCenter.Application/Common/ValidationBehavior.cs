using FluentValidation;
using MediatR;
using ValidationException = System.ComponentModel.DataAnnotations.ValidationException;

namespace NotificationCenter.Application.Common;

public sealed class ValidationBehavior<TRequest, TResponse>(IEnumerable<IValidator<TRequest>> validators)
    : IPipelineBehavior<TRequest, TResponse>
    where TRequest : notnull
{
    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken ct)
    {
        var failures = await Validate(request, ct);
        if (failures.Count == 0)
            return await next(ct);

        var details = failures
            .GroupBy(f => f.PropertyName)
            .ToDictionary(g => g.Key, g => g.Select(f => f.ErrorMessage).ToArray());

        if (typeof(TResponse) == typeof(Result))
        {
            var failure = Result.BadRequest("Validation failed", details);
            return (TResponse)(object)failure;
        }

        if (!typeof(TResponse).IsGenericType ||
            typeof(TResponse).GetGenericTypeDefinition() != typeof(Result<>))
            throw new InvalidOperationException(
                "How did we get here? Please check if your handler is configured correctly.");
        {
            var valueType = typeof(TResponse).GetGenericArguments()[0];

            var closed = typeof(Result<>).MakeGenericType(valueType);
            var method = closed.GetMethod(nameof(Result<object>.BadRequest),
                [typeof(string), typeof(object), typeof(string)]);

            var failure = method!.Invoke(null, ["Validation failed", details, "Validation"]);
            return (TResponse)failure!;
        }

    }

    private async Task<List<FluentValidation.Results.ValidationFailure>> Validate(TRequest request, CancellationToken ct)
    {
        if (!validators.Any()) return [];
        var ctx = new ValidationContext<TRequest>(request);
        var results = await Task.WhenAll(validators.Select(v => v.ValidateAsync(ctx, ct)));
        return results.SelectMany(r => r.Errors).Where(e => e is not null).ToList();
    }
}
