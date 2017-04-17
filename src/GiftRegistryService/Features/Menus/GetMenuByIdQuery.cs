using MediatR;
using GiftRegistryService.Data;
using GiftRegistryService.Features.Core;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using System.Data.Entity;

namespace GiftRegistryService.Features.Menus
{
    public class GetMenuByIdQuery
    {
        public class GetMenuByIdRequest : IRequest<GetMenuByIdResponse> { 
            public int Id { get; set; }
            public Guid TenantUniqueId { get; set; }
        }

        public class GetMenuByIdResponse
        {
            public MenuApiModel Menu { get; set; } 
        }

        public class GetMenuByIdHandler : IAsyncRequestHandler<GetMenuByIdRequest, GetMenuByIdResponse>
        {
            public GetMenuByIdHandler(GiftRegistryServiceContext context, ICache cache)
            {
                _context = context;
                _cache = cache;
            }

            public async Task<GetMenuByIdResponse> Handle(GetMenuByIdRequest request)
            {                
                return new GetMenuByIdResponse()
                {
                    Menu = MenuApiModel.FromMenu(await _context.Menus
                    .Include(x => x.Tenant)				
					.SingleAsync(x=>x.Id == request.Id &&  x.Tenant.UniqueId == request.TenantUniqueId))
                };
            }

            private readonly GiftRegistryServiceContext _context;
            private readonly ICache _cache;
        }

    }

}
