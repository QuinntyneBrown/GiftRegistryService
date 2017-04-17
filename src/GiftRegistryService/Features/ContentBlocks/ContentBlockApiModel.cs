using GiftRegistryService.Data.Model;

namespace GiftRegistryService.Features.ContentBlocks
{
    public class ContentBlockApiModel
    {        
        public int Id { get; set; }
        public int? TenantId { get; set; }
        public string Name { get; set; }

        public static TModel FromContentBlock<TModel>(ContentBlock contentBlock) where
            TModel : ContentBlockApiModel, new()
        {
            var model = new TModel();
            model.Id = contentBlock.Id;
            model.TenantId = contentBlock.TenantId;
            model.Name = contentBlock.Name;
            return model;
        }

        public static ContentBlockApiModel FromContentBlock(ContentBlock contentBlock)
            => FromContentBlock<ContentBlockApiModel>(contentBlock);

    }
}
