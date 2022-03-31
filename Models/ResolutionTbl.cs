using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace PZLServiceDesk.Models
{
    [Table("ResolutionTbl")]
    public partial class ResolutionTbl
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }
        [Column("issueID")]
        public int? IssueId { get; set; }
        public string ResolutionDesc { get; set; }
        [Column("status")]
        [StringLength(150)]
        public string Status { get; set; }
        [StringLength(250)]
        public string Resolvedby { get; set; }
        public int? ResSeq { get; set; }
        public DateTime DateResolved { get; set; }
    }
}
