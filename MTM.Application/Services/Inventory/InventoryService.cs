using AutoMapper;
using MTM.Application.Services.Inventory.Dto;
using MTM.Domain;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using MTM.Domain.Model;
using FluentMediator;


namespace MTM.Application
{
	public class InventoryService : IInventoryService
	{
		IInventoryRepository _repository;
		private readonly IMediator _mediator;
		public InventoryService(IInventoryRepository repository, IMediator mediator) {
			_repository = repository;
			_mediator = mediator;
		}


		public async Task Create(Inventory inventory)
		{
			inventory.Id = Guid.NewGuid();
			await _repository.Create(inventory);
			await _mediator.PublishAsync(inventory);
		}

		public InventoryDto Get(Guid Id)
		{
			throw new NotImplementedException();
		}

		public async Task<List<Inventory>> GetAll()
		{
			var res =await  _repository.GetAll();
			return res;
		}

		public async Task BulkInsertAsync(List<Inventory> inventories)
		{
			await _repository.BulkInsertAsync(inventories);
			
		}
		


	}
}
