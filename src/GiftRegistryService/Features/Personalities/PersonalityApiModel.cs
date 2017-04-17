using GiftRegistryService.Data.Model;

namespace GiftRegistryService.Features.Personalities
{
    public class PersonalityApiModel
    {        
        public int Id { get; set; }

        public int? TenantId { get; set; }

        public string Name { get; set; }

        public string Firstname { get; set; }

        public string Lastname { get; set; }

        public string ImageUrl { get; set; }

        public string Abstract { get; set; }

        public string Description { get; set; }


        public static TModel FromPersonality<TModel>(Personality personality) where
            TModel : PersonalityApiModel, new()
        {
            var model = new TModel();
            model.Id = personality.Id;
            model.TenantId = personality.TenantId;
            model.Name = personality.Name;
            return model;
        }

        public static PersonalityApiModel FromPersonality(Personality personality)
            => FromPersonality<PersonalityApiModel>(personality);

    }
}
