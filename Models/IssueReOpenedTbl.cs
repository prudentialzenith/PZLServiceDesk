using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace PZLServiceDesk.Models
{
    [Table("IssueReOpenedTbl")]
    public partial class IssueReOpenedTbl
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }
        [Column("IssueID")]
        public int? IssueId { get; set; }
        [Column("ResolutionID")]
        public int? ResolutionId { get; set; }
        [StringLength(150)]
        public string IssueDesc { get; set; }
        public DateTime? Datecreated { get; set; }
    }
}
