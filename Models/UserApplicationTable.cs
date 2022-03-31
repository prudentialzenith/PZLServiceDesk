using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace PZLServiceDesk.Models
{
    [Table("UserApplicationTable")]
    public partial class UserApplicationTable
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }
        [Column("ApplicationID")]
        public int? ApplicationId { get; set; }
        [Column("UserID")]
        public int? UserId { get; set; }
        [StringLength(200)]
        public string Username { get; set; }
        [Column("password")]
        [StringLength(300)]
        public string Password { get; set; }
        [StringLength(100)]
        public string Token { get; set; }
        public int? DeptD { get; set; }
        [StringLength(100)]
        public string Email { get; set; }
        public DateTime? LastLogin { get; set; }
        [Column("SessionID")]
        [StringLength(10)]
        public string SessionId { get; set; }
        public DateTime? SessionTime { get; set; }
        public DateTime? DateCreated { get; set; }
        [StringLength(50)]
        public string Status { get; set; }
        public bool? IsDefault { get; set; }
    }
}
