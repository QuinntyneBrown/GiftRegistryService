using MediatR;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using GiftRegistryService.Features.Core;
using static GiftRegistryService.Features.ContentBlocks.AddOrUpdateContentBlockCommand;
using static GiftRegistryService.Features.ContentBlocks.GetContentBlocksQuery;
using static GiftRegistryService.Features.ContentBlocks.GetContentBlockByIdQuery;
using static GiftRegistryService.Features.ContentBlocks.RemoveContentBlockCommand;

namespace GiftRegistryService.Features.ContentBlocks
{
    [Authorize]
    [RoutePrefix("api/contentBlock")]
    public class ContentBlockController : ApiController
    {
        public ContentBlockController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [Route("add")]
        [HttpPost]
        [ResponseType(typeof(AddOrUpdateContentBlockResponse))]
        public async Task<IHttpActionResult> Add(AddOrUpdateContentBlockRequest request)
        {
            request.TenantUniqueId = Request.GetTenantUniqueId();
            return Ok(await _mediator.Send(request));
        }

        [Route("update")]
        [HttpPut]
        [ResponseType(typeof(AddOrUpdateContentBlockResponse))]
        public async Task<IHttpActionResult> Update(AddOrUpdateContentBlockRequest request)
        {
            request.TenantUniqueId = Request.GetTenantUniqueId();
            return Ok(await _mediator.Send(request));
        }
        
        [Route("get")]
        [AllowAnonymous]
        [HttpGet]
        [ResponseType(typeof(GetContentBlocksResponse))]
        public async Task<IHttpActionResult> Get()
        {
            var request = new GetContentBlocksRequest();
            request.TenantUniqueId = Request.GetTenantUniqueId();
            return Ok(await _mediator.Send(request));
        }

        [Route("getById")]
        [HttpGet]
        [ResponseType(typeof(GetContentBlockByIdResponse))]
        public async Task<IHttpActionResult> GetById([FromUri]GetContentBlockByIdRequest request)
        {
            request.TenantUniqueId = Request.GetTenantUniqueId();
            return Ok(await _mediator.Send(request));
        }

        [Route("remove")]
        [HttpDelete]
        [ResponseType(typeof(RemoveContentBlockResponse))]
        public async Task<IHttpActionResult> Remove([FromUri]RemoveContentBlockRequest request)
        {
            request.TenantUniqueId = Request.GetTenantUniqueId();
            return Ok(await _mediator.Send(request));
        }

        protected readonly IMediator _mediator;
    }
}
