using MediatR;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using GiftRegistryService.Features.Core;
using static GiftRegistryService.Features.Personalities.AddOrUpdatePersonalityCommand;
using static GiftRegistryService.Features.Personalities.GetPersonalitiesQuery;
using static GiftRegistryService.Features.Personalities.GetPersonalityByIdQuery;
using static GiftRegistryService.Features.Personalities.RemovePersonalityCommand;

namespace GiftRegistryService.Features.Personalities
{
    [Authorize]
    [RoutePrefix("api/personality")]
    public class PersonalityController : ApiController
    {
        public PersonalityController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [Route("add")]
        [HttpPost]
        [ResponseType(typeof(AddOrUpdatePersonalityResponse))]
        public async Task<IHttpActionResult> Add(AddOrUpdatePersonalityRequest request)
        {
            request.TenantUniqueId = Request.GetTenantUniqueId();
            return Ok(await _mediator.Send(request));
        }

        [Route("update")]
        [HttpPut]
        [ResponseType(typeof(AddOrUpdatePersonalityResponse))]
        public async Task<IHttpActionResult> Update(AddOrUpdatePersonalityRequest request)
        {
            request.TenantUniqueId = Request.GetTenantUniqueId();
            return Ok(await _mediator.Send(request));
        }
        
        [Route("get")]
        [AllowAnonymous]
        [HttpGet]
        [ResponseType(typeof(GetPersonalitiesResponse))]
        public async Task<IHttpActionResult> Get()
        {
            var request = new GetPersonalitiesRequest();
            request.TenantUniqueId = Request.GetTenantUniqueId();
            return Ok(await _mediator.Send(request));
        }

        [Route("getById")]
        [HttpGet]
        [ResponseType(typeof(GetPersonalityByIdResponse))]
        public async Task<IHttpActionResult> GetById([FromUri]GetPersonalityByIdRequest request)
        {
            request.TenantUniqueId = Request.GetTenantUniqueId();
            return Ok(await _mediator.Send(request));
        }

        [Route("remove")]
        [HttpDelete]
        [ResponseType(typeof(RemovePersonalityResponse))]
        public async Task<IHttpActionResult> Remove([FromUri]RemovePersonalityRequest request)
        {
            request.TenantUniqueId = Request.GetTenantUniqueId();
            return Ok(await _mediator.Send(request));
        }

        protected readonly IMediator _mediator;
    }
}
