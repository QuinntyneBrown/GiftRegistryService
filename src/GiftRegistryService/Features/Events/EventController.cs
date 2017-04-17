using MediatR;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using GiftRegistryService.Features.Core;

using static GiftRegistryService.Features.Events.AddOrUpdateEventCommand;
using static GiftRegistryService.Features.Events.GetEventsQuery;
using static GiftRegistryService.Features.Events.GetEventByIdQuery;
using static GiftRegistryService.Features.Events.RemoveEventCommand;
using static GiftRegistryService.Features.Events.GetClosestEventsQuery;

namespace GiftRegistryService.Features.Events
{
    [Authorize]
    [RoutePrefix("api/event")]
    public class EventController : ApiController
    {
        public EventController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [Route("add")]
        [HttpPost]
        [ResponseType(typeof(AddOrUpdateEventResponse))]
        public async Task<IHttpActionResult> Add(AddOrUpdateEventRequest request)
        {
            request.TenantUniqueId = Request.GetTenantUniqueId();
            return Ok(await _mediator.Send(request));
        }

        [Route("update")]
        [HttpPut]
        [ResponseType(typeof(AddOrUpdateEventResponse))]
        public async Task<IHttpActionResult> Update(AddOrUpdateEventRequest request)
        {
            request.TenantUniqueId = Request.GetTenantUniqueId();
            return Ok(await _mediator.Send(request));
        }
        
        [Route("get")]
        [AllowAnonymous]
        [HttpGet]
        [ResponseType(typeof(GetEventsResponse))]
        public async Task<IHttpActionResult> Get()
        {
            var request = new GetEventsRequest();
            request.TenantUniqueId = Request.GetTenantUniqueId();
            return Ok(await _mediator.Send(request));
        }

        [Route("getClosest")]
        [AllowAnonymous]
        [HttpGet]
        [ResponseType(typeof(GetClosestEventsResponse))]
        public async Task<IHttpActionResult> GetClosestEvents([FromUri]GetClosestEventsRequest request)
        {            
            request.TenantUniqueId = Request.GetTenantUniqueId();
            return Ok(await _mediator.Send(request));
        }

        [Route("getById")]
        [HttpGet]
        [ResponseType(typeof(GetEventByIdResponse))]
        public async Task<IHttpActionResult> GetById([FromUri]GetEventByIdRequest request)
        {
            request.TenantUniqueId = Request.GetTenantUniqueId();
            return Ok(await _mediator.Send(request));
        }

        [Route("remove")]
        [HttpDelete]
        [ResponseType(typeof(RemoveEventResponse))]
        public async Task<IHttpActionResult> Remove([FromUri]RemoveEventRequest request)
        {
            request.TenantUniqueId = Request.GetTenantUniqueId();
            return Ok(await _mediator.Send(request));
        }

        protected readonly IMediator _mediator;
    }
}
