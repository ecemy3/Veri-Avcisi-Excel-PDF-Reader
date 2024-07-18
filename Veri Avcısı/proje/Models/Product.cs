using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection.Emit;
using System.Web;

namespace proje.Models
{
    public class Product
    {
        [Key]
        public int productID { get; set; }
        [Required]
        [MaxLength(100)]
        public string ProductName { get; set; }
        [Required]
        [MaxLength(50)]
        public string ProductCode { get; set; }
        [Required]
        [MaxLength(50)]
        public string ProductDetail { get; set; }
        [Required]
        public decimal UnitPrice { get; set; }
    }
}