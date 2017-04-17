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
    public class AddOrUpdatePhotoGallerySlideCommand
    {
        public class AddOrUpdatePhotoGallerySlideRequest : IRequest<AddOrUpdatePhotoGallerySlideResponse>
        {
            public PhotoGallerySlideApiModel PhotoGallerySlide { get; set; }
            public Guid TenantUniqueId { get; set; }
        }

        public class AddOrUpdatePhotoGallerySlideResponse { }

        public class AddOrUpdatePhotoGallerySlideHandler : IAsyncRequestHandler<AddOrUpdatePhotoGallerySlideRequest, AddOrUpdatePhotoGallerySlideResponse>
        {
            public AddOrUpdatePhotoGallerySlideHandler(GiftRegistryServiceContext context, ICache cache)
            {
                _context = context;
                _cache = cache;
            }

            public async Task<AddOrUpdatePhotoGallerySlideResponse> Handle(AddOrUpdatePhotoGallerySlideRequest request)
            {
                var entity = await _context.PhotoGallerySlides
                    .Include(x => x.Tenant)
                    .SingleOrDefaultAsync(x => x.Id == request.PhotoGallerySlide.Id && x.Tenant.UniqueId == request.TenantUniqueId);
                
                if (entity == null) {
                    var tenant = await _context.Tenants.SingleAsync(x => x.UniqueId == request.TenantUniqueId);
                    _context.PhotoGallerySlides.Add(entity = new PhotoGallerySlide() { TenantId = tenant.Id });
                }

                entity.Name = request.PhotoGallerySlide.Name;
                
                await _context.SaveChangesAsync();

                return new AddOrUpdatePhotoGallerySlideResponse();
            }

            private readonly GiftRegistryServiceContext _context;
            private readonly ICache _cache;
        }

    }

}
