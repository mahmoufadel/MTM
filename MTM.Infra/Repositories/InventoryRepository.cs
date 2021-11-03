using MTM.Domain.Inventories;
using MTM.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using MTM.Domain;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using EFCore.BulkExtensions;

namespace MTM.Infra.Repositories
{
	public class InventoryRepository : Repository<Inventory>, IInventoryRepository
	{
		public InventoryRepository(EFCoreDemoContext dbContext) : base(dbContext)
		{

		}
		public async Task<List<Inventory>> GetAll()
		{
			var q = this.Query();
			/*if (string.IsNullOrEmpty(inventory.Summary))
				q.Where(i => i.Summary == inventory.Summary);*/

			var res = await q.ToListAsync();
			return res;
		}

		public async Task BulkInsertAsync(List<Inventory> inventories)
		{
			/*
		  context.BulkInsert(entitiesList);                 context.BulkInsertAsync(entitiesList);
context.BulkInsertOrUpdate(entitiesList);         context.BulkInsertOrUpdateAsync(entitiesList);      //Upsert
context.BulkInsertOrUpdateOrDelete(entitiesList); context.BulkInsertOrUpdateOrDeleteAsync(entitiesList);//Sync
context.BulkUpdate(entitiesList);                 context.BulkUpdateAsync(entitiesList);
context.BulkDelete(entitiesList);                 context.BulkDeleteAsync(entitiesList);
context.BulkRead(entitiesList);                   context.BulkReadAsync(entitiesList);
context.Truncate<Entity>();                       context.TruncateAsync<Entity>();

		  */
			await this._context.BulkInsertAsync(inventories);

		}
		

		}

}

