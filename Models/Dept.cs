using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace PZLServiceDesk.Models
{
    [Table("Dept")]
    public partial class Dept
    {
        public Dept()
        {
            Users = new HashSet<User>();
        }

        [Key]
        [Column("ID")]
        public int Id { get; set; }
        [Column("Dept")]
        [StringLength(150)]
        public string Dept1 { get; set; }
        [StringLength(150)]
        public string DeptAlias { get; set; }

        [InverseProperty(nameof(User.DeptDNavigation))]
        public virtual ICollection<User> Users { get; set; }
    }
}
