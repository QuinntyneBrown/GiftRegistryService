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
    public class AddOrUpdateMenuItemCommand
    {
        public class AddOrUpdateMenuItemRequest : IRequest<AddOrUpdateMenuItemResponse>
        {
            public MenuItemApiModel MenuItem { get; set; }
            public Guid TenantUniqueId { get; set; }
        }

        public class AddOrUpdateMenuItemResponse { }

        public class AddOrUpdateMenuItemHandler : IAsyncRequestHandler<AddOrUpdateMenuItemRequest, AddOrUpdateMenuItemResponse>
        {
            public AddOrUpdateMenuItemHandler(GiftRegistryServiceContext context, ICache cache)
            {
                _context = context;
                _cache = cache;
            }

            public async Task<AddOrUpdateMenuItemResponse> Handle(AddOrUpdateMenuItemRequest request)
            {
                var entity = await _context.MenuItems
                    .Include(x => x.Tenant)
                    .SingleOrDefaultAsync(x => x.Id == request.MenuItem.Id && x.Tenant.UniqueId == request.TenantUniqueId);
                
                if (entity == null) {
                    var tenant = await _context.Tenants.SingleAsync(x => x.UniqueId == request.TenantUniqueId);
                    _context.MenuItems.Add(entity = new MenuItem() { TenantId = tenant.Id });
                }

                entity.Name = request.MenuItem.Name;
                
                await _context.SaveChangesAsync();

                return new AddOrUpdateMenuItemResponse();
            }

            private readonly GiftRegistryServiceContext _context;
            private readonly ICache _cache;
        }

    }

}
