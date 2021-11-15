using FluentMediator;
//using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authentication.OAuth;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
//using Microsoft.Identity.Web;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

using MTM.Application;
using MTM.Application.Services.Transactions;
using MTM.Domain;
using MTM.Domain.Model;
using MTM.Infra;
using MTM.Infra.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MTM.Identity.Model;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using MTM.Infra.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using MTM.Domain.Inventories;
using AutoMapper;
using StackExchange.Redis.Extensions.Core.Configuration;
using StackExchange.Redis.Extensions.Newtonsoft;
using MTM.Cache.Redis;

namespace MTM.API
{
	public class Startup
	{
		public Startup(IConfiguration configuration)
		{
			Configuration = configuration;
		}

		public IConfiguration Configuration { get; }

		// This method gets called by the runtime. Use this method to add services to the container.
		public void ConfigureServices(IServiceCollection services)
		{

			/*
			 1-	Our customers would like to have an application where they can create multiple IT assets
			2-	They would like to have the ability to assign each of those assets to a person 
			3-	Each person needs to have a user on the system to be able to visualize the assets assigned to them 
			4-	A major part of the person details is an address assigned to each one of them 

			 */

			var redisConfiguration = Configuration.GetSection("Redis").Get<RedisConfiguration>();
			services.AddStackExchangeRedisExtensions<NewtonsoftSerializer>(redisConfiguration);

			services.AddDbContext<EFCoreDemoContext>(opts => opts.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

			services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
			services.AddScoped(typeof(IInventoryRepository), typeof(InventoryRepository));
			services.AddScoped(typeof(IInventoryTransRepository), typeof(InventoryTransRepository));
			services.AddScoped(typeof(IAssetRepository), typeof(AssetRepository));


			services.AddTransient<IInventoryService, InventoryService>();
			services.AddTransient<ITransService, TransService>();
			services.AddTransient<IAssetService, AssetService>();
			//services.AddTransient<ICacheManager, RedisCacheManager>();
			services.AddTransient<ICacheManager, MemoryCacheManager>();
			


			services.AddDbContext<ApplicationDbContext>(options =>	options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

			services.AddIdentity<ApplicationUser, IdentityRole>()
				.AddEntityFrameworkStores<ApplicationDbContext>()
				.AddDefaultTokenProviders();

			//cache 
			// services.AddDistributedMemoryCache();
			/*services.AddStackExchangeRedisCache(options =>
				{
					options.Configuration = _config["MyRedisConStr"];
					options.InstanceName = "SampleInstance";
				});
			*/

			

			// Adding Authentication  
			services.AddAuthentication(options =>
			{
				options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
				options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
				options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
			})

			// Adding Jwt Bearer  
			.AddJwtBearer(options =>
			{
				options.SaveToken = true;
				options.RequireHttpsMetadata = false;
				options.TokenValidationParameters = new TokenValidationParameters()
				{
					ValidateIssuer = true,
					ValidateAudience = true,
					ValidAudience = Configuration["JWT:ValidAudience"],
					ValidIssuer = Configuration["JWT:ValidIssuer"],
					IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["JWT:Secret"]))
				};
			});


			services.AddFluentMediator(builder =>
			{
				builder.On<Inventory>().PipelineAsync().Call<ITransService>((handler, request) => handler.CreateTrans(request));
			});



			var mapperConfig = new MapperConfiguration(mc =>
			{
				mc.AddProfile(new MappingProfile());
			});
			IMapper mapper = mapperConfig.CreateMapper();
			services.AddSingleton(mapper);

			services.AddApplicationInsightsTelemetry();
			services.AddControllers();
			services.AddSwaggerGen(c =>
			{
				c.SwaggerDoc("v1", new OpenApiInfo { Title = "MTM.API", Version = "v1" });
			});
		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
		{
			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
				app.UseSwagger();
				app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "MTM.API v1"));
			}

			app.UseHttpsRedirection();

			app.UseRouting();
			app.UseAuthentication();
			app.UseAuthorization();

			

			app.UseEndpoints(endpoints =>
			{
				endpoints.MapControllers();
			});
		}
	}
}
