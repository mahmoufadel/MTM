using MTM.Application.Services.Inventory.Dto;
using MTM.Domain.Model;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MTM.Application.Services.Transactions
{
	public interface ITransService
	{
		void Create(InventoryTrans inventoryTrans);
		Task CreateTrans(Domain.Model.Inventory inventory);
		AssetDto Get(Guid Id);
		Task<List<InventoryTrans>> GetAll();
	}
}