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
	public class AssetRepository : Repository<Asset>, IAssetRepository
	{
		EFCoreDemoContext _dbContext;
		public AssetRepository(EFCoreDemoContext dbContext) : base(dbContext)
		{
			_dbContext = dbContext;
		}
		public async Task<List<Asset>> GetAll(Asset asset)
		{
			var q = this.Query();
			if (asset.UserId.HasValue) q=q.Where(a => a.UserId == asset.UserId);
			var res = await q.ToListAsync();
			return res;
		}

		public async Task BulkInsertAsync(List<Asset> assets)
		{
			await this._context.BulkInsertAsync(assets);
		}
		public override void Update(Asset asset)
		{
			_dbContext.Assets.Where(r =>r.Id==asset.Id).BatchUpdate(r => new Asset { UserId = asset.UserId });			
		}

		public async Task<Asset> Get(Guid Id)
		{
			return await this.Query().FirstOrDefaultAsync(a => a.Id == Id);
		}
	}

}

