using MediatR;
using GiftRegistryService.Data;
using GiftRegistryService.Features.Core;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using System.Data.Entity;
using static GiftRegistryService.Features.Geolocation.GetAddressFromLatitudeAndLongitudeQuery;
using GiftRegistryService.Features.Geolocation;
using static GiftRegistryService.Features.Geolocation.GetLongLatCoordinatesQuery;

namespace GiftRegistryService.Features.Events
{
    public class GetClosestEventsQuery
    {
        public class GetClosestEventsRequest : IRequest<GetClosestEventsResponse>
        {
            public Guid TenantUniqueId { get; set; }
            public string Address { get; set; }
        }

        public class GetClosestEventsResponse
        {
            public ICollection<ClosetEventApiModel> Events { get; set; } = new HashSet<ClosetEventApiModel>();
        }

        public class GetClosestEventsHandler : IAsyncRequestHandler<GetClosestEventsRequest, GetClosestEventsResponse>
        {
            public GetClosestEventsHandler(GiftRegistryServiceContext context, IMediator mediator)
            {
                _context = context;
                _mediator = mediator;
            }

            public async Task<GetClosestEventsResponse> Handle(GetClosestEventsRequest request)
            {
                var utcNow = DateTime.UtcNow;
                
                var events = await _context.Events
                    .Include(x => x.Tenant)
                    .Include(x=>x.EventLocation)
                    .Where(x => x.Tenant.UniqueId == request.TenantUniqueId && x.Start > utcNow)
                    .ToListAsync();

                var longitudeAndLatitude = await _mediator.Send(
                    new GetLongLatCoordinatesRequest() { Address = request.Address });

                var closeEvents = events.Select(x => ClosetEventApiModel.FromEventAndOriginCoordinates(x, longitudeAndLatitude.Longitude, longitudeAndLatitude.Latitude))
                    .OrderBy(x => x.EventLocation.Distance)
                    .ToList();

                return new GetClosestEventsResponse()
                {
                    Events = closeEvents
                };
            }

            protected readonly IMediator _mediator;
            protected readonly GiftRegistryServiceContext _context;
        }

    }

}
