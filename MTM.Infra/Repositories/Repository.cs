using MTM.Domain;
using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MTM.Infra.Repositories
{
    
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
    {       

        internal EFCoreDemoContext _context;
        internal DbSet<TEntity> dbSet;

        public  IQueryable<TEntity> FindAll()
		{
			return this.dbSet; 
        }
        public Repository(EFCoreDemoContext dbContext)
        {
            this._context = dbContext;
            this.dbSet = dbContext.Set<TEntity>();
        }

        public IQueryable<TEntity> Query()
        {
            return dbSet;
        }

        public async Task Create(TEntity entity)
        {
            await dbSet.AddAsync(entity);
            await _context.SaveChangesAsync();
        }



    }
}
