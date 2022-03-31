using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace PZLServiceDesk.Models
{
    [Table("SanctionScreeningSupportingDocument")]
    public partial class SanctionScreeningSupportingDocument
    {
        [Key]
        [Column("ID")]
        public int Id { get; set; }
        [Column("ClaimID")]
        public int? ClaimId { get; set; }
        [StringLength(250)]
        public string FileAddress { get; set; }
        [StringLength(150)]
        public string FileName { get; set; }
        [StringLength(150)]
        public string FileType { get; set; }
        public DateTime Datecreated { get; set; }
    }
}
