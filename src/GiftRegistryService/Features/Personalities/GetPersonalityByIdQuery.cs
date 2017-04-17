using MediatR;
using GiftRegistryService.Data;
using GiftRegistryService.Features.Core;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using System.Data.Entity;

namespace GiftRegistryService.Features.Personalities
{
    public class GetPersonalityByIdQuery
    {
        public class GetPersonalityByIdRequest : IRequest<GetPersonalityByIdResponse> { 
            public int Id { get; set; }
            public Guid TenantUniqueId { get; set; }
        }

        public class GetPersonalityByIdResponse
        {
            public PersonalityApiModel Personality { get; set; } 
        }

        public class GetPersonalityByIdHandler : IAsyncRequestHandler<GetPersonalityByIdRequest, GetPersonalityByIdResponse>
        {
            public GetPersonalityByIdHandler(GiftRegistryServiceContext context, ICache cache)
            {
                _context = context;
                _cache = cache;
            }

            public async Task<GetPersonalityByIdResponse> Handle(GetPersonalityByIdRequest request)
            {                
                return new GetPersonalityByIdResponse()
                {
                    Personality = PersonalityApiModel.FromPersonality(await _context.Personalities
                    .Include(x => x.Tenant)				
					.SingleAsync(x=>x.Id == request.Id &&  x.Tenant.UniqueId == request.TenantUniqueId))
                };
            }

            private readonly GiftRegistryServiceContext _context;
            private readonly ICache _cache;
        }

    }

}
