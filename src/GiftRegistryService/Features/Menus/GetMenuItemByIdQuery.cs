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
    public class GetMenuItemByIdQuery
    {
        public class GetMenuItemByIdRequest : IRequest<GetMenuItemByIdResponse> { 
            public int Id { get; set; }
            public Guid TenantUniqueId { get; set; }
        }

        public class GetMenuItemByIdResponse
        {
            public MenuItemApiModel MenuItem { get; set; } 
        }

        public class GetMenuItemByIdHandler : IAsyncRequestHandler<GetMenuItemByIdRequest, GetMenuItemByIdResponse>
        {
            public GetMenuItemByIdHandler(GiftRegistryServiceContext context, ICache cache)
            {
                _context = context;
                _cache = cache;
            }

            public async Task<GetMenuItemByIdResponse> Handle(GetMenuItemByIdRequest request)
            {                
                return new GetMenuItemByIdResponse()
                {
                    MenuItem = MenuItemApiModel.FromMenuItem(await _context.MenuItems
                    .Include(x => x.Tenant)				
					.SingleAsync(x=>x.Id == request.Id &&  x.Tenant.UniqueId == request.TenantUniqueId))
                };
            }

            private readonly GiftRegistryServiceContext _context;
            private readonly ICache _cache;
        }

    }

}
