using System;
using System.Linq;
using VirtualMind.DAL;

namespace VirtualMind.Model
{
    public static class SeedData
    {
        public static void Initialize()
        {
            using (var context = new ApplicationDBContext())
            {
                if (context.Limits.Any())
                {
                    return;
                }

                context.Limits.AddRange(
                    new Limit
                    {
                        UserId = 1,
                        StartDate = DateTime.Parse("2021-08-01"),
                        EndDate = DateTime.Parse("2021-08-31"),
                        Currency = Currency.USD,
                        Amount = 200
                    },

                    new Limit
                    {
                        UserId = 1,
                        StartDate = DateTime.Parse("2021-08-01"),
                        EndDate = DateTime.Parse("2021-08-31"),
                        Currency = Currency.BRL,
                        Amount = 300
                    },

                    new Limit
                    {
                        UserId = 2,
                        StartDate = DateTime.Parse("2021-08-01"),
                        EndDate = DateTime.Parse("2021-08-31"),
                        Currency = Currency.USD,
                        Amount = 1000
                    },

                    new Limit
                    {
                        UserId = 2,
                        StartDate = DateTime.Parse("2021-08-01"),
                        EndDate = DateTime.Parse("2021-08-31"),
                        Currency = Currency.BRL,
                        Amount = 900
                    }
                );
                context.SaveChanges();
            }
        }
    }
}
