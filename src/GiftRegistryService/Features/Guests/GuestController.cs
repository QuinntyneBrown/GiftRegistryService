using MediatR;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using GiftRegistryService.Features.Core;
using static GiftRegistryService.Features.Guests.AddOrUpdateGuestCommand;
using static GiftRegistryService.Features.Guests.GetGuestsQuery;
using static GiftRegistryService.Features.Guests.GetGuestByIdQuery;
using static GiftRegistryService.Features.Guests.RemoveGuestCommand;

namespace GiftRegistryService.Features.Guests
{
    [Authorize]
    [RoutePrefix("api/guest")]
    public class GuestController : ApiController
    {
        public GuestController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [Route("add")]
        [HttpPost]
        [ResponseType(typeof(AddOrUpdateGuestResponse))]
        public async Task<IHttpActionResult> Add(AddOrUpdateGuestRequest request)
        {
            request.TenantUniqueId = Request.GetTenantUniqueId();
            return Ok(await _mediator.Send(request));
        }

        [Route("update")]
        [HttpPut]
        [ResponseType(typeof(AddOrUpdateGuestResponse))]
        public async Task<IHttpActionResult> Update(AddOrUpdateGuestRequest request)
        {
            request.TenantUniqueId = Request.GetTenantUniqueId();
            return Ok(await _mediator.Send(request));
        }
        
        [Route("get")]
        [AllowAnonymous]
        [HttpGet]
        [ResponseType(typeof(GetGuestsResponse))]
        public async Task<IHttpActionResult> Get()
        {
            var request = new GetGuestsRequest();
            request.TenantUniqueId = Request.GetTenantUniqueId();
            return Ok(await _mediator.Send(request));
        }

        [Route("getById")]
        [HttpGet]
        [ResponseType(typeof(GetGuestByIdResponse))]
        public async Task<IHttpActionResult> GetById([FromUri]GetGuestByIdRequest request)
        {
            request.TenantUniqueId = Request.GetTenantUniqueId();
            return Ok(await _mediator.Send(request));
        }

        [Route("remove")]
        [HttpDelete]
        [ResponseType(typeof(RemoveGuestResponse))]
        public async Task<IHttpActionResult> Remove([FromUri]RemoveGuestRequest request)
        {
            request.TenantUniqueId = Request.GetTenantUniqueId();
            return Ok(await _mediator.Send(request));
        }

        protected readonly IMediator _mediator;
    }
}
