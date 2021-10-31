using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MTM.Application;
using MTM.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MTM.API.Controllers
{
	[ApiController]	
	[Authorize]
	[Route("api/[controller]/[action]")]
	public class InventoryController : ControllerBase
	{
		IInventoryService _inventoryService;
		public InventoryController (IInventoryService inventoryService)
		{
			_inventoryService = inventoryService;
		}

		[HttpGet]
		[ActionName("GetAll")]
		public async Task<List<Inventory>> GetAll()
		{
			return await _inventoryService.GetAll();
			
		}
		[HttpPost]
		[ActionName("Create")]
		public async Task Create(Inventory  inventory)
		{
			 await _inventoryService.Create(inventory);
		}

		[HttpPost]
		[ActionName("BulkInsertAsync")]
		public async Task BulkInsertAsync(List<Inventory> inventories)
		{
			await _inventoryService.BulkInsertAsync(inventories);
		}
	}
}
