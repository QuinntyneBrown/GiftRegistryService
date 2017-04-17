using MediatR;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using GiftRegistryService.Features.Core;
using static GiftRegistryService.Features.Menus.AddOrUpdateMenuCommand;
using static GiftRegistryService.Features.Menus.GetMenusQuery;
using static GiftRegistryService.Features.Menus.GetMenuByIdQuery;
using static GiftRegistryService.Features.Menus.RemoveMenuCommand;

namespace GiftRegistryService.Features.Menus
{
    [Authorize]
    [RoutePrefix("api/menu")]
    public class MenuController : ApiController
    {
        public MenuController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [Route("add")]
        [HttpPost]
        [ResponseType(typeof(AddOrUpdateMenuResponse))]
        public async Task<IHttpActionResult> Add(AddOrUpdateMenuRequest request)
        {
            request.TenantUniqueId = Request.GetTenantUniqueId();
            return Ok(await _mediator.Send(request));
        }

        [Route("update")]
        [HttpPut]
        [ResponseType(typeof(AddOrUpdateMenuResponse))]
        public async Task<IHttpActionResult> Update(AddOrUpdateMenuRequest request)
        {
            request.TenantUniqueId = Request.GetTenantUniqueId();
            return Ok(await _mediator.Send(request));
        }
        
        [Route("get")]
        [AllowAnonymous]
        [HttpGet]
        [ResponseType(typeof(GetMenusResponse))]
        public async Task<IHttpActionResult> Get()
        {
            var request = new GetMenusRequest();
            request.TenantUniqueId = Request.GetTenantUniqueId();
            return Ok(await _mediator.Send(request));
        }

        [Route("getById")]
        [HttpGet]
        [ResponseType(typeof(GetMenuByIdResponse))]
        public async Task<IHttpActionResult> GetById([FromUri]GetMenuByIdRequest request)
        {
            request.TenantUniqueId = Request.GetTenantUniqueId();
            return Ok(await _mediator.Send(request));
        }

        [Route("remove")]
        [HttpDelete]
        [ResponseType(typeof(RemoveMenuResponse))]
        public async Task<IHttpActionResult> Remove([FromUri]RemoveMenuRequest request)
        {
            request.TenantUniqueId = Request.GetTenantUniqueId();
            return Ok(await _mediator.Send(request));
        }

        protected readonly IMediator _mediator;
    }
}
