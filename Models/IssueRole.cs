using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace PZLServiceDesk.Models
{
    [Table("IssueRole")]
    public partial class IssueRole
    {
        public IssueRole()
        {
            Users = new HashSet<User>();
        }

        [Key]
        [Column("ID")]
        public int Id { get; set; }
        [StringLength(150)]
        public string Role { get; set; }
        [StringLength(150)]
        public string RoleAlias { get; set; }

        [InverseProperty(nameof(User.IssueRole))]
        public virtual ICollection<User> Users { get; set; }
    }
}
