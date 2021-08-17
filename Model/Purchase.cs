using System;
using System.ComponentModel.DataAnnotations;

namespace VirtualMind.Model
{
    public class Purchase
    {
        [Key]
        [Required]
        public int PurchaseId { get; set; }
        [Required]
        public int UserId { get; set; }
        [Required]
        [EnumDataType(typeof(Currency))]
        public Currency Currency { get; set; }
        [Required]
        public decimal Amount { get; set; }
        [Required]
        public decimal Exchange { get; set; }
        public decimal Result { get; set; }
        public DateTime Date { get; set; }
    }

    public enum Currency
    {
        USD, BRL, CAD
    }
}
