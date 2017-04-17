using GiftRegistryService.Data.Model;

namespace GiftRegistryService.Features.Guests
{
    public class GuestApiModel
    {        
        public int Id { get; set; }
        public int? TenantId { get; set; }

        public static TModel FromGuest<TModel>(Guest guest) where
            TModel : GuestApiModel, new()
        {
            var model = new TModel();
            model.Id = guest.Id;
            model.TenantId = guest.TenantId;
            return model;
        }

        public static GuestApiModel FromGuest(Guest guest)
            => FromGuest<GuestApiModel>(guest);

    }
}
