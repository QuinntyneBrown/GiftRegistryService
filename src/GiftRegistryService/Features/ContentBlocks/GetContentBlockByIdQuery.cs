using MediatR;
using GiftRegistryService.Data;
using GiftRegistryService.Features.Core;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using System.Data.Entity;

namespace GiftRegistryService.Features.ContentBlocks
{
    public class GetContentBlockByIdQuery
    {
        public class GetContentBlockByIdRequest : IRequest<GetContentBlockByIdResponse> { 
            public int Id { get; set; }
            public Guid TenantUniqueId { get; set; }
        }

        public class GetContentBlockByIdResponse
        {
            public ContentBlockApiModel ContentBlock { get; set; } 
        }

        public class GetContentBlockByIdHandler : IAsyncRequestHandler<GetContentBlockByIdRequest, GetContentBlockByIdResponse>
        {
            public GetContentBlockByIdHandler(GiftRegistryServiceContext context, ICache cache)
            {
                _context = context;
                _cache = cache;
            }

            public async Task<GetContentBlockByIdResponse> Handle(GetContentBlockByIdRequest request)
            {                
                return new GetContentBlockByIdResponse()
                {
                    ContentBlock = ContentBlockApiModel.FromContentBlock(await _context.ContentBlocks
                    .Include(x => x.Tenant)				
					.SingleAsync(x=>x.Id == request.Id &&  x.Tenant.UniqueId == request.TenantUniqueId))
                };
            }

            private readonly GiftRegistryServiceContext _context;
            private readonly ICache _cache;
        }

    }

}
