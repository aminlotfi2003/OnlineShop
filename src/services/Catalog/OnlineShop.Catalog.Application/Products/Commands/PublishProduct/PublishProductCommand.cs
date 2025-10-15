using MediatR;
using OnlineShop.Catalog.Application.Products.Dtos;

namespace OnlineShop.Catalog.Application.Products.Commands.PublishProduct;

public sealed record PublishProductCommand(Guid ProductId) : IRequest<ProductDto>;
