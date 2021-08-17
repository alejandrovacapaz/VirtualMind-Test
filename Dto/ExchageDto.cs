using VirtualMind.Model;

namespace VirtualMind
{
    public class ExchageDto
    {
        public int UserId { get; set; }
        public Currency Currency { get; set; }
        public decimal Amount { get; set; }
    }
}
