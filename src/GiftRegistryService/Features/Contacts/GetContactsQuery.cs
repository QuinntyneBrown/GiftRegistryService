using MediatR;
using GiftRegistryService.Data;
using GiftRegistryService.Features.Core;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using System.Data.Entity;

namespace GiftRegistryService.Features.Contacts
{
    public class GetContactsQuery
    {
        public class GetContactsRequest : IRequest<GetContactsResponse> { 
            public Guid TenantUniqueId { get; set; }       
        }

        public class GetContactsResponse
        {
            public ICollection<ContactApiModel> Contacts { get; set; } = new HashSet<ContactApiModel>();
        }

        public class GetContactsHandler : IAsyncRequestHandler<GetContactsRequest, GetContactsResponse>
        {
            public GetContactsHandler(GiftRegistryServiceContext context, ICache cache)
            {
                _context = context;
                _cache = cache;
            }

            public async Task<GetContactsResponse> Handle(GetContactsRequest request)
            {
                var contacts = await _context.Contacts
                    .Include(x => x.Tenant)
                    .Where(x => x.Tenant.UniqueId == request.TenantUniqueId )
                    .ToListAsync();

                return new GetContactsResponse()
                {
                    Contacts = contacts.Select(x => ContactApiModel.FromContact(x)).ToList()
                };
            }

            private readonly GiftRegistryServiceContext _context;
            private readonly ICache _cache;
        }

    }

}
