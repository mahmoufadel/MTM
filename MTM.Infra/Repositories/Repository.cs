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
        
        public virtual void Delete(object id)
        {
            TEntity entityToDelete = dbSet.Find(id);
            Delete(entityToDelete);
        }
        public virtual void Delete(TEntity entityToDelete)
        {
            if (entityToDelete != null)
            {
                if (_context.Entry(entityToDelete).State == EntityState.Detached)
                {
                    dbSet.Attach(entityToDelete);
                }
                dbSet.Remove(entityToDelete);
            }
        }

        public async Task Create(TEntity entity)
        {
            await dbSet.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public virtual void Update(TEntity entityToUpdate)
        {

            dbSet.Attach(entityToUpdate);
            _context.Entry(entityToUpdate).State = EntityState.Modified;
            _context.SaveChanges();
        }
        public virtual void Update(TEntity entityToUpdate, params object[] key)
        {
            var entry = this._context.Entry(entityToUpdate);

            if (entry.State == EntityState.Detached)
            {
                var currentEntry = this.dbSet.Find(key);
                if (currentEntry != null)
                {
                    var attachedEntry = this._context.Entry(currentEntry);
                    attachedEntry.CurrentValues.SetValues(entityToUpdate);
                }
                else
                {
                    this.dbSet.Attach(entityToUpdate);
                    entry.State = EntityState.Modified;
                }
            }
        }



    }
}
