using MediatR;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using GiftRegistryService.Features.Core;
using static GiftRegistryService.Features.PhotoGalleries.AddOrUpdatePhotoGallerySlideCommand;
using static GiftRegistryService.Features.PhotoGalleries.GetPhotoGallerySlidesQuery;
using static GiftRegistryService.Features.PhotoGalleries.GetPhotoGallerySlideByIdQuery;
using static GiftRegistryService.Features.PhotoGalleries.RemovePhotoGallerySlideCommand;

namespace GiftRegistryService.Features.PhotoGalleries
{
    [Authorize]
    [RoutePrefix("api/photoGallerySlide")]
    public class PhotoGallerySlideController : ApiController
    {
        public PhotoGallerySlideController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [Route("add")]
        [HttpPost]
        [ResponseType(typeof(AddOrUpdatePhotoGallerySlideResponse))]
        public async Task<IHttpActionResult> Add(AddOrUpdatePhotoGallerySlideRequest request)
        {
            request.TenantUniqueId = Request.GetTenantUniqueId();
            return Ok(await _mediator.Send(request));
        }

        [Route("update")]
        [HttpPut]
        [ResponseType(typeof(AddOrUpdatePhotoGallerySlideResponse))]
        public async Task<IHttpActionResult> Update(AddOrUpdatePhotoGallerySlideRequest request)
        {
            request.TenantUniqueId = Request.GetTenantUniqueId();
            return Ok(await _mediator.Send(request));
        }
        
        [Route("get")]
        [AllowAnonymous]
        [HttpGet]
        [ResponseType(typeof(GetPhotoGallerySlidesResponse))]
        public async Task<IHttpActionResult> Get()
        {
            var request = new GetPhotoGallerySlidesRequest();
            request.TenantUniqueId = Request.GetTenantUniqueId();
            return Ok(await _mediator.Send(request));
        }

        [Route("getById")]
        [HttpGet]
        [ResponseType(typeof(GetPhotoGallerySlideByIdResponse))]
        public async Task<IHttpActionResult> GetById([FromUri]GetPhotoGallerySlideByIdRequest request)
        {
            request.TenantUniqueId = Request.GetTenantUniqueId();
            return Ok(await _mediator.Send(request));
        }

        [Route("remove")]
        [HttpDelete]
        [ResponseType(typeof(RemovePhotoGallerySlideResponse))]
        public async Task<IHttpActionResult> Remove([FromUri]RemovePhotoGallerySlideRequest request)
        {
            request.TenantUniqueId = Request.GetTenantUniqueId();
            return Ok(await _mediator.Send(request));
        }

        protected readonly IMediator _mediator;
    }
}
