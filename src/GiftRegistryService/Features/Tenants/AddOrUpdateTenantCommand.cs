using GiftRegistryService.Data;
using GiftRegistryService.Data.Model;
using GiftRegistryService.Features.Core;
using MediatR;
using System;
using System.Threading.Tasks;
using System.Data.Entity;

namespace GiftRegistryService.Features.Tenants
{
    public class AddOrUpdateTenantCommand
    {
        public class AddOrUpdateTenantRequest : IRequest<AddOrUpdateTenantResponse>
        {
            public TenantApiModel Tenant { get; set; }
        }

        public class AddOrUpdateTenantResponse { }

        public class AddOrUpdateTenantHandler : IAsyncRequestHandler<AddOrUpdateTenantRequest, AddOrUpdateTenantResponse>
        {
            public AddOrUpdateTenantHandler(GiftRegistryServiceContext context, ICache cache)
            {
                _context = context;
                _cache = cache;
            }

            public async Task<AddOrUpdateTenantResponse> Handle(AddOrUpdateTenantRequest request)
            {
                var entity = await _context.Tenants                    
                    .SingleOrDefaultAsync(x => x.Id == request.Tenant.Id);
                
                if (entity == null) {                    
                    _context.Tenants.Add(entity = new Tenant() { });
                }

                entity.Name = request.Tenant.Name;

                entity.HostUrl = request.Tenant.HostUrl;

                entity.UniqueId = new Guid(request.Tenant.UniqueId);

                await _context.SaveChangesAsync();

                return new AddOrUpdateTenantResponse();
            }

            private readonly GiftRegistryServiceContext _context;
            private readonly ICache _cache;
        }
    }
}
