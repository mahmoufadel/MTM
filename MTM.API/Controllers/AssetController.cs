using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MTM.Application;
using MTM.Application.Services.Inventory.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MTM.API.Controllers
{
	[ApiController]

	[Route("api/[controller]/[action]")]
	//[Authorize]
	public class AssetController : ControllerBase
	{
		IAssetService  _assetService;

		public AssetController(IAssetService assetService) 
		{
			_assetService = assetService;
		}

		[HttpGet]
		[ActionName("Get")]
		public async Task<AssetDto> Get(Guid Id)
		{
			return await _assetService.Get(Id);
		}

		[HttpPost]
		[ActionName("Create")]
		public async Task Create(AssetDto assetDto)
		{
			await _assetService.Create(assetDto);

		}

		[HttpGet]
		[ActionName("GetAll")]		
		public async Task<List<AssetDto>> GetAll(AssetDto  assetDto)
		{
			return await _assetService.GetAll(assetDto);

		}

		[HttpPost]
		[ActionName("AssignToUser")]
		public void Assign(AssetDto assetDto,Guid UserId)
		{
			 _assetService.AssignToUser(assetDto, UserId);

		}
	}
}
