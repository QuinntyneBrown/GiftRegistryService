using MediatR;
using GiftRegistryService.Data;
using GiftRegistryService.Features.Core;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using System.Data.Entity;

namespace GiftRegistryService.Features.PhotoGalleries
{
    public class GetPhotoGallerySlidesQuery
    {
        public class GetPhotoGallerySlidesRequest : IRequest<GetPhotoGallerySlidesResponse> { 
            public Guid TenantUniqueId { get; set; }       
        }

        public class GetPhotoGallerySlidesResponse
        {
            public ICollection<PhotoGallerySlideApiModel> PhotoGallerySlides { get; set; } = new HashSet<PhotoGallerySlideApiModel>();
        }

        public class GetPhotoGallerySlidesHandler : IAsyncRequestHandler<GetPhotoGallerySlidesRequest, GetPhotoGallerySlidesResponse>
        {
            public GetPhotoGallerySlidesHandler(GiftRegistryServiceContext context, ICache cache)
            {
                _context = context;
                _cache = cache;
            }

            public async Task<GetPhotoGallerySlidesResponse> Handle(GetPhotoGallerySlidesRequest request)
            {
                var photoGallerySlides = await _context.PhotoGallerySlides
                    .Include(x => x.Tenant)
                    .Where(x => x.Tenant.UniqueId == request.TenantUniqueId )
                    .ToListAsync();

                return new GetPhotoGallerySlidesResponse()
                {
                    PhotoGallerySlides = photoGallerySlides.Select(x => PhotoGallerySlideApiModel.FromPhotoGallerySlide(x)).ToList()
                };
            }

            private readonly GiftRegistryServiceContext _context;
            private readonly ICache _cache;
        }

    }

}
