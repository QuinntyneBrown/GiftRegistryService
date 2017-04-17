using GiftRegistryService.Data.Model;

namespace GiftRegistryService.Features.Menus
{
    public class MenuItemApiModel
    {        
        public int Id { get; set; }
        public int? TenantId { get; set; }
        public string Name { get; set; }

        public static TModel FromMenuItem<TModel>(MenuItem menuItem) where
            TModel : MenuItemApiModel, new()
        {
            var model = new TModel();
            model.Id = menuItem.Id;
            model.TenantId = menuItem.TenantId;
            model.Name = menuItem.Name;
            return model;
        }

        public static MenuItemApiModel FromMenuItem(MenuItem menuItem)
            => FromMenuItem<MenuItemApiModel>(menuItem);

    }
}
