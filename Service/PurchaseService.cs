using System.Threading.Tasks;
using VirtualMind.DAL.Repository;
using VirtualMind.Model;

namespace VirtualMind.Service
{
    public class PurchaseService
    {
        private readonly IRepository<Purchase> _purchase;

        public PurchaseService(IRepository<Purchase> purchase)
        {
            _purchase = purchase;
        }

        public async Task<Purchase> AddPurchase(Purchase purchase)
        {
            return await _purchase.Create(purchase);
        }
    }
}
