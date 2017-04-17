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
    public class GetGuestByIdQuery
    {
        public class GetGuestByIdRequest : IRequest<GetGuestByIdResponse> { 
            public int Id { get; set; }
            public Guid TenantUniqueId { get; set; }
        }

        public class GetGuestByIdResponse
        {
            public GuestApiModel Guest { get; set; } 
        }

        public class GetGuestByIdHandler : IAsyncRequestHandler<GetGuestByIdRequest, GetGuestByIdResponse>
        {
            public GetGuestByIdHandler(GiftRegistryServiceContext context, ICache cache)
            {
                _context = context;
                _cache = cache;
            }

            public async Task<GetGuestByIdResponse> Handle(GetGuestByIdRequest request)
            {                
                return new GetGuestByIdResponse()
                {
                    Guest = GuestApiModel.FromGuest(await _context.Guests
                    .Include(x => x.Tenant)				
					.SingleAsync(x=>x.Id == request.Id &&  x.Tenant.UniqueId == request.TenantUniqueId))
                };
            }

            private readonly GiftRegistryServiceContext _context;
            private readonly ICache _cache;
        }

    }

}
