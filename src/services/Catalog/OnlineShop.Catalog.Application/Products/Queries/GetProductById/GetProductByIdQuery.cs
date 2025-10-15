using MediatR;
using OnlineShop.Catalog.Application.Products.Dtos;

namespace OnlineShop.Catalog.Application.Products.Queries.GetProductById;

public sealed record GetProductByIdQuery(Guid Id) : IRequest<ProductDto>;
