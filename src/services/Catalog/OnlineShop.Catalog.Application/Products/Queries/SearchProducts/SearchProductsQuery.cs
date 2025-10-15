using MediatR;
using OnlineShop.Catalog.Application.Products.Dtos;

namespace OnlineShop.Catalog.Application.Products.Queries.SearchProducts;

public sealed record SearchProductsQuery(string? Term, int Page = 1, int PageSize = 20) : IRequest<IReadOnlyList<ProductDto>>;
