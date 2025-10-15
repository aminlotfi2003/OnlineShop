using MediatR;
using OnlineShop.Catalog.Application.Products.Dtos;

namespace OnlineShop.Catalog.Application.Products.Commands.ChangePrice;

public sealed record ChangePriceCommand(Guid ProductId, decimal Price, string Currency = "IRR") : IRequest<ProductDto>;
