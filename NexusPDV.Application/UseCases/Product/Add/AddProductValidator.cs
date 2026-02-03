using FluentValidation;

namespace NexusPDV.Application.UseCases.Product.Add
{
    public class AddProductValidator : AbstractValidator<AddProductCommand>
    {
        public AddProductValidator()
        {
            RuleFor(x => x.Title.Trim())
                .NotEmpty()
                .WithMessage("O titulo do produto não pode ser vazio.");

            RuleFor(x => x.Price)
                .GreaterThan(0)
                .WithMessage("O preço deve ser maior que zero");

            RuleFor(x => x.StockQuantity)
                .GreaterThan(0)
                .WithMessage("Estoque inicial não pode ser negativo");
        }
    }
}
