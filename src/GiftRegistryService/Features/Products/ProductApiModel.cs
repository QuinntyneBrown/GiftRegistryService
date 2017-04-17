using GiftRegistryService.Data.Model;

namespace GiftRegistryService.Features.Products
{
    public class ProductApiModel
    {        
        public int Id { get; set; }
        public int? TenantId { get; set; }
        public string Name { get; set; }

        public static TModel FromProduct<TModel>(Product product) where
            TModel : ProductApiModel, new()
        {
            var model = new TModel();
            model.Id = product.Id;
            model.TenantId = product.TenantId;
            model.Name = product.Name;
            return model;
        }

        public static ProductApiModel FromProduct(Product product)
            => FromProduct<ProductApiModel>(product);

    }
}
