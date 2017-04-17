using GiftRegistryService.Data.Helpers;
using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace GiftRegistryService.Data.Model
{
    [SoftDelete("IsDeleted")]
    public class EventLocation: Location, ILoggable
    {
        [Key, ForeignKey("Event")]
        public int EventLocationId { get; set; }
            
        [ForeignKey("Tenant")]
        public int? TenantId { get; set; }
        
        public DateTime CreatedOn { get; set; }
        
		public DateTime LastModifiedOn { get; set; }
        
		public string CreatedBy { get; set; }
        
		public string LastModifiedBy { get; set; }
        
		public bool IsDeleted { get; set; }

        public virtual Tenant Tenant { get; set; }

        public virtual Event Event { get; set; }
    }
}
