using FluentValidation;
using test_webapi.Models;

namespace test_webapi.Validators
{
    public class CategoryValidator : AbstractValidator<Category>
    {
        public CategoryValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("اسم التصنيف مطلوب")
                .MaximumLength(50).WithMessage("اسم التصنيف يجب ألا يتجاوز 50 حرف");

            RuleFor(x => x.Description)
                .MaximumLength(500).WithMessage("الوصف يجب ألا يتجاوز 500 حرف");
        }
    }
}
