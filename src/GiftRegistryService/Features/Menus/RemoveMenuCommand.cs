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
    public class RemoveMenuCommand
    {
        public class RemoveMenuRequest : IRequest<RemoveMenuResponse>
        {
            public int Id { get; set; }
            public Guid TenantUniqueId { get; set; } 
        }

        public class RemoveMenuResponse { }

        public class RemoveMenuHandler : IAsyncRequestHandler<RemoveMenuRequest, RemoveMenuResponse>
        {
            public RemoveMenuHandler(GiftRegistryServiceContext context, ICache cache)
            {
                _context = context;
                _cache = cache;
            }

            public async Task<RemoveMenuResponse> Handle(RemoveMenuRequest request)
            {
                var menu = await _context.Menus.SingleAsync(x=>x.Id == request.Id && x.Tenant.UniqueId == request.TenantUniqueId);
                menu.IsDeleted = true;
                await _context.SaveChangesAsync();
                return new RemoveMenuResponse();
            }

            private readonly GiftRegistryServiceContext _context;
            private readonly ICache _cache;
        }
    }
}
