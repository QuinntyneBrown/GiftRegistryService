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
    public class AddOrUpdateGuestCommand
    {
        public class AddOrUpdateGuestRequest : IRequest<AddOrUpdateGuestResponse>
        {
            public GuestApiModel Guest { get; set; }
            public Guid TenantUniqueId { get; set; }
        }

        public class AddOrUpdateGuestResponse { }

        public class AddOrUpdateGuestHandler : IAsyncRequestHandler<AddOrUpdateGuestRequest, AddOrUpdateGuestResponse>
        {
            public AddOrUpdateGuestHandler(GiftRegistryServiceContext context, ICache cache)
            {
                _context = context;
                _cache = cache;
            }

            public async Task<AddOrUpdateGuestResponse> Handle(AddOrUpdateGuestRequest request)
            {
                var entity = await _context.Guests
                    .Include(x => x.Tenant)
                    .SingleOrDefaultAsync(x => x.Id == request.Guest.Id && x.Tenant.UniqueId == request.TenantUniqueId);
                
                if (entity == null) {
                    var tenant = await _context.Tenants.SingleAsync(x => x.UniqueId == request.TenantUniqueId);
                    _context.Guests.Add(entity = new Guest() { TenantId = tenant.Id });
                }
                
                
                await _context.SaveChangesAsync();

                return new AddOrUpdateGuestResponse();
            }

            private readonly GiftRegistryServiceContext _context;
            private readonly ICache _cache;
        }

    }

}
