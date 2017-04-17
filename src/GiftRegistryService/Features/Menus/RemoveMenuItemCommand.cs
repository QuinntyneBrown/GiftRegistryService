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
    public class RemoveMenuItemCommand
    {
        public class RemoveMenuItemRequest : IRequest<RemoveMenuItemResponse>
        {
            public int Id { get; set; }
            public Guid TenantUniqueId { get; set; } 
        }

        public class RemoveMenuItemResponse { }

        public class RemoveMenuItemHandler : IAsyncRequestHandler<RemoveMenuItemRequest, RemoveMenuItemResponse>
        {
            public RemoveMenuItemHandler(GiftRegistryServiceContext context, ICache cache)
            {
                _context = context;
                _cache = cache;
            }

            public async Task<RemoveMenuItemResponse> Handle(RemoveMenuItemRequest request)
            {
                var menuItem = await _context.MenuItems.SingleAsync(x=>x.Id == request.Id && x.Tenant.UniqueId == request.TenantUniqueId);
                menuItem.IsDeleted = true;
                await _context.SaveChangesAsync();
                return new RemoveMenuItemResponse();
            }

            private readonly GiftRegistryServiceContext _context;
            private readonly ICache _cache;
        }
    }
}
