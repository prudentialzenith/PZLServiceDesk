using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace PZLServiceDesk.Models
{
    [Table("ApplicationTable")]
    public partial class ApplicationTable
    {
        [Key]
        [Column("ID")]
        public int Id { get; set; }
        [StringLength(150)]
        public string ApplicationName { get; set; }
        [StringLength(300)]
        public string ApplicationDescription { get; set; }
    }
}
