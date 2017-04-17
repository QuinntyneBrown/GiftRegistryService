using MediatR;
using GiftRegistryService.Data;
using GiftRegistryService.Data.Model;
using GiftRegistryService.Features.Core;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using System.Data.Entity;

namespace GiftRegistryService.Features.Personalities
{
    public class AddOrUpdatePersonalityCommand
    {
        public class AddOrUpdatePersonalityRequest : IRequest<AddOrUpdatePersonalityResponse>
        {
            public PersonalityApiModel Personality { get; set; }
            public Guid TenantUniqueId { get; set; }
        }

        public class AddOrUpdatePersonalityResponse { }

        public class AddOrUpdatePersonalityHandler : IAsyncRequestHandler<AddOrUpdatePersonalityRequest, AddOrUpdatePersonalityResponse>
        {
            public AddOrUpdatePersonalityHandler(GiftRegistryServiceContext context, ICache cache)
            {
                _context = context;
                _cache = cache;
            }

            public async Task<AddOrUpdatePersonalityResponse> Handle(AddOrUpdatePersonalityRequest request)
            {
                var entity = await _context.Personalities
                    .Include(x => x.Tenant)
                    .SingleOrDefaultAsync(x => x.Id == request.Personality.Id && x.Tenant.UniqueId == request.TenantUniqueId);
                
                if (entity == null) {
                    var tenant = await _context.Tenants.SingleAsync(x => x.UniqueId == request.TenantUniqueId);
                    _context.Personalities.Add(entity = new Personality() { TenantId = tenant.Id });
                }

                entity.Name = request.Personality.Name;
                
                await _context.SaveChangesAsync();

                return new AddOrUpdatePersonalityResponse();
            }

            private readonly GiftRegistryServiceContext _context;
            private readonly ICache _cache;
        }

    }

}
