using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VirtualMind.Model;

namespace VirtualMind.DAL.Repository
{
    public class RepositoryPurchase : IRepository<Purchase>
    {
        ApplicationDBContext _dbContext;
        public RepositoryPurchase(ApplicationDBContext applicationDbContext)
        {
            _dbContext = applicationDbContext;
        }

        public async Task<Purchase> Create(Purchase _object)
        {
            var obj = await _dbContext.Purchases.AddAsync(_object);
            _dbContext.SaveChanges();
            return obj.Entity;
        }

        public void Delete(Purchase _object)
        {
            _dbContext.Remove(_object);
            _dbContext.SaveChanges();
        }

        public IEnumerable<Purchase> GetAll()
        {
            return _dbContext.Purchases.ToList();
        }

        public Purchase GetById(int Id)
        {
            return _dbContext.Purchases.Where(x => x.PurchaseId == Id).FirstOrDefault();
        }

        public void Update(Purchase _object)
        {
            _dbContext.Purchases.Update(_object);
            _dbContext.SaveChanges(); throw new NotImplementedException();
        }
    }
}
