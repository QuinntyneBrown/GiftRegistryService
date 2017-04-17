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
    public class AddOrUpdateProductCommand
    {
        public class AddOrUpdateProductRequest : IRequest<AddOrUpdateProductResponse>
        {
            public ProductApiModel Product { get; set; }
            public Guid TenantUniqueId { get; set; }
        }

        public class AddOrUpdateProductResponse { }

        public class AddOrUpdateProductHandler : IAsyncRequestHandler<AddOrUpdateProductRequest, AddOrUpdateProductResponse>
        {
            public AddOrUpdateProductHandler(GiftRegistryServiceContext context, ICache cache)
            {
                _context = context;
                _cache = cache;
            }

            public async Task<AddOrUpdateProductResponse> Handle(AddOrUpdateProductRequest request)
            {
                var entity = await _context.Products
                    .Include(x => x.Tenant)
                    .SingleOrDefaultAsync(x => x.Id == request.Product.Id && x.Tenant.UniqueId == request.TenantUniqueId);
                
                if (entity == null) {
                    var tenant = await _context.Tenants.SingleAsync(x => x.UniqueId == request.TenantUniqueId);
                    _context.Products.Add(entity = new Product() { TenantId = tenant.Id });
                }

                entity.Name = request.Product.Name;
                
                await _context.SaveChangesAsync();

                return new AddOrUpdateProductResponse();
            }

            private readonly GiftRegistryServiceContext _context;
            private readonly ICache _cache;
        }

    }

}
