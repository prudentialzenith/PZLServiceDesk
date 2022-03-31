using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace PZLServiceDesk.Models
{
    [Table("Product")]
    public partial class Product
    {
        [Key]
        public int Id { get; set; }
        [StringLength(250)]
        public string Name { get; set; }
        [Column(TypeName = "money")]
        public decimal? Price { get; set; }
        public int? Quantity { get; set; }
        public bool? Status { get; set; }
    }
}
