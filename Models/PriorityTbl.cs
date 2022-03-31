using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace PZLServiceDesk.Models
{
    [Table("PriorityTbl")]
    public partial class PriorityTbl
    {
        public PriorityTbl()
        {
            IssueTbls = new HashSet<IssueTbl>();
        }

        [Key]
        [Column("id")]
        public int Id { get; set; }
        [StringLength(150)]
        public string PriorityName { get; set; }
        public int? Prioritytime { get; set; }
        public string PriorityDesc { get; set; }
        public DateTime DateCreated { get; set; }

        [InverseProperty(nameof(IssueTbl.PriorityNavigation))]
        public virtual ICollection<IssueTbl> IssueTbls { get; set; }
    }
}
