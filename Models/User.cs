using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace PZLServiceDesk.Models
{
    [Table("User")]
    public partial class User
    {
        [Column("ID")]
        public int Id { get; set; }
        [Key]
        [StringLength(100)]
        public string Username { get; set; }
        [StringLength(300)]
        public string Password { get; set; }
        public int? DeptD { get; set; }
        [Column("IssueRoleID")]
        public int? IssueRoleId { get; set; }
        [Column("RequisionRoleID")]
        public int? RequisionRoleId { get; set; }
        [StringLength(100)]
        public string Email { get; set; }
        public DateTime? LastLogin { get; set; }
        [Column("SessionID")]
        public string SessionId { get; set; }
        public DateTime? SessionTime { get; set; }
        public DateTime DateCreated { get; set; }
        [Required]
        [StringLength(50)]
        public string Status { get; set; }
        public bool IsDefault { get; set; }
        public bool Islocked { get; set; }
        [Column("OTP")]
        [StringLength(50)]
        public string Otp { get; set; }

        [ForeignKey(nameof(DeptD))]
        [InverseProperty(nameof(Dept.Users))]
        public virtual Dept DeptDNavigation { get; set; }
        [ForeignKey(nameof(IssueRoleId))]
        [InverseProperty("Users")]
        public virtual IssueRole IssueRole { get; set; }
    }
}
