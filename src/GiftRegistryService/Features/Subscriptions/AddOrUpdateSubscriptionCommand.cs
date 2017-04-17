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
    public class AddOrUpdateSubscriptionCommand
    {
        public class AddOrUpdateSubscriptionRequest : IRequest<AddOrUpdateSubscriptionResponse>
        {
            public SubscriptionApiModel Subscription { get; set; }
        }

        public class AddOrUpdateSubscriptionResponse { }

        public class AddOrUpdateSubscriptionHandler : IAsyncRequestHandler<AddOrUpdateSubscriptionRequest, AddOrUpdateSubscriptionResponse>
        {
            public AddOrUpdateSubscriptionHandler(GiftRegistryServiceContext context, ICache cache)
            {
                _context = context;
                _cache = cache;
            }

            public async Task<AddOrUpdateSubscriptionResponse> Handle(AddOrUpdateSubscriptionRequest request)
            {
                var entity = await _context.Subscriptions
                    .SingleOrDefaultAsync(x => x.Id == request.Subscription.Id);
                
                if (entity == null) {
                    _context.Subscriptions.Add(entity = new Subscription() { });
                }

                entity.Name = request.Subscription.Name;

                entity.EffectiveDate = request.Subscription.EffectiveDate;

                entity.ExpiresOn = request.Subscription.ExpiresOn;

                entity.FeatureId = request.Subscription.FeatureId;

                entity.AccountId = request.Subscription.AccountId;

                await _context.SaveChangesAsync();

                return new AddOrUpdateSubscriptionResponse();
            }

            private readonly GiftRegistryServiceContext _context;
            private readonly ICache _cache;
        }

    }

}
