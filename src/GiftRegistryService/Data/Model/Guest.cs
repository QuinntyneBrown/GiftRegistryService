using System;
using System.Collections.Generic;
using GiftRegistryService.Data.Helpers;
using System.ComponentModel.DataAnnotations.Schema;

namespace GiftRegistryService.Data.Model
{
    [SoftDelete("IsDeleted")]
    public class Guest: ILoggable
    {
        public int Id { get; set; }
        
		[ForeignKey("Tenant")]
        public int? TenantId { get; set; }

        [ForeignKey("ContactRef")]
        public int? ContactRefId { get; set; }

        [ForeignKey("EventRef")]
        public int? EventRefId { get; set; }

        public bool IsAttending { get; set; }

        public DateTime CreatedOn { get; set; }
        
		public DateTime LastModifiedOn { get; set; }
        
		public string CreatedBy { get; set; }
        
		public string LastModifiedBy { get; set; }
        
		public bool IsDeleted { get; set; }

        public virtual Tenant Tenant { get; set; }

        public virtual EventRef EventRef { get; set; }

        public virtual ContactRef ContactRef { get; set; }
    }
}
