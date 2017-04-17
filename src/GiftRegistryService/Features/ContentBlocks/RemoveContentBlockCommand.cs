using MediatR;
using GiftRegistryService.Data;
using GiftRegistryService.Data.Model;
using GiftRegistryService.Features.Core;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using System.Data.Entity;

namespace GiftRegistryService.Features.ContentBlocks
{
    public class RemoveContentBlockCommand
    {
        public class RemoveContentBlockRequest : IRequest<RemoveContentBlockResponse>
        {
            public int Id { get; set; }
            public Guid TenantUniqueId { get; set; } 
        }

        public class RemoveContentBlockResponse { }

        public class RemoveContentBlockHandler : IAsyncRequestHandler<RemoveContentBlockRequest, RemoveContentBlockResponse>
        {
            public RemoveContentBlockHandler(GiftRegistryServiceContext context, ICache cache)
            {
                _context = context;
                _cache = cache;
            }

            public async Task<RemoveContentBlockResponse> Handle(RemoveContentBlockRequest request)
            {
                var contentBlock = await _context.ContentBlocks.SingleAsync(x=>x.Id == request.Id && x.Tenant.UniqueId == request.TenantUniqueId);
                contentBlock.IsDeleted = true;
                await _context.SaveChangesAsync();
                return new RemoveContentBlockResponse();
            }

            private readonly GiftRegistryServiceContext _context;
            private readonly ICache _cache;
        }
    }
}
