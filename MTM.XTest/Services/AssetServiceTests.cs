using Moq;
using MTM.Domain;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using FluentMediator;
using System.Threading.Tasks;
using Xunit;
using MTM.Cache.Redis;
using AutoMapper;
using MTM.Application;
using MTM.Application.Services.Inventory.Dto;
using MTM.Infra;
using Microsoft.EntityFrameworkCore;
using MTM.Infra.Repositories;
using MTM.Domain.Model;
using MTM.Application.Services.Transactions;

namespace MTM.XTest.Services
{
    public class AssetServiceTests : BaseServiceTests
    {
        private IAssetService assetService;
        public AssetServiceTests() : base()
        {
            assetService = new AssetService(repository, _mockIMediator.Object, _mapper, cacheManager);
        }


        [Fact]
        public async Task Create_Success()
        {
            //Arrange            

            //Act
            var Id = Guid.NewGuid();
            
            await assetService.Create(new AssetDto()
            {
                Id = Id,
                code = "123",
                CreatedBy = Guid.NewGuid(),
                UserId = Guid.NewGuid(),
                AssetType = Domain.Enum.Enums.EAssetType.TypeTow
            });

            var asset = await assetService.Get(Id);
            //Assert          
            Assert.Equal(asset.Id, Id);
        }

        [Fact]
        public async Task GetAll_Success()
        {
            var UserId = Guid.NewGuid();
            var Id = Guid.NewGuid();
            await assetService.Create(new AssetDto()
            {
                Id = Id,
                code = "123",
                CreatedBy = Guid.NewGuid(),
                UserId = UserId,
                AssetType = Domain.Enum.Enums.EAssetType.TypeTow
            });

           var res=await assetService.GetAll(new AssetDto() { UserId = UserId });
            Assert.Equal(res.Count, 1);
        }

        [Fact]
        public async Task GetAll_Fail()
        {
            var UserId = Guid.NewGuid();
            var Id = Guid.NewGuid();
            await assetService.Create(new AssetDto()
            {
                Id = Id,
                code = "1",
                CreatedBy = Guid.NewGuid(),
                UserId = UserId,
                AssetType = Domain.Enum.Enums.EAssetType.TypeTow
            });
            await assetService.Create(new AssetDto()
            {
                Id = Guid.NewGuid(),
                code = "2",
                CreatedBy = Guid.NewGuid(),
                UserId = Guid.NewGuid(),
                AssetType = Domain.Enum.Enums.EAssetType.TypeTow
            });

            var res = await assetService.GetAll(new AssetDto() { UserId = Guid.NewGuid() });
            Assert.Equal(res.Count, 0);
        }
    }
}
