using MediatR;
using GiftRegistryService.Data;
using GiftRegistryService.Data.Model;
using GiftRegistryService.Features.Core;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using System.Data.Entity;

namespace GiftRegistryService.Features.Products
{
    public class RemoveProductCommand
    {
        public class RemoveProductRequest : IRequest<RemoveProductResponse>
        {
            public int Id { get; set; }
            public Guid TenantUniqueId { get; set; } 
        }

        public class RemoveProductResponse { }

        public class RemoveProductHandler : IAsyncRequestHandler<RemoveProductRequest, RemoveProductResponse>
        {
            public RemoveProductHandler(GiftRegistryServiceContext context, ICache cache)
            {
                _context = context;
                _cache = cache;
            }

            public async Task<RemoveProductResponse> Handle(RemoveProductRequest request)
            {
                var product = await _context.Products.SingleAsync(x=>x.Id == request.Id && x.Tenant.UniqueId == request.TenantUniqueId);
                product.IsDeleted = true;
                await _context.SaveChangesAsync();
                return new RemoveProductResponse();
            }

            private readonly GiftRegistryServiceContext _context;
            private readonly ICache _cache;
        }
    }
}
