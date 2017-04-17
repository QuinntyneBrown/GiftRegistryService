using MediatR;
using GiftRegistryService.Data;
using GiftRegistryService.Features.Core;
using System.Threading.Tasks;
using System.Data.Entity;
using System;

namespace GiftRegistryService.Features.PhotoGalleries
{
    public class GetPhotoGalleryByIdQuery
    {
        public class GetPhotoGalleryByIdRequest : IRequest<GetPhotoGalleryByIdResponse> { 
            public int Id { get; set; }
            public Guid TenantUniqueId { get; set; }
        }

        public class GetPhotoGalleryByIdResponse
        {
            public PhotoGalleryApiModel PhotoGallery { get; set; } 
        }

        public class GetPhotoGalleryByIdHandler : IAsyncRequestHandler<GetPhotoGalleryByIdRequest, GetPhotoGalleryByIdResponse>
        {
            public GetPhotoGalleryByIdHandler(GiftRegistryServiceContext context, ICache cache)
            {
                _context = context;
                _cache = cache;
            }

            public async Task<GetPhotoGalleryByIdResponse> Handle(GetPhotoGalleryByIdRequest request)
            {                
                return new GetPhotoGalleryByIdResponse()
                {
                    PhotoGallery = PhotoGalleryApiModel.FromPhotoGallery(await _context.PhotoGalleries
                    .Include(x => x.PhotoGallerySlides)
                    .SingleAsync(x=>x.Id == request.Id))
                };
            }

            private readonly GiftRegistryServiceContext _context;
            private readonly ICache _cache;
        }
    }
}
