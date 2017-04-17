using MediatR;
using GiftRegistryService.Data;
using GiftRegistryService.Features.Core;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using System.Data.Entity;

namespace GiftRegistryService.Features.Products
{
    public class GetProductsQuery
    {
        public class GetProductsRequest : IRequest<GetProductsResponse> { 
            public Guid TenantUniqueId { get; set; }       
        }

        public class GetProductsResponse
        {
            public ICollection<ProductApiModel> Products { get; set; } = new HashSet<ProductApiModel>();
        }

        public class GetProductsHandler : IAsyncRequestHandler<GetProductsRequest, GetProductsResponse>
        {
            public GetProductsHandler(GiftRegistryServiceContext context, ICache cache)
            {
                _context = context;
                _cache = cache;
            }

            public async Task<GetProductsResponse> Handle(GetProductsRequest request)
            {
                var products = await _context.Products
                    .Include(x => x.Tenant)
                    .Where(x => x.Tenant.UniqueId == request.TenantUniqueId )
                    .ToListAsync();

                return new GetProductsResponse()
                {
                    Products = products.Select(x => ProductApiModel.FromProduct(x)).ToList()
                };
            }

            private readonly GiftRegistryServiceContext _context;
            private readonly ICache _cache;
        }

    }

}
