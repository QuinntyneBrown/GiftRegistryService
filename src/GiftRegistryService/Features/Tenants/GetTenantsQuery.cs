using MediatR;
using GiftRegistryService.Data;
using GiftRegistryService.Features.Core;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using System.Data.Entity;

namespace GiftRegistryService.Features.Tenants
{
    public class GetTenantsQuery
    {
        public class GetTenantsRequest : IRequest<GetTenantsResponse> { 
            public Guid TenantUniqueId { get; set; }       
        }

        public class GetTenantsResponse
        {
            public ICollection<TenantApiModel> Tenants { get; set; } = new HashSet<TenantApiModel>();
        }

        public class GetTenantsHandler : IAsyncRequestHandler<GetTenantsRequest, GetTenantsResponse>
        {
            public GetTenantsHandler(GiftRegistryServiceContext context, ICache cache)
            {
                _context = context;
                _cache = cache;
            }

            public async Task<GetTenantsResponse> Handle(GetTenantsRequest request)
            {
                var tenants = await _context.Tenants                    
                    .ToListAsync();

                return new GetTenantsResponse()
                {
                    Tenants = tenants.Select(x => TenantApiModel.FromTenant(x)).ToList()
                };
            }

            private readonly GiftRegistryServiceContext _context;
            private readonly ICache _cache;
        }

    }

}
