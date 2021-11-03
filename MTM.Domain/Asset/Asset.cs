using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static MTM.Domain.Enum.Enums;

namespace MTM.Domain
{
	public class Asset
	{
		[Key]
		public Guid Id { get; set; }

		public string code { get; set; }

		public string Description { get; set; }

		public EAssetType AssetType { get; set; }

		public Guid? UserId { get; set; }

		public Guid CreatedBy { get; set; }
}
}
