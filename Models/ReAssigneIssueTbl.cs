using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace PZLServiceDesk.Models
{
    [Table("ReAssigneIssueTbl")]
    public partial class ReAssigneIssueTbl
    {
        [Column("id")]
        public int Id { get; set; }
        [Column("IssueID")]
        public int? IssueId { get; set; }
        [StringLength(150)]
        public string PreviousAssignee { get; set; }
        [StringLength(150)]
        public string NewAssignee { get; set; }
        [Key]
        public DateTime DateAssigned { get; set; }
    }
}
