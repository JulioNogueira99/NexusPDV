using FluentValidation;
using NexusPDV.Application.InputModels;

namespace NexusPDV.Application.Validators
{
    public class PlaceOrderValidator : AbstractValidator<PlaceOrderInputModel>
    {
        public PlaceOrderValidator()
        {
            RuleFor(x => x.CustomerId)
                .GreaterThan(0)
                .WithMessage("O ID do cliente é inválido.");

            RuleFor(x => x.Items)
                .NotEmpty()
                .WithMessage("O pedido deve conter pelo menos um item.");

            RuleForEach(x => x.Items).SetValidator(new OrderItemValidator());
        }
    }

    public class OrderItemValidator : AbstractValidator<OrderItemInputModel>
    {
        public OrderItemValidator()
        {
            RuleFor(x => x.ProductId)
                .GreaterThan(0)
                .WithMessage("Produto inválido.");

            RuleFor(x => x.Quantity)
                .GreaterThan(0)
                .WithMessage("A quantidade deve ser maior que zero.");
        }
    }
}