using MTM.Application.Services.Inventory.Dto;
using MTM.Domain.Model;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MTM.Application
{
	public interface IAssetService
	{
		Task Create(AssetDto assetDto);
		void Update(AssetDto assetDto);

		Task<AssetDto> Get(Guid Id);
		Task<List<AssetDto>> GetAll(AssetDto assetDto);

		Task BulkInsertAsync(List<AssetDto> assets);
		void AssignToUser(AssetDto assetDto, Guid UserId);


	}
}