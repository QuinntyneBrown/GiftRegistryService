using MediatR;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using GiftRegistryService.Features.Core;
using static GiftRegistryService.Features.Menus.AddOrUpdateMenuItemCommand;
using static GiftRegistryService.Features.Menus.GetMenuItemsQuery;
using static GiftRegistryService.Features.Menus.GetMenuItemByIdQuery;
using static GiftRegistryService.Features.Menus.RemoveMenuItemCommand;

namespace GiftRegistryService.Features.Menus
{
    [Authorize]
    [RoutePrefix("api/menuItem")]
    public class MenuItemController : ApiController
    {
        public MenuItemController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [Route("add")]
        [HttpPost]
        [ResponseType(typeof(AddOrUpdateMenuItemResponse))]
        public async Task<IHttpActionResult> Add(AddOrUpdateMenuItemRequest request)
        {
            request.TenantUniqueId = Request.GetTenantUniqueId();
            return Ok(await _mediator.Send(request));
        }

        [Route("update")]
        [HttpPut]
        [ResponseType(typeof(AddOrUpdateMenuItemResponse))]
        public async Task<IHttpActionResult> Update(AddOrUpdateMenuItemRequest request)
        {
            request.TenantUniqueId = Request.GetTenantUniqueId();
            return Ok(await _mediator.Send(request));
        }
        
        [Route("get")]
        [AllowAnonymous]
        [HttpGet]
        [ResponseType(typeof(GetMenuItemsResponse))]
        public async Task<IHttpActionResult> Get()
        {
            var request = new GetMenuItemsRequest();
            request.TenantUniqueId = Request.GetTenantUniqueId();
            return Ok(await _mediator.Send(request));
        }

        [Route("getById")]
        [HttpGet]
        [ResponseType(typeof(GetMenuItemByIdResponse))]
        public async Task<IHttpActionResult> GetById([FromUri]GetMenuItemByIdRequest request)
        {
            request.TenantUniqueId = Request.GetTenantUniqueId();
            return Ok(await _mediator.Send(request));
        }

        [Route("remove")]
        [HttpDelete]
        [ResponseType(typeof(RemoveMenuItemResponse))]
        public async Task<IHttpActionResult> Remove([FromUri]RemoveMenuItemRequest request)
        {
            request.TenantUniqueId = Request.GetTenantUniqueId();
            return Ok(await _mediator.Send(request));
        }

        protected readonly IMediator _mediator;
    }
}
