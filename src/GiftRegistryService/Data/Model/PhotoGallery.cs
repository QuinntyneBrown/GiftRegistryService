using GiftRegistryService.Data.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace GiftRegistryService.Data.Model
{
    [SoftDelete("IsDeleted")]
    public class PhotoGallery: ILoggable
    {
        public int Id { get; set; }
        
		[ForeignKey("Tenant")]
        public int? TenantId { get; set; }
        
		[Index("NameIndex", IsUnique = false)]
        [Column(TypeName = "VARCHAR")]        
		public string Name { get; set; }

        public string Description { get; set; }
        
		public DateTime CreatedOn { get; set; }
        
		public DateTime LastModifiedOn { get; set; }
        
		public string CreatedBy { get; set; }
        
		public string LastModifiedBy { get; set; }
        
		public bool IsDeleted { get; set; }

        public ICollection<PhotoGallerySlide> PhotoGallerySlides { get; set; } = new HashSet<PhotoGallerySlide>();

        public virtual Tenant Tenant { get; set; }
    }
}