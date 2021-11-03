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

namespace MTM.XTest.Services
{
    public class BaseServiceTests
    {
        public readonly IAssetRepository repository;
        public readonly IMapper _mapper;
        public readonly Mock<IMediator> _mockIMediator = new Mock<IMediator>();
        public readonly ICacheManager cacheManager = new MemoryCacheManager();



        public BaseServiceTests()
        {
            var options = new DbContextOptionsBuilder<EFCoreDemoContext>()
                .UseInMemoryDatabase(databaseName: "MovieListDatabase")
               .Options;

            var context = new EFCoreDemoContext(options);
            repository = new AssetRepository(context);

            var mapperConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new MappingProfile());
            });
            _mapper = mapperConfig.CreateMapper();
        }



    }
}
