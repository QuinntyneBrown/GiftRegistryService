using MediatR;
using GiftRegistryService.Data;
using GiftRegistryService.Data.Model;
using GiftRegistryService.Features.Core;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using System.Data.Entity;

namespace GiftRegistryService.Features.Contacts
{
    public class AddOrUpdateContactCommand
    {
        public class AddOrUpdateContactRequest : IRequest<AddOrUpdateContactResponse>
        {
            public ContactApiModel Contact { get; set; }
            public Guid TenantUniqueId { get; set; }
        }

        public class AddOrUpdateContactResponse { }

        public class AddOrUpdateContactHandler : IAsyncRequestHandler<AddOrUpdateContactRequest, AddOrUpdateContactResponse>
        {
            public AddOrUpdateContactHandler(GiftRegistryServiceContext context, ICache cache)
            {
                _context = context;
                _cache = cache;
            }

            public async Task<AddOrUpdateContactResponse> Handle(AddOrUpdateContactRequest request)
            {
                var entity = await _context.Contacts
                    .Include(x => x.Tenant)
                    .SingleOrDefaultAsync(x => x.Id == request.Contact.Id && x.Tenant.UniqueId == request.TenantUniqueId);
                
                if (entity == null) {
                    var tenant = await _context.Tenants.SingleAsync(x => x.UniqueId == request.TenantUniqueId);
                    _context.Contacts.Add(entity = new Contact() { TenantId = tenant.Id });
                }

                entity.Name = request.Contact.Name;
                
                await _context.SaveChangesAsync();

                return new AddOrUpdateContactResponse();
            }

            private readonly GiftRegistryServiceContext _context;
            private readonly ICache _cache;
        }

    }

}
