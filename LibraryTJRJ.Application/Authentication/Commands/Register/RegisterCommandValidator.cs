using FluentValidation;

namespace LibraryTJRJ.Application.Authentication.Commands.Register;

public class RegisterCommandValidator : AbstractValidator<RegisterCommand>
{
    public RegisterCommandValidator()
    {
        
        RuleFor(r => r.FirstName)
            .NotEmpty()
            .MaximumLength(100);
        
        RuleFor(r => r.LastName)
            .NotEmpty()
            .MaximumLength(100);
       
        RuleFor(c => c.Email).EmailAddress();
        
        RuleFor(r => r.Password)
            .NotEmpty()
            .MinimumLength(5)
            .MaximumLength(50);
    }
}
