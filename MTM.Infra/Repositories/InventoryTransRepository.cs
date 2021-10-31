using MTM.Domain.Inventories;
using MTM.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using MTM.Domain;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace MTM.Infra.Repositories
{
	public class InventoryTransRepository : Repository<InventoryTrans>, IInventoryTransRepository
	{
		public InventoryTransRepository(EFCoreDemoContext dbContext) : base(dbContext)
		{

		}
		public async Task<List<InventoryTrans>> GetAll()
		{
			var res = await this.Query().ToListAsync();
			return res;
		}


	}



}

