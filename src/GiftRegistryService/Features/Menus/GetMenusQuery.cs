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
    public class GetMenusQuery
    {
        public class GetMenusRequest : IRequest<GetMenusResponse> { 
            public Guid TenantUniqueId { get; set; }       
        }

        public class GetMenusResponse
        {
            public ICollection<MenuApiModel> Menus { get; set; } = new HashSet<MenuApiModel>();
        }

        public class GetMenusHandler : IAsyncRequestHandler<GetMenusRequest, GetMenusResponse>
        {
            public GetMenusHandler(GiftRegistryServiceContext context, ICache cache)
            {
                _context = context;
                _cache = cache;
            }

            public async Task<GetMenusResponse> Handle(GetMenusRequest request)
            {
                var menus = await _context.Menus
                    .Include(x => x.Tenant)
                    .Where(x => x.Tenant.UniqueId == request.TenantUniqueId )
                    .ToListAsync();

                return new GetMenusResponse()
                {
                    Menus = menus.Select(x => MenuApiModel.FromMenu(x)).ToList()
                };
            }

            private readonly GiftRegistryServiceContext _context;
            private readonly ICache _cache;
        }

    }

}
