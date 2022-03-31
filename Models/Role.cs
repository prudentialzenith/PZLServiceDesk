using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace PZLServiceDesk.Models
{
    [Table("Role")]
    public partial class Role
    {
        public Role()
        {
            //Users = new HashSet<User>();
        }

        [Key]
        [Column("ID")]
        public int Id { get; set; }
        [Column("Role")]
        [StringLength(150)]
        public string Role1 { get; set; }
        [StringLength(150)]
        public string RoleAlias { get; set; }

       // [InverseProperty(nameof(User.Role))]
        //public virtual ICollection<User> Users { get; set; }
    }
}
