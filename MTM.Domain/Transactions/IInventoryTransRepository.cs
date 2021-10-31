using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MTM.Domain.Model;

namespace MTM.Domain
{
	public interface IInventoryTransRepository : IRepository<InventoryTrans>
	{
		Task<List<InventoryTrans>> GetAll();
	}
}
