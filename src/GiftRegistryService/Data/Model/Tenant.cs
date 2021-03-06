using GiftRegistryService.Data.Helpers;
using static GiftRegistryService.Constants;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;

namespace GiftRegistryService.Data.Model
{
    [SoftDelete("IsDeleted")]
    public class Tenant: ILoggable
    {
        public int Id { get; set; }

        [Index("UniqueIdIndex", IsUnique = true)]
        [Column(TypeName = "UNIQUEIDENTIFIER")]
        public Guid UniqueId { get; set; } = Guid.NewGuid();

        [Index("NameIndex", IsUnique = true)]
        [Column(TypeName = "VARCHAR")]
        [StringLength(MaxStringLength)]
        public string Name { get; set; }

        public string HostUrl { get; set; }

        public DateTime CreatedOn { get; set; }

        public DateTime LastModifiedOn { get; set; }

        public string CreatedBy { get; set; }

        public string LastModifiedBy { get; set; }

        public bool IsDeleted { get; set; }
    }
}
