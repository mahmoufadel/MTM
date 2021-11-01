using MTM.Application.Services.Inventory.Dto;
using MTM.Domain.Model;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MTM.Application
{
	public interface IInventoryService
	{
		Task Create(Inventory inventoryDto);

		InventoryDto Get(Guid Id);
		Task<List<InventoryDto>> GetAll();

		Task BulkInsertAsync(List<Inventory> inventories);
	}
}