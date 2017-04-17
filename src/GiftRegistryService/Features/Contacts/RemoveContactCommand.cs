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
    public class RemoveContactCommand
    {
        public class RemoveContactRequest : IRequest<RemoveContactResponse>
        {
            public int Id { get; set; }
            public Guid TenantUniqueId { get; set; } 
        }

        public class RemoveContactResponse { }

        public class RemoveContactHandler : IAsyncRequestHandler<RemoveContactRequest, RemoveContactResponse>
        {
            public RemoveContactHandler(GiftRegistryServiceContext context, ICache cache)
            {
                _context = context;
                _cache = cache;
            }

            public async Task<RemoveContactResponse> Handle(RemoveContactRequest request)
            {
                var contact = await _context.Contacts.SingleAsync(x=>x.Id == request.Id && x.Tenant.UniqueId == request.TenantUniqueId);
                contact.IsDeleted = true;
                await _context.SaveChangesAsync();
                return new RemoveContactResponse();
            }

            private readonly GiftRegistryServiceContext _context;
            private readonly ICache _cache;
        }
    }
}
