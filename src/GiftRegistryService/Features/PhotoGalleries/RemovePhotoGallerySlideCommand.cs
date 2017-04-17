using MediatR;
using GiftRegistryService.Data;
using GiftRegistryService.Data.Model;
using GiftRegistryService.Features.Core;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using System.Data.Entity;

namespace GiftRegistryService.Features.PhotoGalleries
{
    public class RemovePhotoGallerySlideCommand
    {
        public class RemovePhotoGallerySlideRequest : IRequest<RemovePhotoGallerySlideResponse>
        {
            public int Id { get; set; }
            public Guid TenantUniqueId { get; set; } 
        }

        public class RemovePhotoGallerySlideResponse { }

        public class RemovePhotoGallerySlideHandler : IAsyncRequestHandler<RemovePhotoGallerySlideRequest, RemovePhotoGallerySlideResponse>
        {
            public RemovePhotoGallerySlideHandler(GiftRegistryServiceContext context, ICache cache)
            {
                _context = context;
                _cache = cache;
            }

            public async Task<RemovePhotoGallerySlideResponse> Handle(RemovePhotoGallerySlideRequest request)
            {
                var photoGallerySlide = await _context.PhotoGallerySlides.SingleAsync(x=>x.Id == request.Id && x.Tenant.UniqueId == request.TenantUniqueId);
                photoGallerySlide.IsDeleted = true;
                await _context.SaveChangesAsync();
                return new RemovePhotoGallerySlideResponse();
            }

            private readonly GiftRegistryServiceContext _context;
            private readonly ICache _cache;
        }
    }
}
