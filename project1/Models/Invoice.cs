
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
    using System.Web;
    using System.Linq;

 namespace project1.Models
    {
        [Table("Invoices")]
        public class Invoice
        {
        [Key]
        public int Id { get; set; }
        [Required]
        [StringLength(20)]
        [Column("Invoice")]
        public string InvoiceNumber { get; set; }
        [Required]
        public DateTime Date { get; set; }
        [Required]
        [StringLength(100)]
        public string Customer { get; set; }
        [Required]
        public decimal Total { get; set; }
        }
    }
