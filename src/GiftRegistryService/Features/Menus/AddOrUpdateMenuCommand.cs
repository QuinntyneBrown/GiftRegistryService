using MediatR;
using GiftRegistryService.Data;
using GiftRegistryService.Data.Model;
using GiftRegistryService.Features.Core;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using System.Data.Entity;

namespace GiftRegistryService.Features.Menus
{
    public class AddOrUpdateMenuCommand
    {
        public class AddOrUpdateMenuRequest : IRequest<AddOrUpdateMenuResponse>
        {
            public MenuApiModel Menu { get; set; }
            public Guid TenantUniqueId { get; set; }
        }

        public class AddOrUpdateMenuResponse { }

        public class AddOrUpdateMenuHandler : IAsyncRequestHandler<AddOrUpdateMenuRequest, AddOrUpdateMenuResponse>
        {
            public AddOrUpdateMenuHandler(GiftRegistryServiceContext context, ICache cache)
            {
                _context = context;
                _cache = cache;
            }

            public async Task<AddOrUpdateMenuResponse> Handle(AddOrUpdateMenuRequest request)
            {
                var entity = await _context.Menus
                    .Include(x => x.Tenant)
                    .SingleOrDefaultAsync(x => x.Id == request.Menu.Id && x.Tenant.UniqueId == request.TenantUniqueId);
                
                if (entity == null) {
                    var tenant = await _context.Tenants.SingleAsync(x => x.UniqueId == request.TenantUniqueId);
                    _context.Menus.Add(entity = new Menu() { TenantId = tenant.Id });
                }

                entity.Name = request.Menu.Name;
                
                await _context.SaveChangesAsync();

                return new AddOrUpdateMenuResponse();
            }

            private readonly GiftRegistryServiceContext _context;
            private readonly ICache _cache;
        }

    }

}
