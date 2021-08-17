using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VirtualMind.Model;

namespace VirtualMind.DAL.Repository
{
    public class RepositoryLimit : IRepository<Limit>
    {
        ApplicationDBContext _dbContext;

        public RepositoryLimit(ApplicationDBContext applicationDbContext)
        {
            _dbContext = applicationDbContext;
        }
        public async Task<Limit> Create(Limit _object)
        {
            var obj = await _dbContext.Limits.AddAsync(_object);
            _dbContext.SaveChanges();
            return obj.Entity;
        }

        public void Delete(Limit _object)
        {
            _dbContext.Remove(_object);
            _dbContext.SaveChanges();
        }

        public IEnumerable<Limit> GetAll()
        {
            return _dbContext.Limits.ToList();
        }

        public Limit GetById(int Id)
        {
            return _dbContext.Limits.Where(x => x.LimitId == Id).FirstOrDefault();
        }

        public void Update(Limit _object)
        {
            _dbContext.Limits.Update(_object);
            _dbContext.SaveChanges();
        }
    }
}
