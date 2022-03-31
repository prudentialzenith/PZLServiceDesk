using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace PZLServiceDesk.Models
{
    [Table("IssueTypeTbl")]
    public partial class IssueTypeTbl
    {
        public IssueTypeTbl()
        {
            IssueTbls = new HashSet<IssueTbl>();
        }

        [Key]
        [Column("id")]
        public int Id { get; set; }
        [StringLength(250)]
        public string IssueName { get; set; }
        [StringLength(250)]
        public string IssueAssignee { get; set; }
        [StringLength(250)]
        public string AsigneeEmail { get; set; }
        public DateTime Datecreated { get; set; }

  
        public virtual ICollection<IssueTbl> IssueTbls { get; set; }
    }
}
