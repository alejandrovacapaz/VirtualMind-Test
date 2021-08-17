using System;
using System.Collections.Generic;
using System.Linq;
using VirtualMind.DAL.Repository;
using VirtualMind.Model;

namespace VirtualMind.Service
{
    public class LimitService
    {
        private readonly IRepository<Limit> _limit;

        public LimitService(IRepository<Limit> limit)
        {

            _limit = limit;
        }

        public IEnumerable<Limit> GetRange(int userId, Currency currency, DateTime date)
        {
            return _limit.GetAll().Where(x => x.UserId == userId && x.Currency == currency
                && x.EndDate >= date && x.StartDate <= date).ToList();
        }
    }
}
