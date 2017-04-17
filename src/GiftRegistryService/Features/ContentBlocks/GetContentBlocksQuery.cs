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
    public class GetContentBlocksQuery
    {
        public class GetContentBlocksRequest : IRequest<GetContentBlocksResponse> { 
            public Guid TenantUniqueId { get; set; }       
        }

        public class GetContentBlocksResponse
        {
            public ICollection<ContentBlockApiModel> ContentBlocks { get; set; } = new HashSet<ContentBlockApiModel>();
        }

        public class GetContentBlocksHandler : IAsyncRequestHandler<GetContentBlocksRequest, GetContentBlocksResponse>
        {
            public GetContentBlocksHandler(GiftRegistryServiceContext context, ICache cache)
            {
                _context = context;
                _cache = cache;
            }

            public async Task<GetContentBlocksResponse> Handle(GetContentBlocksRequest request)
            {
                var contentBlocks = await _context.ContentBlocks
                    .Include(x => x.Tenant)
                    .Where(x => x.Tenant.UniqueId == request.TenantUniqueId )
                    .ToListAsync();

                return new GetContentBlocksResponse()
                {
                    ContentBlocks = contentBlocks.Select(x => ContentBlockApiModel.FromContentBlock(x)).ToList()
                };
            }

            private readonly GiftRegistryServiceContext _context;
            private readonly ICache _cache;
        }

    }

}
