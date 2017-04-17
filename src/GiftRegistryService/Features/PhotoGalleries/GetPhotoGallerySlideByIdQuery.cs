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
    public class GetPhotoGallerySlideByIdQuery
    {
        public class GetPhotoGallerySlideByIdRequest : IRequest<GetPhotoGallerySlideByIdResponse> { 
            public int Id { get; set; }
            public Guid TenantUniqueId { get; set; }
        }

        public class GetPhotoGallerySlideByIdResponse
        {
            public PhotoGallerySlideApiModel PhotoGallerySlide { get; set; } 
        }

        public class GetPhotoGallerySlideByIdHandler : IAsyncRequestHandler<GetPhotoGallerySlideByIdRequest, GetPhotoGallerySlideByIdResponse>
        {
            public GetPhotoGallerySlideByIdHandler(GiftRegistryServiceContext context, ICache cache)
            {
                _context = context;
                _cache = cache;
            }

            public async Task<GetPhotoGallerySlideByIdResponse> Handle(GetPhotoGallerySlideByIdRequest request)
            {                
                return new GetPhotoGallerySlideByIdResponse()
                {
                    PhotoGallerySlide = PhotoGallerySlideApiModel.FromPhotoGallerySlide(await _context.PhotoGallerySlides
                    .Include(x => x.Tenant)				
					.SingleAsync(x=>x.Id == request.Id &&  x.Tenant.UniqueId == request.TenantUniqueId))
                };
            }

            private readonly GiftRegistryServiceContext _context;
            private readonly ICache _cache;
        }

    }

}
