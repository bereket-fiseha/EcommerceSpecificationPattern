using Domain.Interface.Repository.Common;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repository.Common
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ECDbContext _dbContext;
        public UnitOfWork(ECDbContext dbContext)
        {


            _dbContext = dbContext;
        }
        public async Task SaveChangeAsync()
        {
            await _dbContext.SaveChangesAsync();
        }
    }
}
