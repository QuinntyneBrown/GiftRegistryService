using GiftRegistryService.Data.Helpers;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace GiftRegistryService.Data.Model
{
    [SoftDelete("IsDeleted")]
    public class Event: ILoggable
    {
        public int Id { get; set; }
        
		[ForeignKey("Tenant")]
        public int? TenantId { get; set; }
        
		[Index("NameIndex", IsUnique = false)]
        [Column(TypeName = "VARCHAR")]        
		public string Name { get; set; }

        public string Url { get; set; }

        public string ImageUrl { get; set; }

        public string Description { get; set; }

        public string Abstract { get; set; }
        
        public DateTime? Start { get; set; }

        public DateTime? End { get; set; }

        public ICollection<TagRef> EventTagRefs { get; set; } = new HashSet<TagRef>();

        public ICollection<CategoryRef> EventCategoryRefs { get; set; } = new HashSet<CategoryRef>();

        public DateTime CreatedOn { get; set; }
        
		public DateTime LastModifiedOn { get; set; }
        
		public string CreatedBy { get; set; }
        
		public string LastModifiedBy { get; set; }
        
		public bool IsDeleted { get; set; }

        public virtual Tenant Tenant { get; set; }

        public virtual EventLocation EventLocation { get; set; }
    }
}
