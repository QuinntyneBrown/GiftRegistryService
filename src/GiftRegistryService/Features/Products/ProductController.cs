using MediatR;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using GiftRegistryService.Features.Core;
using static GiftRegistryService.Features.Products.AddOrUpdateProductCommand;
using static GiftRegistryService.Features.Products.GetProductsQuery;
using static GiftRegistryService.Features.Products.GetProductByIdQuery;
using static GiftRegistryService.Features.Products.RemoveProductCommand;

namespace GiftRegistryService.Features.Products
{
    [Authorize]
    [RoutePrefix("api/product")]
    public class ProductController : ApiController
    {
        public ProductController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [Route("add")]
        [HttpPost]
        [ResponseType(typeof(AddOrUpdateProductResponse))]
        public async Task<IHttpActionResult> Add(AddOrUpdateProductRequest request)
        {
            request.TenantUniqueId = Request.GetTenantUniqueId();
            return Ok(await _mediator.Send(request));
        }

        [Route("update")]
        [HttpPut]
        [ResponseType(typeof(AddOrUpdateProductResponse))]
        public async Task<IHttpActionResult> Update(AddOrUpdateProductRequest request)
        {
            request.TenantUniqueId = Request.GetTenantUniqueId();
            return Ok(await _mediator.Send(request));
        }
        
        [Route("get")]
        [AllowAnonymous]
        [HttpGet]
        [ResponseType(typeof(GetProductsResponse))]
        public async Task<IHttpActionResult> Get()
        {
            var request = new GetProductsRequest();
            request.TenantUniqueId = Request.GetTenantUniqueId();
            return Ok(await _mediator.Send(request));
        }

        [Route("getById")]
        [HttpGet]
        [ResponseType(typeof(GetProductByIdResponse))]
        public async Task<IHttpActionResult> GetById([FromUri]GetProductByIdRequest request)
        {
            request.TenantUniqueId = Request.GetTenantUniqueId();
            return Ok(await _mediator.Send(request));
        }

        [Route("remove")]
        [HttpDelete]
        [ResponseType(typeof(RemoveProductResponse))]
        public async Task<IHttpActionResult> Remove([FromUri]RemoveProductRequest request)
        {
            request.TenantUniqueId = Request.GetTenantUniqueId();
            return Ok(await _mediator.Send(request));
        }

        protected readonly IMediator _mediator;
    }
}
