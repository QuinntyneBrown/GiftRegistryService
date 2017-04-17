using MediatR;
using GiftRegistryService.Data;
using GiftRegistryService.Data.Model;
using GiftRegistryService.Features.Core;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using System.Data.Entity;

namespace GiftRegistryService.Features.Guests
{
    public class RemoveGuestCommand
    {
        public class RemoveGuestRequest : IRequest<RemoveGuestResponse>
        {
            public int Id { get; set; }
            public Guid TenantUniqueId { get; set; } 
        }

        public class RemoveGuestResponse { }

        public class RemoveGuestHandler : IAsyncRequestHandler<RemoveGuestRequest, RemoveGuestResponse>
        {
            public RemoveGuestHandler(GiftRegistryServiceContext context, ICache cache)
            {
                _context = context;
                _cache = cache;
            }

            public async Task<RemoveGuestResponse> Handle(RemoveGuestRequest request)
            {
                var guest = await _context.Guests.SingleAsync(x=>x.Id == request.Id && x.Tenant.UniqueId == request.TenantUniqueId);
                guest.IsDeleted = true;
                await _context.SaveChangesAsync();
                return new RemoveGuestResponse();
            }

            private readonly GiftRegistryServiceContext _context;
            private readonly ICache _cache;
        }
    }
}
