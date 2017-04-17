using MediatR;
using GiftRegistryService.Data;
using GiftRegistryService.Data.Model;
using GiftRegistryService.Features.Core;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using System.Data.Entity;

namespace GiftRegistryService.Features.Accounts
{
    public class AddOrUpdateProfileCommand
    {
        public class AddOrUpdateProfileRequest : IRequest<AddOrUpdateProfileResponse>
        {
            public ProfileApiModel Profile { get; set; }
            public Guid TenantUniqueId { get; set; }
        }

        public class AddOrUpdateProfileResponse { }

        public class AddOrUpdateProfileHandler : IAsyncRequestHandler<AddOrUpdateProfileRequest, AddOrUpdateProfileResponse>
        {
            public AddOrUpdateProfileHandler(GiftRegistryServiceContext context, ICache cache)
            {
                _context = context;
                _cache = cache;
            }

            public async Task<AddOrUpdateProfileResponse> Handle(AddOrUpdateProfileRequest request)
            {
                var entity = await _context.Profiles
                    .Include(x => x.Tenant)
                    .SingleOrDefaultAsync(x => x.Id == request.Profile.Id && x.Tenant.UniqueId == request.TenantUniqueId);
                
                if (entity == null) {
                    var tenant = await _context.Tenants.SingleAsync(x => x.UniqueId == request.TenantUniqueId);
                    _context.Profiles.Add(entity = new Profile() { TenantId = tenant.Id });
                }

                entity.Name = request.Profile.Name;
                
                await _context.SaveChangesAsync();

                return new AddOrUpdateProfileResponse();
            }

            private readonly GiftRegistryServiceContext _context;
            private readonly ICache _cache;
        }

    }

}
