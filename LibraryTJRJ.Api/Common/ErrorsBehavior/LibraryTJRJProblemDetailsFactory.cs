using ErrorOr;
using LibraryTJRJ.Api.Common.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.Extensions.Options;
using System.Diagnostics;

namespace LibraryTJRJ.Api.Common.ErrorsBehavior;

public class LibraryTJRJProblemDetailsFactory : ProblemDetailsFactory
{
    private readonly ApiBehaviorOptions _options;

    public LibraryTJRJProblemDetailsFactory(IOptions<ApiBehaviorOptions> options)
    {
        _options = options?.Value ?? throw new ArgumentNullException(nameof(options));
    }

    public override ProblemDetails CreateProblemDetails(
        HttpContext httpContext,
        int? statusCode = null,
        string? title = null,
        string? type = null,
        string? detail = null,
        string? instance = null)
    {
        statusCode ??= 500;

        var problemDetails = new ProblemDetails()
        {
            Status = statusCode,
            Detail = detail,
            Title = title,
            Instance = instance,
            Type = type
        };

        ApplyProblemDetailsDefault(httpContext, problemDetails, statusCode.Value);

        return problemDetails;
    }

    public override ValidationProblemDetails CreateValidationProblemDetails(
        HttpContext httpContext,
        ModelStateDictionary modelStateDictionary,
        int? statusCode = null,
        string? title = null,
        string? type = null,
        string? detail = null,
        string? instance = null)
    {
        if (modelStateDictionary == null)
            throw new ArgumentNullException(nameof(modelStateDictionary));

        statusCode ??= 400;

        var validationProblemDetails = new ValidationProblemDetails(modelStateDictionary)
        {
            Status = statusCode,
            Detail = detail,
            Instance = instance,
            Type = type
        };

        if (!string.IsNullOrEmpty(title))
            validationProblemDetails.Title = title;

        ApplyProblemDetailsDefault(httpContext, validationProblemDetails, statusCode.Value);

        return validationProblemDetails;
    }


    private void ApplyProblemDetailsDefault(HttpContext httpContext, ProblemDetails problemDetails, int statusCode)
    {

        problemDetails.Status ??= statusCode;

        if (_options.ClientErrorMapping.TryGetValue(statusCode, out var clientErrorData))
        {
            problemDetails.Title ??= clientErrorData.Title;
            problemDetails.Type ??= clientErrorData.Link;

            if (string.IsNullOrEmpty(problemDetails.Detail))
            {
                problemDetails.Detail = "An error occurred. Please consult the API documentation or contact support.";
            }
        }

        AddTraceId(httpContext, problemDetails);

        AddErrorCodes(httpContext, problemDetails);
    }

    private void AddTraceId(HttpContext httpContext, ProblemDetails problemDetails)
    {
        var traceId = Activity.Current?.Id ?? httpContext.TraceIdentifier ?? Guid.NewGuid().ToString();
        problemDetails.Extensions["traceId"] = traceId;
    }

    private void AddErrorCodes(HttpContext httpContext, ProblemDetails problemDetails)
    {
        var errors = httpContext.Items[HttpContextItemKeys.Errors] as List<Error>;

        if (errors?.Any() == true)
        {
            problemDetails.Extensions["errorCodes"] = errors.Select(e => e.Code);
        }
    }
}
