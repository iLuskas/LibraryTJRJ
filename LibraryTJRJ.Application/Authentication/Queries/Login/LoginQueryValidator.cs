﻿using FluentValidation;

namespace LibraryTJRJ.Application.Authentication.Queries.Login;

public class LoginQueryValidator : AbstractValidator<LoginQuery>
{
    public LoginQueryValidator()
    {
        RuleFor(r => r.Email).NotEmpty();
        RuleFor(r => r.Password).NotEmpty();
    }
}
