using FluentValidation;
using SouQna.Business.Contracts.Requests;

namespace SouQna.Business.Contracts.Validators
{
    public class AddProductRequestValidator : AbstractValidator<AddProductRequest>
    {
        public AddProductRequestValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Name is required")
                .MaximumLength(50).WithMessage("Name must not exceed 50 characters");

            RuleFor(x => x.Description)
                .NotEmpty().WithMessage("Description is required")
                .MaximumLength(1000).WithMessage("Description must not exceed 1000 characters");

            RuleFor(x => x.Image)
                .NotNull().WithMessage("Image is required")
                .Must(file =>
                {
                    if(file is null || file.Length < 4)
                        return false;

                    using var stream = file.OpenReadStream();
                    if(!stream.CanRead)
                        return false;

                    var header = new byte[4];
                    stream.ReadExactly(header);

                    bool isPng = header[0] == 0x89 && header[1] == 0x50 && header[2] == 0x4E && header[3] == 0x47;
                    bool isJpeg = header[0] == 0xFF && header[1] == 0xD8 && header[2] == 0xFF;

                    return isPng || isJpeg;
                }).WithMessage("Invalid image format. Only PNG and JPEG files are allowed");

            RuleFor(x => x.Price)
                .GreaterThan(0).WithMessage("Price cannot be negative");
        }
    }
}