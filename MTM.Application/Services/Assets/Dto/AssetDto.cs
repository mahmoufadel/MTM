using MTM.Domain.Inventories.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static MTM.Domain.Enum.Enums;

namespace MTM.Application.Services.Inventory.Dto
{
	public class AssetDto
	{
		public Guid Id { get; set; }

		public string code { get; set; }

		public string Description { get; set; }

		public EAssetType AssetType { get; set; }

		public Guid? UserId { get; set; }

		public Guid CreatedBy { get; set; }
	}
}
