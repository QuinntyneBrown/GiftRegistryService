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
    public class GetProductByIdQuery
    {
        public class GetProductByIdRequest : IRequest<GetProductByIdResponse> { 
            public int Id { get; set; }
            public Guid TenantUniqueId { get; set; }
        }

        public class GetProductByIdResponse
        {
            public ProductApiModel Product { get; set; } 
        }

        public class GetProductByIdHandler : IAsyncRequestHandler<GetProductByIdRequest, GetProductByIdResponse>
        {
            public GetProductByIdHandler(GiftRegistryServiceContext context, ICache cache)
            {
                _context = context;
                _cache = cache;
            }

            public async Task<GetProductByIdResponse> Handle(GetProductByIdRequest request)
            {                
                return new GetProductByIdResponse()
                {
                    Product = ProductApiModel.FromProduct(await _context.Products
                    .Include(x => x.Tenant)				
					.SingleAsync(x=>x.Id == request.Id &&  x.Tenant.UniqueId == request.TenantUniqueId))
                };
            }

            private readonly GiftRegistryServiceContext _context;
            private readonly ICache _cache;
        }

    }

}
