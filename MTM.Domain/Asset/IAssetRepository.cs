using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MTM.Domain.Model;

namespace MTM.Domain
{
	public interface IAssetRepository : IRepository<Asset>
	{
		Task<Asset> Get(Guid Id);
		Task<List<Asset>> GetAll(Asset asset);
		Task BulkInsertAsync(List<Asset> inventories);		
	}
}
