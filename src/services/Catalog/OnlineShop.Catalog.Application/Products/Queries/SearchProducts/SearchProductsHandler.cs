using MediatR;
using OnlineShop.Catalog.Application.Common.Mapping;
using OnlineShop.Catalog.Application.Products.Dtos;
using OnlineShop.Catalog.Domain.Products;

namespace OnlineShop.Catalog.Application.Products.Queries.SearchProducts;

public sealed class SearchProductsHandler(IProductRepository repo)
    : IRequestHandler<SearchProductsQuery, IReadOnlyList<ProductDto>>
{
    public async Task<IReadOnlyList<ProductDto>> Handle(SearchProductsQuery request, CancellationToken ct)
    {
        var list = await repo.ListAsync(q =>
        {
            var query = q;
            if (!string.IsNullOrWhiteSpace(request.Term))
            {
                var t = request.Term.Trim().ToLower();
                query = query.Where(x => x.Title.ToLower().Contains(t) || x.Sku.ToLower().Contains(t));
            }
            return query
                .OrderByDescending(x => x.PublishedAt ?? x.CreatedAt)
                .Skip((request.Page - 1) * request.PageSize)
                .Take(request.PageSize);
        }, ct);

        return list.Select(p => p.ToDto()).ToList();
    }
}
