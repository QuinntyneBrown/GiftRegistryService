using MediatR;
using GiftRegistryService.Data;
using GiftRegistryService.Features.Core;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using System.Data.Entity;

namespace GiftRegistryService.Features.Guests
{
    public class GetGuestsQuery
    {
        public class GetGuestsRequest : IRequest<GetGuestsResponse> { 
            public Guid TenantUniqueId { get; set; }       
        }

        public class GetGuestsResponse
        {
            public ICollection<GuestApiModel> Guests { get; set; } = new HashSet<GuestApiModel>();
        }

        public class GetGuestsHandler : IAsyncRequestHandler<GetGuestsRequest, GetGuestsResponse>
        {
            public GetGuestsHandler(GiftRegistryServiceContext context, ICache cache)
            {
                _context = context;
                _cache = cache;
            }

            public async Task<GetGuestsResponse> Handle(GetGuestsRequest request)
            {
                var guests = await _context.Guests
                    .Include(x => x.Tenant)
                    .Where(x => x.Tenant.UniqueId == request.TenantUniqueId )
                    .ToListAsync();

                return new GetGuestsResponse()
                {
                    Guests = guests.Select(x => GuestApiModel.FromGuest(x)).ToList()
                };
            }

            private readonly GiftRegistryServiceContext _context;
            private readonly ICache _cache;
        }

    }

}
