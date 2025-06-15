using FluentValidation;
using test_webapi.Models;

namespace test_webapi.Validators
{
    public class BookValidator : AbstractValidator<Book>
    {
        public BookValidator()
        {
            RuleFor(x => x.Title)
                .NotEmpty().WithMessage("العنوان مطلوب")
                .MaximumLength(200).WithMessage("العنوان يجب ألا يتجاوز 200 حرف");

            RuleFor(x => x.ISBN)
                .NotEmpty().WithMessage("الرقم التسلسلي مطلوب")
                .Matches(@"^(?=(?:\D*\d){10}(?:(?:\D*\d){3})?$)[\d-]+$")
                .WithMessage("الرقم التسلسلي غير صحيح");

            RuleFor(x => x.PublicationYear)
                .NotEmpty().WithMessage("سنة النشر مطلوبة")
                .InclusiveBetween(1800, 2100).WithMessage("سنة النشر يجب أن تكون بين 1800 و 2100");

            RuleFor(x => x.AuthorId)
                .NotEmpty().WithMessage("المؤلف مطلوب");

            RuleFor(x => x.CategoryId)
                .NotEmpty().WithMessage("التصنيف مطلوب");
        }
    }
}
