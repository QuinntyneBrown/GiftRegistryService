using MediatR;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using System;
using System.Net.Http;

using static GiftRegistryService.Features.PhotoGalleries.AddOrUpdatePhotoGalleryCommand;
using static GiftRegistryService.Features.PhotoGalleries.GetPhotoGalleriesQuery;
using static GiftRegistryService.Features.PhotoGalleries.GetPhotoGalleryByIdQuery;
using static GiftRegistryService.Features.PhotoGalleries.RemovePhotoGalleryCommand;
using static GiftRegistryService.Features.PhotoGalleries.GetLatestGalleriesQuery;
using GiftRegistryService.Features.Core;

namespace GiftRegistryService.Features.PhotoGalleries
{
    [Authorize]
    [RoutePrefix("api/photoGallery")]
    public class PhotoGalleryController : ApiController
    {
        public PhotoGalleryController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [Route("add")]
        [HttpPost]
        [ResponseType(typeof(AddOrUpdatePhotoGalleryResponse))]
        public async Task<IHttpActionResult> Add(AddOrUpdatePhotoGalleryRequest request)
        {
            request.TenantUniqueId = Request.GetTenantUniqueId();
            return Ok(await _mediator.Send(request));
        }

        [Route("update")]
        [HttpPut]
        [ResponseType(typeof(AddOrUpdatePhotoGalleryResponse))]
        public async Task<IHttpActionResult> Update(AddOrUpdatePhotoGalleryRequest request)
        {
            request.TenantUniqueId = Request.GetTenantUniqueId();
            return Ok(await _mediator.Send(request));
        }
        
        [Route("get")]
        [AllowAnonymous]
        [HttpGet]
        [ResponseType(typeof(GetPhotoGalleriesResponse))]
        public async Task<IHttpActionResult> Get()
        {
            var request = new GetPhotoGalleriesRequest();
            request.TenantUniqueId = Request.GetTenantUniqueId();
            return Ok(await _mediator.Send(request));
        }

        [Route("getlatest")]
        [AllowAnonymous]
        [HttpGet]
        [ResponseType(typeof(GetLatestGalleriesResponse))]
        public async Task<IHttpActionResult> GetLatest()
        {
            var request = new GetLatestGalleriesRequest();
            request.TenantUniqueId = Request.GetTenantUniqueId();
            return Ok(await _mediator.Send(request));
        }

        [Route("getById")]
        [HttpGet]
        [ResponseType(typeof(GetPhotoGalleryByIdResponse))]
        public async Task<IHttpActionResult> GetById([FromUri]GetPhotoGalleryByIdRequest request)
        {
            request.TenantUniqueId = Request.GetTenantUniqueId();
            return Ok(await _mediator.Send(request));
        }

        [Route("remove")]
        [HttpDelete]
        [ResponseType(typeof(RemovePhotoGalleryResponse))]
        public async Task<IHttpActionResult> Remove([FromUri]RemovePhotoGalleryRequest request)
        {
            request.TenantUniqueId = Request.GetTenantUniqueId();
            return Ok(await _mediator.Send(request));
        }

        protected readonly IMediator _mediator;
    }
}
