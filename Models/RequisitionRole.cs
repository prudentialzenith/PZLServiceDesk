using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace PZLServiceDesk.Models
{
    [Table("RequisitionRole")]
    public partial class RequisitionRole
    {
        [Key]
        [Column("ID")]
        public int Id { get; set; }
        [StringLength(150)]
        public string Role { get; set; }
        [StringLength(150)]
        public string RoleAlias { get; set; }
    }
}
