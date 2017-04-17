using MediatR;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using GiftRegistryService.Features.Core;
using static GiftRegistryService.Features.Contacts.AddOrUpdateContactCommand;
using static GiftRegistryService.Features.Contacts.GetContactsQuery;
using static GiftRegistryService.Features.Contacts.GetContactByIdQuery;
using static GiftRegistryService.Features.Contacts.RemoveContactCommand;

namespace GiftRegistryService.Features.Contacts
{
    [Authorize]
    [RoutePrefix("api/contact")]
    public class ContactController : ApiController
    {
        public ContactController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [Route("add")]
        [HttpPost]
        [ResponseType(typeof(AddOrUpdateContactResponse))]
        public async Task<IHttpActionResult> Add(AddOrUpdateContactRequest request)
        {
            request.TenantUniqueId = Request.GetTenantUniqueId();
            return Ok(await _mediator.Send(request));
        }

        [Route("update")]
        [HttpPut]
        [ResponseType(typeof(AddOrUpdateContactResponse))]
        public async Task<IHttpActionResult> Update(AddOrUpdateContactRequest request)
        {
            request.TenantUniqueId = Request.GetTenantUniqueId();
            return Ok(await _mediator.Send(request));
        }
        
        [Route("get")]
        [AllowAnonymous]
        [HttpGet]
        [ResponseType(typeof(GetContactsResponse))]
        public async Task<IHttpActionResult> Get()
        {
            var request = new GetContactsRequest();
            request.TenantUniqueId = Request.GetTenantUniqueId();
            return Ok(await _mediator.Send(request));
        }

        [Route("getById")]
        [HttpGet]
        [ResponseType(typeof(GetContactByIdResponse))]
        public async Task<IHttpActionResult> GetById([FromUri]GetContactByIdRequest request)
        {
            request.TenantUniqueId = Request.GetTenantUniqueId();
            return Ok(await _mediator.Send(request));
        }

        [Route("remove")]
        [HttpDelete]
        [ResponseType(typeof(RemoveContactResponse))]
        public async Task<IHttpActionResult> Remove([FromUri]RemoveContactRequest request)
        {
            request.TenantUniqueId = Request.GetTenantUniqueId();
            return Ok(await _mediator.Send(request));
        }

        protected readonly IMediator _mediator;
    }
}
