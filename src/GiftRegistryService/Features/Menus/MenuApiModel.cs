using GiftRegistryService.Data.Model;

namespace GiftRegistryService.Features.Menus
{
    public class MenuApiModel
    {        
        public int Id { get; set; }
        public int? TenantId { get; set; }
        public string Name { get; set; }

        public static TModel FromMenu<TModel>(Menu menu) where
            TModel : MenuApiModel, new()
        {
            var model = new TModel();
            model.Id = menu.Id;
            model.TenantId = menu.TenantId;
            model.Name = menu.Name;
            return model;
        }

        public static MenuApiModel FromMenu(Menu menu)
            => FromMenu<MenuApiModel>(menu);

    }
}
