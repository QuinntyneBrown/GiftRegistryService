using MediatR;
using GiftRegistryService.Data;
using GiftRegistryService.Data.Model;
using GiftRegistryService.Features.Core;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using System.Data.Entity;

namespace GiftRegistryService.Features.Events
{
    public class RemoveEventCommand
    {
        public class RemoveEventRequest : IRequest<RemoveEventResponse>
        {
            public int Id { get; set; }
            public Guid TenantUniqueId { get; set; } 
        }

        public class RemoveEventResponse { }

        public class RemoveEventHandler : IAsyncRequestHandler<RemoveEventRequest, RemoveEventResponse>
        {
            public RemoveEventHandler(GiftRegistryServiceContext context, ICache cache)
            {
                _context = context;
                _cache = cache;
            }

            public async Task<RemoveEventResponse> Handle(RemoveEventRequest request)
            {
                var e = await _context.Events.SingleAsync(x=>x.Id == request.Id && x.Tenant.UniqueId == request.TenantUniqueId);
                e.IsDeleted = true;
                await _context.SaveChangesAsync();
                return new RemoveEventResponse();
            }

            private readonly GiftRegistryServiceContext _context;
            private readonly ICache _cache;
        }
    }
}
