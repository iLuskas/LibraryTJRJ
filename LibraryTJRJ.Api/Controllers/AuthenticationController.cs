using ErrorOr;
using LibraryTJRJ.Application.Authentication.Commands.Register;
using LibraryTJRJ.Application.Authentication.Common;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using LibraryTJRJ.Domain.Common.Errors;
using LibraryTJRJ.Contracts.Authentication;
using LibraryTJRJ.Application.Authentication.Queries.Login;

namespace LibraryTJRJ.Api.Controllers;

[ApiVersion(ApiVersions.V1)]
[Route("api/v{version:apiVersion}/auth")]
[AllowAnonymous]
public class AuthenticationController : ApiController
{
    private readonly ISender _mediator;

    public AuthenticationController(ISender mediator)
    {
        _mediator = mediator;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register(RegisterRequest request)
    {
        var command = new RegisterCommand(
            FirstName: request.FirstName,
            LastName: request.LastName,
            Email: request.Email,
            Password: request.Password
        );

        ErrorOr<AuthenticationResult> authResult = await _mediator.Send(command);

        return authResult.Match(
            authResult => Ok(new AuthenticationResponse(
                Id: authResult.User.Id,
                FirstName: authResult.User.FirstName,
                LastName: authResult.User.LastName,
                Email: authResult.User.Email,
                Token: authResult.Token
                )
            ),
            errors => Problem(errors)
            );
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login(LoginRequest request)
    {
        var query = new LoginQuery(
            Email: request.Email,
            Password: request.Password
        );

        ErrorOr<AuthenticationResult> authResult = await _mediator.Send(query);

        if (authResult.IsError && authResult.FirstError == Errors.Authentication.InvalidCredentials)
            return Problem(statusCode: StatusCodes.Status401Unauthorized, title: authResult.FirstError.Description);

        return authResult.Match(
            authResult => Ok(new AuthenticationResponse(
                    Id: authResult.User.Id, 
                    FirstName: authResult.User.FirstName, 
                    LastName: authResult.User.LastName, 
                    Email: authResult.User.Email,
                    Token: authResult.Token
                )
            ),
            errors => Problem(errors)
        );
    }
}
