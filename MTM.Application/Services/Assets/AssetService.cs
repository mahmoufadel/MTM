using AutoMapper;
using MTM.Application.Services.Inventory.Dto;
using MTM.Domain;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using MTM.Domain.Model;
using FluentMediator;
using StackExchange.Redis.Extensions.Core.Abstractions;
using MTM.Cache.Redis;

namespace MTM.Application
{
	public class AssetService : IAssetService
	{
		IAssetRepository _repository;
		private readonly IMediator _mediator;
		private readonly IMapper _mapper;
		private readonly ICacheManager _cache;
		public AssetService(IAssetRepository repository, IMediator mediator, IMapper mapper,
			ICacheManager cacheManager) {
			_repository = repository;
			_mediator = mediator;
			_mapper = mapper;
			_cache = cacheManager;
		}


		public async Task Create(AssetDto assetDto)
		{
			await _cache.AddAsync(assetDto.Id.ToString(), assetDto);
			var asset = _mapper.Map<Asset>(assetDto);			
			await _repository.Create(asset);
			
		}

		public async Task<AssetDto> Get(Guid Id)
		{
			var ob=await _cache.GetAsync<AssetDto>(Id.ToString());
			if (ob != null)
				return ob;

			var res= await _repository.Get(Id);
			return _mapper.Map<AssetDto>(res);
		}

		public async Task<List<AssetDto>> GetAll(AssetDto assetDto)
		{
			var res =await  _repository.GetAll(_mapper.Map<Asset>(assetDto));
			
			return _mapper.Map<List<AssetDto>>(res); 
		}

		public async Task BulkInsertAsync(List<AssetDto> inventories)
		{
			await _repository.BulkInsertAsync(_mapper.Map<List<Asset>>(inventories));			
		}		

		public void Update(AssetDto assetDto)
		{
			 _repository.Update(_mapper.Map<Asset>(assetDto));
		}

		public void AssignToUser(AssetDto assetDto, Guid UserId)
		{
			assetDto.UserId = UserId;
			_repository.Update(_mapper.Map<Asset>(assetDto));
		}

		
	}
}
