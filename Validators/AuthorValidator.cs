using FluentValidation;
using test_webapi.Models;

namespace test_webapi.Validators
{
    public class AuthorValidator : AbstractValidator<Author>
    {
        public AuthorValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("اسم المؤلف مطلوب")
                .MaximumLength(100).WithMessage("اسم المؤلف يجب ألا يتجاوز 100 حرف");

            RuleFor(x => x.Biography)
                .MaximumLength(2000).WithMessage("السيرة الذاتية يجب ألا تتجاوز 2000 حرف");
        }
    }
}
