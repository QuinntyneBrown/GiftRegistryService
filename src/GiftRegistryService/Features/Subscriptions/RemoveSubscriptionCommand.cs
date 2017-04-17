using MediatR;
using GiftRegistryService.Data;
using GiftRegistryService.Data.Model;
using GiftRegistryService.Features.Core;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using System.Data.Entity;

namespace GiftRegistryService.Features.Subscriptions
{
    public class RemoveSubscriptionCommand
    {
        public class RemoveSubscriptionRequest : IRequest<RemoveSubscriptionResponse>
        {
            public int Id { get; set; }
        }

        public class RemoveSubscriptionResponse { }

        public class RemoveSubscriptionHandler : IAsyncRequestHandler<RemoveSubscriptionRequest, RemoveSubscriptionResponse>
        {
            public RemoveSubscriptionHandler(GiftRegistryServiceContext context, ICache cache)
            {
                _context = context;
                _cache = cache;
            }

            public async Task<RemoveSubscriptionResponse> Handle(RemoveSubscriptionRequest request)
            {
                var subscription = await _context.Subscriptions.SingleAsync(x=>x.Id == request.Id);
                subscription.IsDeleted = true;
                await _context.SaveChangesAsync();
                return new RemoveSubscriptionResponse();
            }

            private readonly GiftRegistryServiceContext _context;
            private readonly ICache _cache;
        }
    }
}
