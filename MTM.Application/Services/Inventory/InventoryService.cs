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
		private readonly IMapper _mapper;
		public InventoryService(IInventoryRepository repository, IMediator mediator, IMapper mapper) {
			_repository = repository;
			_mediator = mediator;
			_mapper = mapper;
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

		public async Task<List<InventoryDto>> GetAll()
		{
			var res =await  _repository.GetAll();
			
			return _mapper.Map<List<InventoryDto>>(res); 
		}

		public async Task BulkInsertAsync(List<Inventory> inventories)
		{
			await _repository.BulkInsertAsync(inventories);
			
		}
		


	}
}
