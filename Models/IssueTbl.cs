using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace PZLServiceDesk.Models
{
    [Table("IssueTbl")]
    public partial class IssueTbl
    {
        [Key]
        [Column("ID")]
        public int Id { get; set; }
        public int? Category { get; set; }
        [StringLength(300)]
        public string Subject { get; set; }
        public string Description { get; set; }
        [StringLength(150)]
        public string Status { get; set; }
        [StringLength(200)]
        public string CreatedBy { get; set; }
        public int Priority { get; set; }
        public DateTime? DateCreated { get; set; }
        [StringLength(300)]
        public string AssinedTo { get; set; }
        [StringLength(200)]
        public string ResolvedBy { get; set; }
        public DateTime? DateResolved { get; set; }

        [ForeignKey(nameof(Category))]
        [InverseProperty(nameof(IssueCategoryTbl.IssueTbls))]
        public virtual IssueCategoryTbl CategoryNavigation { get; set; }
        [ForeignKey(nameof(Priority))]
        [InverseProperty(nameof(PriorityTbl.IssueTbls))]
        public virtual PriorityTbl PriorityNavigation { get; set; }
    }
}
