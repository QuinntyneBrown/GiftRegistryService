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
    public class GetContactByIdQuery
    {
        public class GetContactByIdRequest : IRequest<GetContactByIdResponse> { 
            public int Id { get; set; }
            public Guid TenantUniqueId { get; set; }
        }

        public class GetContactByIdResponse
        {
            public ContactApiModel Contact { get; set; } 
        }

        public class GetContactByIdHandler : IAsyncRequestHandler<GetContactByIdRequest, GetContactByIdResponse>
        {
            public GetContactByIdHandler(GiftRegistryServiceContext context, ICache cache)
            {
                _context = context;
                _cache = cache;
            }

            public async Task<GetContactByIdResponse> Handle(GetContactByIdRequest request)
            {                
                return new GetContactByIdResponse()
                {
                    Contact = ContactApiModel.FromContact(await _context.Contacts
                    .Include(x => x.Tenant)				
					.SingleAsync(x=>x.Id == request.Id &&  x.Tenant.UniqueId == request.TenantUniqueId))
                };
            }

            private readonly GiftRegistryServiceContext _context;
            private readonly ICache _cache;
        }

    }

}
