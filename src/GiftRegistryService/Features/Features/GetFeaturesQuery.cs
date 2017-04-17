using MediatR;
using GiftRegistryService.Data;
using GiftRegistryService.Features.Core;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using System.Data.Entity;

namespace GiftRegistryService.Features.Features
{
    public class GetFeaturesQuery
    {
        public class GetFeaturesRequest : IRequest<GetFeaturesResponse> { 
     
        }

        public class GetFeaturesResponse
        {
            public ICollection<FeatureApiModel> Features { get; set; } = new HashSet<FeatureApiModel>();
        }

        public class GetFeaturesHandler : IAsyncRequestHandler<GetFeaturesRequest, GetFeaturesResponse>
        {
            public GetFeaturesHandler(GiftRegistryServiceContext context, ICache cache)
            {
                _context = context;
                _cache = cache;
            }

            public async Task<GetFeaturesResponse> Handle(GetFeaturesRequest request)
            {
                var features = await _context.Features
                    .ToListAsync();

                return new GetFeaturesResponse()
                {
                    Features = features.Select(x => FeatureApiModel.FromFeature(x)).ToList()
                };
            }

            private readonly GiftRegistryServiceContext _context;
            private readonly ICache _cache;
        }

    }

}
