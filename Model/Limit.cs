using System;
using System.ComponentModel.DataAnnotations;

namespace VirtualMind.Model
{
    public class Limit
    {
        [Key]
        [Required]
        public int LimitId { get; set; }
        [Required]
        public DateTime StartDate { get; set; }
        [Required]
        public DateTime EndDate { get; set; }
        [Required]
        public int UserId { get; set; }
        [Required]
        [EnumDataType(typeof(Currency))]
        public Currency Currency { get; set; }
        [Required]
        public decimal Amount { get; set; }

    }
}
