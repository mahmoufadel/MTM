using FluentMediator;
using MTM.Application.Services.Inventory.Dto;
using MTM.Domain;
using MTM.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;

namespace MTM.Application.Services.Transactions
{
	public class TransService : ITransService
	{
		IInventoryTransRepository _repository;
		public TransService(IInventoryTransRepository repository)
		{
			_repository = repository;
		}

		public void Create(InventoryTrans inventoryTrans)
		{
			_repository.Create(inventoryTrans);
			
		}

		public async Task CreateTrans(MTM.Domain.Model.Inventory inventory)
		{
			try
			{
				await _repository.Create(new InventoryTrans { TransId = Guid.NewGuid(), inventory = inventory, Price = 200, Qunatity = 900 });
			}
			catch (Exception ex)
			{

				throw;
			}
			
		}

		public AssetDto Get(Guid Id)
		{
			throw new NotImplementedException();
		}

		public async Task<List<InventoryTrans>> GetAll()
		{

			var res = await _repository.GetAll();
			return res;
		}
	}
}
