using MediatR;
using GiftRegistryService.Data;
using GiftRegistryService.Data.Model;
using GiftRegistryService.Features.Core;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using System.Data.Entity;

namespace GiftRegistryService.Features.Tenants
{
    public class RemoveTenantCommand
    {
        public class RemoveTenantRequest : IRequest<RemoveTenantResponse>
        {
            public int Id { get; set; }
            public Guid TenantUniqueId { get; set; } 
        }

        public class RemoveTenantResponse { }

        public class RemoveTenantHandler : IAsyncRequestHandler<RemoveTenantRequest, RemoveTenantResponse>
        {
            public RemoveTenantHandler(GiftRegistryServiceContext context, ICache cache)
            {
                _context = context;
                _cache = cache;
            }

            public async Task<RemoveTenantResponse> Handle(RemoveTenantRequest request)
            {
                var tenant = await _context.Tenants.SingleAsync(x=>x.Id == request.Id);
                tenant.IsDeleted = true;
                await _context.SaveChangesAsync();
                return new RemoveTenantResponse();
            }

            private readonly GiftRegistryServiceContext _context;
            private readonly ICache _cache;
        }
    }
}
