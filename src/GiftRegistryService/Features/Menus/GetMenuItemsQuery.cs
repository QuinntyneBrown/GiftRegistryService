using MediatR;
using GiftRegistryService.Data;
using GiftRegistryService.Features.Core;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using System.Data.Entity;

namespace GiftRegistryService.Features.Menus
{
    public class GetMenuItemsQuery
    {
        public class GetMenuItemsRequest : IRequest<GetMenuItemsResponse> { 
            public Guid TenantUniqueId { get; set; }       
        }

        public class GetMenuItemsResponse
        {
            public ICollection<MenuItemApiModel> MenuItems { get; set; } = new HashSet<MenuItemApiModel>();
        }

        public class GetMenuItemsHandler : IAsyncRequestHandler<GetMenuItemsRequest, GetMenuItemsResponse>
        {
            public GetMenuItemsHandler(GiftRegistryServiceContext context, ICache cache)
            {
                _context = context;
                _cache = cache;
            }

            public async Task<GetMenuItemsResponse> Handle(GetMenuItemsRequest request)
            {
                var menuItems = await _context.MenuItems
                    .Include(x => x.Tenant)
                    .Where(x => x.Tenant.UniqueId == request.TenantUniqueId )
                    .ToListAsync();

                return new GetMenuItemsResponse()
                {
                    MenuItems = menuItems.Select(x => MenuItemApiModel.FromMenuItem(x)).ToList()
                };
            }

            private readonly GiftRegistryServiceContext _context;
            private readonly ICache _cache;
        }

    }

}
