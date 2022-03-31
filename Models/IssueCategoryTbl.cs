using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace PZLServiceDesk.Models
{
    [Table("IssueCategoryTbl")]
    public partial class IssueCategoryTbl
    {
        public IssueCategoryTbl()
        {
            IssueTbls = new HashSet<IssueTbl>();
        }

        [Key]
        [Column("id")]
        public int Id { get; set; }
        [StringLength(250)]
        public string Category { get; set; }
        [StringLength(250)]
        public string CategoryAssignee { get; set; }
        [StringLength(250)]
        public string AsigneeEmail { get; set; }
        public DateTime Datecreated { get; set; }

        [InverseProperty(nameof(IssueTbl.CategoryNavigation))]
        public virtual ICollection<IssueTbl> IssueTbls { get; set; }
    }
}
